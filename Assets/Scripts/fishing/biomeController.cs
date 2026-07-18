using UnityEngine;

public class biomeController : MonoBehaviour
{
    [SerializeField] weighScale scale;
    [Range(0.5f, 1f)] [SerializeField] float dominanceThreshold = 0.6f;

    void Awake()
    {
        if (scale == null) scale = FindAnyObjectByType<weighScale>();
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
