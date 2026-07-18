using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private IdleGameHandler idleGameHandler;

    [SerializeField] [Min(1)]
    private int swingsBeforeIdle = 1;

    [SerializeField] [Min(0.1f)]
    private float animationSpeed = 1f;

    private int currentSwing = 0;

    private bool isMining = false;

    private void Start()
    {
        animator.SetInteger("state", 0);
    }

    private void Update()
    {
        animator.speed = animationSpeed;
    }

    public void StartMining()
    {
        isMining = true;
    }

    public void StopMining()
    {
        isMining = false;
    }

    public void ResetSwingCycle()
    {
        currentSwing = 0;
    }

    public void IdleFinished()
    {
        if (isMining)
        {
            animator.SetInteger("state", 1);
        }
        else
        {
            animator.SetInteger("state", 0);
        }
    }

    public void ToolFinished()
    {
        idleGameHandler.AddScale();

        currentSwing++;

        if (currentSwing >= swingsBeforeIdle)
        {
            currentSwing = 0;

            if (isMining)
            {
                animator.SetInteger("state", 0);
            }
            else
            {
                animator.SetInteger("state", 0);
            }
        }
        else
        {
            if (isMining)
            {
                animator.Play("Tool", 0, 0f);
            }
            else
            {
                animator.SetInteger("state", 0);
            }
        }
    }

    public void SetAnimationSpeed(float speed)
    {
        animationSpeed = speed;
    }

    public void IncreaseAnimationSpeed(float amount)
    {
        animationSpeed += amount;
    }

    public void DecreaseAnimationSpeed(float amount)
    {
        animationSpeed = Mathf.Max(0.1f, animationSpeed - amount);
    }

    public void IncreaseSwingsPerIdle()
    {
        swingsBeforeIdle++;
    }

    public void SetSwingsPerIdle(int amount)
    {
        swingsBeforeIdle = Mathf.Max(1, amount);
    }

    public int GetSwingsPerIdle()
    {
        return swingsBeforeIdle;
    }
}