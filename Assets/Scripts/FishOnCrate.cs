using UnityEngine;
using System.Collections.Generic;

public class FishOnCrate : MonoBehaviour
{
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private List<FishData> fish = new List<FishData>();

   private int currentFish = 0;

   private int harvestedScales = 0;

   public void SetFish(int fishIndex)
   {
        if (fishIndex < 0 || fishIndex >= fish.Count)
        {
            return;
        }

        currentFish = fishIndex;
        harvestedScales = 0;

        spriteRenderer.sprite = fish[currentFish].fishSprite;
   }

   public void HarvestScale()
   {
        harvestedScales++;

        if (harvestedScales >= fish[currentFish].scalesBeforeSkeleton)
        {
            spriteRenderer.sprite = fish[currentFish].skeletonSprite;
        }
   }

   public bool IsSkeleton()
   {
        return harvestedScales >= fish[currentFish].scalesBeforeSkeleton;
   }
}
