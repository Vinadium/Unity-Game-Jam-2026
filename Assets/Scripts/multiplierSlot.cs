using UnityEngine;

public class multiplierSlot : MonoBehaviour
{
    [SerializeField] float multiplier = 1f;
    [SerializeField] string scaleTag = "dragonScale";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(scaleTag)) return;

        dragonScale ds = other.GetComponent<dragonScale>();
        if (ds !=  null) ds.applyMultiplier(multiplier);
    }
}
