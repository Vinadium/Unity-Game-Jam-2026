using UnityEngine;
using System.Collections.Generic;

public class fishInventory : MonoBehaviour
{
    public static fishInventory Instance { get; private set; }
    readonly Queue<FishData> caught = new Queue<FishData>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return;}
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int Count => caught.Count;
    public void addFish(FishData fish) => caught.Enqueue(fish);

    public bool TryGetNext(out FishData fish)
    {
        if (caught.Count > 0) { fish = caught.Dequeue(); return true; }
        fish = null;
        return false;
    }
}
