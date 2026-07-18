using UnityEngine;
using UnityEngine.UI;

public class PegData : MonoBehaviour
{
    private PhysicsMaterial2D physicsMaterial;
    private Image rimColor;
    private CircleCollider2D circleCollider;
    public float mult = 1;
    private IdleGameHandler idleGameHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        idleGameHandler = GameObject.Find("Scripts").GetComponent<IdleGameHandler>();
        circleCollider = GetComponent<CircleCollider2D>();
        physicsMaterial = new PhysicsMaterial2D();
        circleCollider.sharedMaterial = physicsMaterial;
    }


    void IncreaseMult(float multIncr)
    {
        mult += multIncr;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<dragonScale>().baseValue *= mult;
    }

}
