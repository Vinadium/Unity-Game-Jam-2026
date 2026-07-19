using UnityEngine;
using System.Collections.Generic;

public class FishOnCrate : MonoBehaviour
{
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private List<FishData> fish = new List<FishData>();
   [SerializeField] private PlayerAnimationController playerAnimation;

   private FishData current;

   private int harvestedScales = 0;

   public bool loadNextFish()
    {
        if (fishInventory.Instance == null) return false;
        if (!fishInventory.Instance.TryGetNext(out FishData next)) return false;
        loadFish(next);
        return true;
    }

    private void loadFish(FishData f)
    {
        current = f;
        harvestedScales = 0;
        spriteRenderer.sprite = current.fishSprite;
        playerAnimation.ResetSwingCycle();
        playerAnimation.StartMining();
    }

   public void SetFish(int fishIndex)
    {
        if (fishIndex < 0 || fishIndex >= fish.Count)
            return;

        loadFish(fish[fishIndex]);
    }

   public void HarvestScale()
    {
        if (current == null) return;

        harvestedScales++;

        if (harvestedScales >= current.scalesBeforeSkeleton)
        {
            spriteRenderer.sprite = current.skeletonSprite;

            playerAnimation.StopMining();
            playerAnimation.ResetSwingCycle();
        }
    }

   public bool IsSkeleton()
   {
        return current != null && harvestedScales >= current.scalesBeforeSkeleton;
   }

   private void Update()
    {
        if (current == null || IsSkeleton())
        loadNextFish();
    }
}
