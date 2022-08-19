using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    private string FirstScene = "Title";
    private static string NowLoadingScene = "NowLoading";


    public void Awake()
    {
        SceneManager.LoadScene(FirstScene, LoadSceneMode.Additive);
    }

    public void Start()
    {
        StartCoroutine(SceneInit());
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
        unloadOperation.allowSceneActivation = false;
        loadOperation.allowSceneActivation = true;
        yield return new WaitUntil(()=> { return unloadOperation.isDone && loadOperation.isDone; });
        yield return null;
        unloadOperation = SceneManager.UnloadSceneAsync("NowLoading");
        yield return new WaitUntil(()=> { return unloadOperation.isDone; });

        InitializeSceneController(NextSceneName);
    }


    private void InitializeSceneController(string SceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));
        var sceneController = GameObject.Find(SceneName).GetComponent<SceneController>();

        if (sceneController != null)
        {
            sceneController.OnOpenScene();
            sceneController.SetSceneManager(this); 

        }
    }

    private IEnumerator SceneInit()
    {
        yield return null;
        InitializeSceneController(FirstScene);

    }
}
