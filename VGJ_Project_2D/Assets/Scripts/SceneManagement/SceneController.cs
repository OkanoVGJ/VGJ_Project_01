using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    protected MySceneManager sceneManager;
    protected SoundManager soundManager;

    [SerializeField]
    AudioClip BGMClip;

    private float GameTime = 0.0f;

    public virtual void OnOpenScene()
    {
        if(BGMClip != null)
        {
            soundManager.PlayBGM(BGMClip);
        }
        GameTime = 0.0f;
    }

    public void Update()
    {
        GameTime += Time.deltaTime;
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

    internal float GetTime()
    {
        return GameTime;
    }
}
