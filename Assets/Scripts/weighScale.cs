using UnityEngine;

public class weighScale : MonoBehaviour
{
    [Header("Scale Balancing")]
    [SerializeField] float degreesPerScale = 2f;
    [SerializeField] float maxTitleAngle = 30f;
    [SerializeField] float tiltSmoothing = 5f;

    public int leftCount { get; private set;}
    public int rightCount { get; private set;}

    Rigidbody2D rb;
    float currentAngle;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        int imbalance = rightCount - leftCount;
        float target = Mathf.Clamp(imbalance * degreesPerScale, -maxTitleAngle, maxTitleAngle);

        currentAngle = Mathf.Lerp(currentAngle, target, tiltSmoothing * Time.fixedDeltaTime);

        rb.MoveRotation(-currentAngle);
    }

    public void Add(bool left)
    {
        if (left) leftCount++;
        else rightCount++;
    }

    public void Remove(bool left)
    {
        if (left) leftCount = Mathf.Max(0, leftCount - 1);
        else rightCount = Mathf.Max(0, rightCount - 1);
    }
}
