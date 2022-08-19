using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{


    private static string NowLoadingScene = "NowLoading";



    public void Start()
    {
        InitializeSceneController("Title");
    }


    public void LoadNextScene(string NextSceneName)
    {
        StartCoroutine(LoadProcess(NextSceneName));
    }

    private IEnumerator LoadProcess(string NextSceneName)
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync("NowLoading",LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);

        var unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
        var loadOperation = SceneManager.LoadSceneAsync(NextSceneName, LoadSceneMode.Additive);

        yield return new WaitUntil(()=> { return unloadOperation.isDone && loadOperation.isDone; });

        InitializeSceneController(NextSceneName);
    }


    private void InitializeSceneController(string SceneName)
    {
        var sceneController = GameObject.Find(SceneName).GetComponent<SceneController>();

        if (sceneController != null)
        {
            sceneController.OnOpenScene();
            sceneController.SetSceneManager(this);
        }
    }
}
