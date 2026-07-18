using UnityEngine;

public class ScalePile : MonoBehaviour
{
    [SerializeField] private float startY;
    [SerializeField] private float maxRise = 3f;
    [SerializeField] private int maxScales = 1000;

    private int currentScales;

    private void Start ()
    {
        startY = transform.localPosition.y;
    }

    public void SetScaleCount(int amount)
    {
        currentScales = Mathf.Clamp(amount, 0, maxScales);

        float percent = (float)currentScales / maxScales;

        Vector3 pos = transform.localPosition;
        pos.y = startY + percent * maxRise;
        transform.localPosition = pos;
    }
}
