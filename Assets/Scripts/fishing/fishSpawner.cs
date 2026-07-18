using UnityEngine;
using System.Collections.Generic;

public class fishSpawner : MonoBehaviour
{
    [Header("Fish pool")]
    [SerializeField] fishData[] allFish;
    [SerializeField] GameObject fishPrefab;

    [Header("Pond")]
    [SerializeField] int pondSize = 6;
    [SerializeField] float respawnDelay = 1f;

    [Header("Spawn area (world space)")]
    
    [Header("Refrences")]
    [SerializeField] biomeController biome;
    [SerializeField] hookMovement hook;

    readonly List<fishController> active = new List<fishController>();
    float respawnTimer;

    void Awake()
    {
        if (biome == null) biome = FindAnyObjectByType<biomeController>();
    }

    void Start()
    {
        spawnPond();
    }

    void Update()
    {
        active.RemoveAll(f => f == null);

        if (active.Count > 0) return;

        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0f) spawnPond();
    }

    void spawnPond()
    {
        for (int i=0; i < pondSize; i++) spawnOne();
        respawnTimer = respawnDelay;
    }

    void spawnOne()
    {
        fishData data = pickFish();
        if (data == null) return;

        Vector2 pos = new Vector2(
            Random.Range(hook.minBounds.x, hook.maxBounds.x),
            Random.Range(hook.minBounds.y, hook.maxBounds.y)
        );
    }

    fishData pickFish()
    {
        fishBiome current = biome != null ? biome.currentBiome : fishBiome.Any;

        float total = 0f;
        List<fishData> eligible = new List<fishData>();
        foreach (fishData f in allFish)
        {
            if (f == null) continue;
            if (f.biome == fishBiome.Any || f.biome == current)
            {
                eligible.Add(f);
                total += f.spawnWeight;
            }
        }
        if (eligible.Count == 0 || total <= 0f) return null;

        float r = Random.Range(0f, total);
        foreach (fishData f in eligible)
        {
            r -= f.spawnWeight;
            if (r <= 0f) return f;
        }
        return eligible[eligible.Count - 1];
    }

}
