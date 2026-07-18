using UnityEngine;
using TMPro; 

public class IdleGameHandler : MonoBehaviour
{
    public float pegBounceUpgradeCost;
    public float pegMultUpgradeCost;
    public float moneyAmount;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] GameObject dropPoint;
    [SerializeField] float speed;
    [SerializeField] int numScales;
    [SerializeField] GameObject scale;
    [SerializeField] TMP_Text moneyCountText;
    [SerializeField] TMP_Text scaleCountText;

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        if (horizontal > 0 || horizontal < 0)
        {
            dropPoint.transform.position = new Vector3(dropPoint.transform.position.x+(horizontal*speed),dropPoint.transform.position.y);
        }
        if(Input.GetKeyDown(KeyCode.Space) && numScales > 0)
        {
            Instantiate(scale, dropPoint.transform.position, Quaternion.Euler(0,0,0));
            numScales--;
        }
        moneyCountText.text = $"Money: {moneyAmount}";
        scaleCountText.text = $"Scales: {numScales}";
    }

    public void AddMoney(float value)
    {
        moneyAmount += value;
    }

    public void RemoveMoney(float value)
    {
        moneyAmount -= value;
    }

    public void AddScale(int amount = 1)
    {
        numScales += amount;
    }

    public void IncreaseAnimationSpeed(float amount)
    {
        animationController.IncreaseAnimationSpeed(amount);
    }

    public void DecreaseAnimationSpeed(float amount)
    {
        animationController.DecreaseAnimationSpeed(amount);
    }

    public void SetAnimationSpeed(float speed)
    {
        animationController.SetAnimationSpeed(speed);
    }

    public void IncreaseToolSwings()
    {
        animationController.IncreaseSwingsPerIdle();
    }

}
