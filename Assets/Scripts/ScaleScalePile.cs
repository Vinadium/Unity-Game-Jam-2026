using UnityEngine;

public class ScaleScalePile : MonoBehaviour
{
    [Header("Pile Settings")]
    [SerializeField] private float startingRise = 0.25f;
    [SerializeField] private float maxRise = 1.5f;
    [SerializeField] private int maxVisibleScales = 20;
    [SerializeField] private float moveSpeed = 8f;

    private float startY;
    private Vector3 targetPosition;

    private void Awake()
    {
        startY = transform.localPosition.y;
        targetPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            moveSpeed * Time.deltaTime);
    }

    public void SetScaleCount(int count)
    {
        count = Mathf.Clamp(count, 0, maxVisibleScales);

        float targetY;

        if (count == 0)
        {
            // Completely empty
            targetY = startY;
        }
        else
        {
            // After the first scale, interpolate from startingRise to maxRise
            float percent = (float)(count - 1) / Mathf.Max(1, maxVisibleScales - 1);

            targetY = startY +
                      Mathf.Lerp(startingRise, maxRise, percent);
        }

        targetPosition = new Vector3(
            transform.localPosition.x,
            targetY,
            transform.localPosition.z);
    }
}