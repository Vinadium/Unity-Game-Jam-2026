using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;


public class PegData : MonoBehaviour
{
    private PhysicsMaterial2D physicsMaterial;
    [SerializeField] private SpriteRenderer rimColor;
    private CircleCollider2D circleCollider;
    public float mult = 1;
    private IdleGameHandler idleGameHandler;
    [SerializeField] Texture2D colorGradient;
    float upgradeCost = 1;
    TMP_Text upgradeText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        idleGameHandler = GameObject.Find("IdleHandler").GetComponent<IdleGameHandler>();
        circleCollider = GetComponent<CircleCollider2D>();
        physicsMaterial = new PhysicsMaterial2D();
        circleCollider.sharedMaterial = physicsMaterial;
        upgradeText = GameObject.Find("UpgradeCost").GetComponent<TMP_Text>();
    }

    private void OnMouseDown()
    {
        Upgrade();
    }

    private void OnMouseEnter()
    {
        UpdateUpgradeText();
    }


    void IncreaseMult(float multIncr)
    {
        mult += multIncr;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<dragonScale>().currentValue *= mult;
    }

    public void Upgrade()
    {
        if (idleGameHandler.moneyAmount > upgradeCost)
        {
            IncreaseMult(0.1f);
            ChangeVisual();
            IncreaseUpgradeCost();
            idleGameHandler.moneyAmount -= upgradeCost;
        }

    }
    


    private void ChangeVisual()
    {
        int pixelX = (int) (mult / 0.125f);
        Color color = colorGradient.GetPixel(pixelX, 0);

        rimColor.color = color;
    }

    private void IncreaseUpgradeCost()
    {
        upgradeCost++;
    }

    void UpdateUpgradeText()
    {
        Debug.Log("Hover");
        upgradeText.text = "Upgrade Cost: " + upgradeCost.ToString();
    }


}
