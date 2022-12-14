using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    //====================================================================================================
    // 変数
    //====================================================================================================
    // protected List<Enemy> ControllEnemys = new List<Enemy>();
    //PlayerController playerController = new PlayerController();

    public SceneController sceneController = null;
    public ResultData result;

    //====================================================================================================
    // 初期化
    //====================================================================================================
    void Start()
    {
        var enemys = GameObject.FindObjectsOfType<Enemy>();
        foreach(var e in enemys)
        {
            controllCharacters.Add(e);
        }

        sceneController = GameObject.FindObjectOfType<SceneController>();

        // playerController = GameObject.FindObjectOfType<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        if(controllCharacters.Count == 0)
        {
            ClearGame();
        }
    }

    public void ElapseTurn()
    {
        foreach(var e in controllCharacters)
        {
            e.elapsedTurn = elapseTurn;
            //Debug.Log("経過ターン" + elapseTurn);
            if(!e.isMoveActive && e.activateTurn <= e.elapsedTurn)
            {
                e.isMoveActive = true;
                e.spriteRenderer.enabled = true;
            }
        }
    }

    public int GetTotalEnemy()
    {
        return controllCharacters.Count;
    }

    public void ClearGame()
    {
        Debug.Log("Clear Game");
        result.ClearTime = sceneController.GetTime();
        result.IsClear = true;
        sceneController.TransitionNextScene("Result");
    }
}

