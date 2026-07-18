using UnityEngine;

public class hookMovement : MonoBehaviour
{
    [Header("Hook Movement")]
    [SerializeField] float followSmoothing = 12f;

    [Header("Water space")]
    [SerializeField] public Vector2 minBounds = new Vector2(-8f, -4f);
    [SerializeField] public Vector2 maxBounds = new Vector2(8f, 3f);

    public bool isBusy { get; set;}

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (isBusy) return;
        
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -cam.transform.position.z;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(screenPos);

        Vector2 target = new Vector2(
            Mathf.Clamp(mouseWorld.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(mouseWorld.y, minBounds.y, maxBounds.y));

        transform.position = target;
    }
}
