using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class compendiumEntry
{
    public string fishName;
    public float topWeight;
    public int caughtCount;
}

public class compendium : MonoBehaviour
{
    public static compendium Instance { get; private set;}
    const string saveKey = "compendium";

    [System.Serializable]
    class saveData { public List<compendiumEntry> entries = new List<compendiumEntry>(); }

    readonly Dictionary<string, compendiumEntry> entries = new Dictionary<string, compendiumEntry>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        load();
    }

    public void recordCatch(string fishName, float weight)
    {
        if (!entries.TryGetValue(fishName, out compendiumEntry e))
        {
            e = new compendiumEntry { fishName = fishName, topWeight = 0f, caughtCount = 0 };
            entries[fishName] = e;
        }
        e.caughtCount++;
        if (weight > e.topWeight) e.topWeight = weight;
        save();
    }

    public bool hasCaught(string fishName) => entries.ContainsKey(fishName);
    public float getTopWeight(string fishName) => entries.TryGetValue(fishName, out var e) ? e.topWeight : 0f;
    public int getCount(string fishName) => entries.TryGetValue(fishName, out var e) ? e.caughtCount : 0;
    public IEnumerable<compendiumEntry> allEntries => entries.Values;

    void save()
    {
       saveData data = new saveData { entries = new List<compendiumEntry>(entries.Values)};
       PlayerPrefs.SetString(saveKey, JsonUtility.ToJson(data));
       PlayerPrefs.Save(); 
    }

    void load()
    {
        entries.Clear();
        if (!PlayerPrefs.HasKey(saveKey)) return;
        saveData data = JsonUtility.FromJson<saveData>(PlayerPrefs.GetString(saveKey));
        if (data?.entries == null) return;
        foreach (compendiumEntry e in data.entries) entries[e.fishName] = e;
    }
}
