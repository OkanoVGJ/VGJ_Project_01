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

    async void LoadProcess(string NextSceneName)
    {
    }
}
