using UnityEngine;
using System.Collections;

public class BackgroundMusicHandler : MonoBehaviour
{
    [Header("Playlist Settings")]
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private float fadeOutDuration = 2.0f;
    [SerializeField] private float fadeInDuration = 2.0f;
    [SerializeField] private float maxVolume = 1.0f;

    [Tooltip("Start fading out this many seconds before the current clip ends.")]
    [SerializeField] private float fadeTriggerRemainingSeconds = 5.0f;

    private AudioSource audioSource;
    private Coroutine transitionCoroutine;
    private int currentClipIndex = -1;
    private bool isTransitioning = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Start the playlist if you have clips assigned
        if (playlist != null && playlist.Length > 0)
        {
            PlayRandomTrack();
        }
    }

    private void Update()
    {
        // Block check if nothing is playing, if a fade is happening, or if the array is empty
        if (audioSource.clip == null || !audioSource.isPlaying || isTransitioning || playlist.Length <= 1) return;

        // Calculate time remaining in the current clip
        float remainingTime = audioSource.clip.length - audioSource.time;

        // Trigger the next random song near the end
        if (remainingTime <= fadeTriggerRemainingSeconds)
        {
            PlayRandomTrack();
        }
    }

    /// <summary>
    /// Chooses a new random clip and initiates the fade transition.
    /// </summary>
    public void PlayRandomTrack()
    {
        if (playlist.Length == 0) return;

        int nextIndex = currentClipIndex;

        // If there's more than one song, make sure the new song isn't the same as the current one
        if (playlist.Length > 1)
        {
            while (nextIndex == currentClipIndex)
            {
                nextIndex = Random.Range(0, playlist.Length);
            }
        }
        else
        {
            nextIndex = 0; // Fallback if only 1 song exists
        }

        currentClipIndex = nextIndex;
        AudioClip nextClip = playlist[currentClipIndex];

        // Safely start the fade sequence
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        transitionCoroutine = StartCoroutine(FadeOutAndInRoutine(nextClip));
    }

    private IEnumerator FadeOutAndInRoutine(AudioClip nextClip)
    {
        isTransitioning = true;

        // 1. FADE OUT CURRENT CLIP
        if (audioSource.isPlaying && audioSource.volume > 0f)
        {
            float startVolume = audioSource.volume;
            float timeElapsed = 0f;

            while (timeElapsed < fadeOutDuration)
            {
                timeElapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / fadeOutDuration);
                yield return null;
            }
        }

        // 2. SWAP THE CLIP
        audioSource.Stop();
        audioSource.clip = nextClip;
        audioSource.volume = 0f;
        audioSource.Play();

        // Mark transition as done so the Update loop can safely monitor the next track
        isTransitioning = false;

        // 3. FADE IN NEW CLIP
        float fadeInElapsed = 0f;
        while (fadeInElapsed < fadeInDuration)
        {
            fadeInElapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, maxVolume, fadeInElapsed / fadeInDuration);
            yield return null;
        }

        audioSource.volume = maxVolume;
    }
}
