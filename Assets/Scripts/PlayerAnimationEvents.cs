using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private IdleGameHandler idleGameHandler;
    [SerializeField] private PlayerAnimationController animationController;

    public void IdleFinished()
    {
        animationController.IdleFinished();
    }

    public void ToolFinished()
    {
        idleGameHandler.AddScale();
        animationController.ToolFinished();
    }
}