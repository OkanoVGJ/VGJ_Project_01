using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    protected MySceneManager sceneManager;

    public virtual void OnOpenScene()
    {

    }

    public void SetSceneManager(MySceneManager manager)
    {
        sceneManager = manager;
    }

    public void TransitionNextScene(string SceneName)
    {
        sceneManager.LoadNextScene(SceneName);
    }

    void Start()
    {
    }
    private IEnumerator TransitionTest()
    {
           yield return new WaitForSeconds(2.0f);
            TransitionNextScene("result");
    }
}
