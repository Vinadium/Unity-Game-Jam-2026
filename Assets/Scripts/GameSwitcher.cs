using UnityEngine;

public class GameSwitcher : MonoBehaviour
{

    [SerializeField] GameObject cam;

    [Header("Idle")]
    [SerializeField] GameObject idleUI, idleGame;

    [Header("Fishing")]
    [SerializeField] GameObject fishUI, fishGame;

    private void Awake()
    {
        fishUI.SetActive(false);
    }


    public void PlayFishing()
    {
        idleUI.SetActive(false);
        idleGame.SetActive(false);
        fishUI.SetActive(true);
        fishGame.SetActive(true);

        //Move Camera
        cam.transform.position = new Vector3(42,0,-10);
    }


    public void PlayIdle()
    {
        idleUI.SetActive(true);
        idleGame.SetActive(true);
        fishUI.SetActive(false);
        fishGame.SetActive(false);

        cam.transform.position = new Vector3(0, 0, -10);

    }


}
