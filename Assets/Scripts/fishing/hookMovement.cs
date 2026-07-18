using UnityEngine;

public class hookMovement : MonoBehaviour
{

    [Header("Water space")]
    [SerializeField] public Transform minPoint;
    [SerializeField] public Transform maxPoint;

    [SerializeField] public Vector2 minBounds;
    [SerializeField] public Vector2 maxBounds;

    public bool isBusy { get; set;}

    Camera cam;

    void Awake()
    {
        minBounds = new Vector2(minPoint.position.x, minPoint.position.y);
        maxBounds = new Vector2(maxPoint.position.x, maxPoint.position.y);
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
