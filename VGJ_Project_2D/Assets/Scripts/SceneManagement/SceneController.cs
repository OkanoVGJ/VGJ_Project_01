using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    protected MySceneManager sceneManager;
    protected SoundManager soundManager;

    [SerializeField]
    AudioClip BGMClip;


    public virtual void OnOpenScene()
    {
        if(BGMClip != null)
        {
            soundManager.PlayBGM(BGMClip);
        }
    }

    public void SetSceneManager(MySceneManager manager)
    {
        sceneManager = manager;
    }

    public void SetSoundManager(SoundManager manager)
    {
        soundManager = manager;
    }

    public void TransitionNextScene(string SceneName)
    {
        sceneManager.LoadNextScene(SceneName);
    }

    public void PlaySE(AudioClip seClip)
    {
        soundManager.PlaySE(seClip);
    }

    void Start()
    {
    }

}
