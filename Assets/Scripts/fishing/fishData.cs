using UnityEngine;

public enum fishBiome { Any, Freshwater, Saltwater }

public class fishData : MonoBehaviour
{
    [Header("Identity")]
    public string fishName = "New Fish";
    public Sprite sprite;

    [Header("Value")]
    public float baseValue = 1f;

    [Header("Spawning")]
    public fishBiome biome = fishBiome.Any;
    public float spawnWeight = 10f;

    [Header("Weight range")]
    public float minWeight = 0.5f;
    public float maxWeight = 5f;

    [Header("Behavior overrides")]
    public float swimSpeed = 1.5f;
    [Range(0f, 1f)] public float biteChance = 0.5f;

    public float RollWeight() => Random.Range(minWeight, maxWeight);

    public int scaleValueFor(float weight) => Mathf.Max(1, Mathf.RoundToInt(baseValue * weight)); 
}
