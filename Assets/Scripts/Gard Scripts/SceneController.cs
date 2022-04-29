using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController: MonoBehaviour
    {
        //UI Scene Functions

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void ReloadScene()
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        
        //Static Scene Functions

        public static void LoadSceneStatic(string scene)
        {
            SceneManager.LoadScene(scene);
        }
        
        public static void ReloadSceneStatic()
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
        
        public static void QuitGameStatic()
        {
            Application.Quit();
        }
        
    }
