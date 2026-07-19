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
    [SerializeField] Transform spawnedParent;
    [SerializeField] IdleGameHandler idleGameHandler;

    [Header("Rarity")]
    [SerializeField] float rarityBiasPerLevel = 0.5f;
    [SerializeField] float minBiasExponent = 0.15f;


    readonly List<fishController> active = new List<fishController>();
    float respawnTimer;

    void Awake()
    {
        if (biome == null) biome = FindAnyObjectByType<biomeController>();
        if (hook == null) hook = FindAnyObjectByType<hookMovement>();
        if (idleGameHandler == null) idleGameHandler = FindAnyObjectByType<IdleGameHandler>(FindObjectsInactive.Include);
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

        GameObject go = Instantiate(fishPrefab, pos, Quaternion.identity,spawnedParent);
        fishController fish = go.GetComponent<fishController>();
        fish.Initialize(data, data.RollWeight());
        active.Add(fish);
    }

    fishData pickFish()
    {
        fishBiome current = biome != null ? biome.currentBiome : fishBiome.Any;

        int rodLevel = idleGameHandler != null ? idleGameHandler.rodLevel : 1;
        int upgrades = Mathf.Max(0, rodLevel - 1);
        float biasExponent = Mathf.Max(minBiasExponent, 1f/ (1f + upgrades * rarityBiasPerLevel));

        float total = 0f;
        List<fishData> eligible = new List<fishData>();
        List<float> weights = new List<float>();
        foreach (fishData f in allFish)
        {
            if (f == null) continue;
            if (f.biome == current)
            {
                float w = Mathf.Pow(f.spawnWeight, biasExponent);
                eligible.Add(f);
                weights.Add(w);
                total += w;

            }
        }
        if (eligible.Count == 0 || total <= 0f) return null;

        float r = Random.Range(0f, total);
        for (int i=0;i<eligible.Count;i++)
        {
            r -= weights[i];
            if (r <= 0f) return eligible[i];
        }
        return eligible[eligible.Count - 1];
    }

}
