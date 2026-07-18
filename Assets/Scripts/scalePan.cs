using System;
using UnityEngine;

public class scalePan : MonoBehaviour
{
    public enum Side { left, right}

    [SerializeField] IdleGameHandler handler;
    [SerializeField] weighScale scale;
    [SerializeField] Side side;
    [SerializeField] string scaleTag = "dragonScale";

    bool isLeft => side == Side.left;

    void Awake()
    {
        if (handler == null)
            handler = GameObject.Find("IdleHandler").GetComponent<IdleGameHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Scale entered box bueno");
        if (!other.CompareTag(scaleTag)) return;

        dragonScale ds = other.GetComponent<dragonScale>();
        if (ds == null) return;

        if (!ds.tryCollect(out float value)) return;

        scale.Add(isLeft);
        handler.AddMoney(value);
    }
}
