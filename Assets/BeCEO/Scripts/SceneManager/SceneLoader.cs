using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public static class SceneLoader
{
    public static int localeID;
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }
    public static void ReloadScene()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameScene);
    }


    // future features
    public static void StartFadeSplash()
    {

    }

    public static void StartAppearSplash()
    {

    }
    
}
