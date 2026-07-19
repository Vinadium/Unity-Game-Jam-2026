using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class dragonScale : MonoBehaviour
{
    [SerializeField] public float baseValue = 1f;
    
    public float currentValue;
    bool multiplierApplied;
    bool collected;
    SpriteRenderer spriteRenderer;
    public float CurrentValue => currentValue;

    [SerializeField] Texture2D colorGradient;


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


    float timer = 30f;

    private void Update()
    {
        UpdateVisual();


        if(timer>=0)
            timer -= Time.deltaTime;
        else
            Destroy(this.gameObject);




        if(transform.position.y < -10)
            Destroy(this.gameObject);
    }


    private void UpdateVisual()
    {
        //400 = $10000
        //0 = $0
        int pixelX = (int) currentValue / 25;
        Color colorPixel = colorGradient.GetPixel(pixelX, 0);
        spriteRenderer.color = colorPixel;
    }

    


}
