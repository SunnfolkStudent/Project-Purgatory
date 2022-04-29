using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene("Hub");
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Return()
    {
        
    }
}
