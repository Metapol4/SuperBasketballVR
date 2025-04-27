using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameHandleSO", menuName = "Scene Management/SceneLoader")]
public class GameHandleSO : ScriptableObject
{

    public void PauseGame(bool bIsPaused)
    {
        Time.timeScale = bIsPaused ? 0 : 1;
    }

    public void HandleNextLevel()
    {
        //Manager.Instance.Level logic to get the next one ...
        LevelManager.Instance.LoadNextLevel();
    }

    //Quit function to allow user to stop playing event in the editor
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    
    //Give a valid SceneAsset to perform a async load and open level
    public async void OpenLevel(SceneAsset newScene)
    {
        if(newScene != null)
        {
            await LoadSceneTask(newScene.name);
        }
    }
    private async Task LoadSceneTask(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            await Task.Yield(); 
        }
    }
}
