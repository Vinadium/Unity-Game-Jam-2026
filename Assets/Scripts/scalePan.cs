using UnityEngine;

public class scalePan : MonoBehaviour
{
    public enum Side { left, right}

    [SerializeField] weighScale scale;
    [SerializeField] Side side;
    [SerializeField] string scaleTag = "Scale";

    bool isLeft => side == Side.left;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(scaleTag)) scale.Add(isLeft);
    }
}
