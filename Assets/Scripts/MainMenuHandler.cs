using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    float volume = 0.5f;
    bool optionsOpened = false;
    [SerializeField] private GameObject optionsMenu;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
        }





    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleOptions()
    {
        if (!optionsOpened)
        {
            optionsMenu.SetActive(true);
            optionsOpened = true;
        }
        else
        {
            optionsMenu.SetActive(false);
            optionsOpened = false;
        }
    }



}
