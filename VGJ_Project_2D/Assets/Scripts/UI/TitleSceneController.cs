using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    private MySceneManager sceneManager;

    public void TransitionScene()
    {
        sceneManager.LoadNextScene("Result");
    }
}
