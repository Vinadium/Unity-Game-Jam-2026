using UnityEngine;
using TMPro; 

public class IdleGameHandler : MonoBehaviour
{
    
    public float moneyAmount;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] GameObject dropPoint;
    [SerializeField] int numScales;
    [SerializeField] GameObject scale;
    [SerializeField] TMP_Text moneyCountText;
    [SerializeField] TMP_Text scaleCountText;
    [SerializeField] private ScalePile scalePile;
    [SerializeField] private FishOnCrate fishOnCrate;
    [SerializeField] float launchForce;
    bool shopOpened = false;
    [SerializeField] float rotateSpeed;
    [Header("Shop")]
    [SerializeField] GameObject shop;
    [SerializeField] TMP_Text toolCostText, speedCostText, hitCostText, rodCostText;
    [SerializeField] TMP_Text toolStatText, speedStatText, hitStatText, rodStatText;
    int toolUpgradeCost = 50, speedUpgradeCost=2, hitUpgradeCost=20, rodUpgradeCost=30;
    float speedIncr = 0.1f, hitIncr=1;
    int toolLevel = 1, rodLevel = 1;

    private void Awake()
    {
        toolCostText.text = "Cost: " + toolUpgradeCost;
        speedCostText.text = "Cost: " + speedUpgradeCost;
        hitCostText.text = "Cost: " + hitUpgradeCost;
        rodCostText.text = "Cost: " + rodUpgradeCost;
        toolStatText.text = toolLevel + " per hit -> " + (toolLevel + 1) + " per hit";
        speedStatText.text = animationController.animationSpeed + " -> " + (animationController.animationSpeed + speedIncr);
        hitStatText.text = animationController.swingsBeforeIdle + " -> " + (animationController.swingsBeforeIdle + hitIncr);
        rodStatText.text = "SOMETHING, WHO KNOWS";
    }

    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        

        if (horizontal < 0)
        {
            Vector3 rotationSpeed = new Vector3(0, 0, -rotateSpeed);
            dropPoint.transform.Rotate(rotationSpeed * Time.deltaTime);
        }
        else if (horizontal > 0)
        {
            Vector3 rotationSpeed = new Vector3(0, 0, rotateSpeed);
            dropPoint.transform.Rotate(rotationSpeed*Time.deltaTime);
        }




        if(Input.GetKeyDown(KeyCode.Space) && numScales > 0)
        {
            Debug.Log(dropPoint.transform.rotation.eulerAngles);
            GameObject scaleObj = Instantiate(scale, dropPoint.transform.position, dropPoint.transform.rotation);
            scaleObj.GetComponent<Rigidbody2D>().linearVelocity = -dropPoint.transform.up * launchForce;
            numScales--;
            scalePile.SetScaleCount(numScales);
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

    public void AddScale()
    {
        int amount = toolLevel;
        numScales += amount;
        scalePile.SetScaleCount(numScales);
        fishOnCrate.HarvestScale();
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

    public void ToggleShop()
    {
        if (!shopOpened)
        {
            shop.SetActive(true);
            shopOpened = true;
        }
        else
        {
            shop.SetActive(false);
            shopOpened = false;
        }
    }


    public void UpgradeTool()
    {
        if (moneyAmount>= toolUpgradeCost && toolLevel <= 4)
        {
            moneyAmount -= toolUpgradeCost;
            toolLevel++;
            animationController.SetToolLevel(toolLevel);
            toolUpgradeCost = (int)(toolUpgradeCost * 1.5);
            toolCostText.text = "Cost: " + toolUpgradeCost;
            toolStatText.text = toolLevel + " per hit -> " + (toolLevel + 1) + " per hit";
        }
    }

    public void UpgradeSpeed()
    {
        if(moneyAmount>= speedUpgradeCost)
        {
            moneyAmount -= speedUpgradeCost;
            IncreaseAnimationSpeed(speedIncr);
            speedUpgradeCost = (int)(speedUpgradeCost * 1.5);
            speedCostText.text = "Cost: " + speedUpgradeCost;
            speedStatText.text = animationController.animationSpeed + " -> " + (animationController.animationSpeed+speedIncr);
        }
    }

    public void UpgradeHits()
    {
        if (moneyAmount >= hitUpgradeCost)
        {
            moneyAmount -= hitUpgradeCost;
            IncreaseToolSwings();
            hitUpgradeCost = (int)(hitUpgradeCost * 1.5);
            hitCostText.text = "Cost: " + hitUpgradeCost;
            hitStatText.text = animationController.swingsBeforeIdle + " -> " + (animationController.swingsBeforeIdle+hitIncr);
        }
    }

    public void UpgradeRod()
    {
        if (moneyAmount >= rodUpgradeCost)
        {
            moneyAmount -= rodUpgradeCost;
            //////////////////////////////////////////////////////////////////////////////////////UPGRADE ROD????????


            rodUpgradeCost = (int)(rodUpgradeCost * 1.5);
            rodCostText.text = "Cost: " + rodUpgradeCost;

            hitStatText.text = "SOMETHING, WHO KNOWS";
        }
    }
}
