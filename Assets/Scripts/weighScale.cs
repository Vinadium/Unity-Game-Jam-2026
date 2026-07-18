using UnityEngine;
using UnityEngine.UIElements;

public class weighScale : MonoBehaviour
{
    [Header("Pan references")]
    [SerializeField] Transform leftPan;
    [SerializeField] Transform rightPan;

    [Header("Balance movement")]
    [SerializeField] float unitsPerScale = 0.1f;
    [SerializeField] float maxOffset = 0.5f;
    [SerializeField] float moveSmoothing = 5f;

    public int leftCount { get; private set;}
    public int rightCount { get; private set;}

    Rigidbody2D rb;
    float leftRestY, rightRestY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        leftRestY = leftPan.localPosition.y;
        rightRestY = rightPan.localPosition.y;
    }

    void FixedUpdate()
    {
        int diff = leftCount - rightCount;
        float offset = Mathf.Clamp(diff * unitsPerScale, -maxOffset, maxOffset);

        movePanY(leftPan, leftRestY - offset);
        movePanY(rightPan, rightRestY + offset);
    }

    void movePanY(Transform pan, float targetY)
    {
        Vector3 p = pan.localPosition;
        p.y = Mathf.Lerp(p.y, targetY, moveSmoothing * Time.fixedDeltaTime);
        pan.localPosition = p;
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
