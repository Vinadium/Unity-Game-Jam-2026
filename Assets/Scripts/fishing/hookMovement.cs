using UnityEngine;

public class hookMovement : MonoBehaviour
{
    [Header("Hook Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Water space")]
    [SerializeField] public Vector2 minBounds = new Vector2(-8f, -4f);
    [SerializeField] public Vector2 maxBounds = new Vector2(8f, 3f);

    public bool isBusy { get; set;}

    void Update()
    {
        if (isBusy) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 pos = transform.position;
        pos.x += h * moveSpeed * Time.deltaTime;
        pos.y += v * moveSpeed * Time.deltaTime;

        transform.position = pos;
    }
}
