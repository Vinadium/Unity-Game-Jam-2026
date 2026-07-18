using UnityEngine;

public class dragonScale : MonoBehaviour
{
    [SerializeField] public float baseValue = 1f;
    
    float currentValue;
    bool multiplierApplied;
    bool collected;

    public float CurrentValue => currentValue;

    void Awake()
    {
        currentValue = baseValue;
    }

    public void applyMultiplier(float multiplier)
    {
        if (multiplierApplied) return;
        currentValue *= multiplier;
        multiplierApplied = true;
    }

    public bool tryCollect(out float value)
    {
        value = currentValue;
        if (collected) return false;
        collected = true;
        return true;
    }
}
