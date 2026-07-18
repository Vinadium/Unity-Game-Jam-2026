using UnityEngine;

public class IdleGameHandler : MonoBehaviour
{
    public float pegBounceUpgradeCost;
    public float pegMultUpgradeCost;
    public float scaleAmount;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.CompareTag("Peg"))
            {
                Debug.Log("Clicked Peg");
            }
        }
    }

    public void AddScales(float value)
    {
        scaleAmount += value;
    }

    public void RemoveScales(float value)
    {
        scaleAmount -= value;
    }

}
