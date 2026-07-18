using UnityEngine;

public class dragonScale : MonoBehaviour
{
    [SerializeField] public float baseValue = 1f;
    
    public float currentValue;
    bool multiplierApplied;
    bool collected;
    SpriteRenderer spriteRenderer;
    public float CurrentValue => currentValue;

    void Awake()
    {
        currentValue = baseValue;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void Update()
    {
        UpdateVisual();

        if(transform.position.y < -10)
            Destroy(this.gameObject);
    }


    private void UpdateVisual()
    {
        Debug.Log("UPDATE VISUALS");
        if (currentValue <= 10)
        {
            Debug.Log("White");
            spriteRenderer.color = Color.white;
        }else if (currentValue <= 50)
        {
            Debug.Log("Blue");
            spriteRenderer.color = Color.blue;
        }else if (currentValue <= 100)
        {
            Debug.Log("Green");
            spriteRenderer.color = Color.green;
        }else if (currentValue <= 200)
        {
            Debug.Log("Red");
            spriteRenderer.color = Color.red;
        }
    }
}
