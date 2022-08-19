using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField]
    private string NowLoadingScene = "NowLoading";

    public void LoadNextScene(string NextSceneName)
    {
        LoadProcess(NextSceneName);
    }

    private  IEnumerator LoadProcess(string NextSceneName)
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync("NowLoading",LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);

        var unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
        var loadOperation = SceneManager.LoadSceneAsync(NextSceneName, LoadSceneMode.Additive);

        yield return new WaitUntil(()=> { return unloadOperation.isDone && loadOperation.isDone; });

        var sceneController = GameObject.Find(NextSceneName).GetComponent<SceneController>();

        if(sceneController != null)
        {
            sceneController.OnOpenScene();
        }
    }
}
