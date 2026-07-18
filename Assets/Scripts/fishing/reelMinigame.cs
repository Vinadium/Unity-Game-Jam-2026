using UnityEngine;
using UnityEngine.UIElements;

public class reelMinigame : MonoBehaviour
{
    [Header("Catch zone")]
    [SerializeField] float zoneHeight = 0.18f;
    [SerializeField] float upwardsAccel = 3f;
    [SerializeField] float gravity = 2f;
    [SerializeField] float maxSpeed = 1.5f;
    [SerializeField] KeyCode reelKey = KeyCode.Space;

    [Header("Fish movement")]
    [SerializeField] float fishMoveInterval = 0.7f;
    [SerializeField] float fishSpeed = 1.2f;

    [Header("Progress")]
    [SerializeField] float fillRate = 0.4f;
    [SerializeField] float drainRate = 0.25f;

    [Header("UI System")]
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform track;
    [SerializeField] RectTransform catchZoneUI;
    [SerializeField] RectTransform fishUI;
    [SerializeField] UnityEngine.UI.Image progressFill;

    public bool isActive { get; private set; }

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
    }

    fishController currentFish;
    float zoneCenter, zoneVel;
    float fishPos, fishTarget, fishTimer;
    float progress;

    void Update()
    {
        if (!isActive) return;

        updateZone();
        updateFish();
        updateProgress();
        updateUI();

        if (progress >= 1f) finish(true);
        else if (progress <= 0f) finish(false);
    }

    public void startMinigame(fishController fish)
    {
        currentFish = fish;
        isActive = true;
        zoneCenter = 0.5f; 
        zoneVel = 0f;
        fishPos = 0.5f;
        pickFishTarget();
        progress = 0.3f;
        if (panel != null) panel.SetActive(true);
    }

    void updateZone()
    {
        float accel = Input.GetKey(reelKey) ? upwardsAccel : -gravity;
        zoneVel = Mathf.Clamp(zoneVel + accel * Time.deltaTime, -maxSpeed, maxSpeed);
        zoneCenter = Mathf.Clamp01(zoneCenter + zoneVel * Time.deltaTime);
        if (zoneCenter <= 0f || zoneCenter >= 1f) zoneVel = 0f;
    }

    void updateFish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer <= 0f) pickFishTarget();
        fishPos = Mathf.MoveTowards(fishPos, fishTarget, fishSpeed * Time.deltaTime);
    }

    void pickFishTarget()
    {
        fishTarget = Random.value;
        fishTimer = fishMoveInterval * Random.Range(0.6f, 1.4f);
    }

    void updateProgress()
    {
        bool overlapping = Mathf.Abs(fishPos - zoneCenter) <= zoneHeight * 0.5;
        progress = Mathf.Clamp01(progress + (overlapping ? fillRate: -drainRate) * Time.deltaTime);
    }

    void updateUI()
    {
        if (track == null) return;
        float w = track.rect.width;
        if (catchZoneUI != null)
        {
            catchZoneUI.sizeDelta = new Vector2(zoneHeight * w, catchZoneUI.sizeDelta.y);
            catchZoneUI.anchoredPosition = new Vector2((zoneCenter - 0.5f) * w, catchZoneUI.anchoredPosition.y);
        }
        if (fishUI != null)
            fishUI.anchoredPosition = new Vector2((fishPos - 0.5f) * w, fishUI.anchoredPosition.y);
        if (progressFill != null)
            progressFill.fillAmount = progress;
    }

    void finish(bool caught)
    {
        isActive = false;
        if (caught) currentFish.onReeledIn();
        else currentFish.onEscape();
        currentFish = null;
        if (panel != null) panel.SetActive(false);
    }
}
