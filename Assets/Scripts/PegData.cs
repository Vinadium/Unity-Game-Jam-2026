using UnityEngine;
using UnityEngine.UI;

public class PegData : MonoBehaviour
{
    private PhysicsMaterial2D physicsMaterial;
    [SerializeField] private SpriteRenderer rimColor;
    private CircleCollider2D circleCollider;
    public float mult = 1;
    private IdleGameHandler idleGameHandler;
    [SerializeField] Texture2D colorGradient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        idleGameHandler = GameObject.Find("Scripts").GetComponent<IdleGameHandler>();
        circleCollider = GetComponent<CircleCollider2D>();
        physicsMaterial = new PhysicsMaterial2D();
        circleCollider.sharedMaterial = physicsMaterial;
    }

    private void OnMouseDown()
    {
        Upgrade();
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
        Debug.Log("Clicked Upgrade");
        IncreaseMult(0.1f);
        ChangeVisual();
    }


    private void ChangeVisual()
    {
        int pixelX = (int) (mult / 0.125f);
        Color color = colorGradient.GetPixel(pixelX, 0);

        rimColor.color = color;
    }



}
