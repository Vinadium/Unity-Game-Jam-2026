using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class compendiumUi : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text listText;
    [SerializeField] fishSpawner spawner;

    bool isOpen = false;

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
        if (spawner == null) spawner = FindAnyObjectByType<fishSpawner>(FindObjectsInactive.Include);
    }

    public void toggleCompendium()
    {
        isOpen = !isOpen;
        if (panel != null) panel.SetActive(isOpen);
        if (isOpen) refresh();
    }

    void refresh()
    {
        if (listText == null) return;

        StringBuilder sb = new StringBuilder();

        if (spawner != null && spawner.AllFish != null)
        {
            var roster = spawner.AllFish.Where(f => f != null).OrderBy(f => f.fishName);

            foreach (fishData f in roster)
            {
                bool caught = compendium.Instance != null && compendium.Instance.hasCaught(f.fishName);
                if (caught)
                {
                    float w = compendium.Instance.getTopWeight(f.fishName);
                    int c = compendium.Instance.getCount(f.fishName);
                    sb.AppendLine($"{f.fishName} - {w:F2} kg (caught {c}x)");
                }
                else
                {
                    sb.AppendLine("???");
                }
            }
        }
        

        listText.text = sb.Length > 0 ? sb.ToString(): "No fish known yet.";
    }
}
