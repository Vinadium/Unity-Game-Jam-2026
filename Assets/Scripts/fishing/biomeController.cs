using UnityEngine;

public class biomeController : MonoBehaviour
{
    [SerializeField] weighScale scale;
    [Range(0.5f, 1f)] [SerializeField] float dominanceThreshold = 0.6f;

    [Header("Backgrounds")]
    [SerializeField] GameObject freshwaterBackground;
    [SerializeField] GameObject saltwaterBackground;

    fishBiome lastBiome;
    bool initialized = false;

    void Awake()
    {
        if (scale == null) scale = FindAnyObjectByType<weighScale>();
    }

    void Update()
    {
        fishBiome current = currentBiome;
        if (initialized && current == lastBiome) return;

        if (freshwaterBackground != null) freshwaterBackground.SetActive(current == fishBiome.Freshwater);
        if (saltwaterBackground != null) saltwaterBackground.SetActive(current == fishBiome.Saltwater);

        lastBiome = current;
        initialized = true;
    }

    public fishBiome currentBiome
    {
        get
        {
            if (scale == null) return fishBiome.Any;

            int total = scale.leftCount + scale.rightCount;
            if (total == 0) return fishBiome.Any;

            float leftRatio = (float)scale.leftCount / total;
            float rightRatio =  (float)scale.rightCount / total;

            if (leftRatio > dominanceThreshold) return fishBiome.Freshwater;
            if (rightRatio > dominanceThreshold) return fishBiome.Saltwater;
            return fishBiome.Any;
        }
    }
}
