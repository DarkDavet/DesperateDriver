using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadGameplayScene()
    {
        Debug.Log("Gameplay scene loaded");
        StartCoroutine(LoadAndStartGameplay());
        //SceneManager.LoadSceneAsync(Scenes.GAMEPLAY);
    }

    public void LoadMainMenuScene()
    {
        Debug.Log("MainMenu> scene loaded");
        StartCoroutine(LoadAndStartMainMenu());
    }
    private IEnumerator LoadAndStartGameplay()
    {
        yield return Load(Scenes.GAMEPLAY);
        yield return new WaitForSeconds(1);

    }

    private IEnumerator LoadAndStartMainMenu()
    {
        yield return Load(Scenes.MAIN_MENU);
        yield return new WaitForSeconds(1);

    }

    private IEnumerator Load(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}

