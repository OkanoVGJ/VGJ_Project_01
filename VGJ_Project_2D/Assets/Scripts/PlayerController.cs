using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    //====================================================================================================
    // 変数
    //====================================================================================================
    protected List<Player> ControllPlayers = new List<Player>();
    EnemyController enemyController = new EnemyController();

    //====================================================================================================
    // 初期化
    //====================================================================================================
    void Start()
    {
        ControllPlayers = GameObject.FindObjectsOfType<Player>();
        enemyController = GameObject.FindObjectOfType<enemyController>();
        isMovable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovable)
        {
            Input();
        }
    }

    void Input()
    {
        if (Keyboard.current.FindKeyOnCurrenKeyboardLayout("A")){
            MoveMessage(DIRECTION_TYPE.LEFT);
            return;
        }
        if (Keyboard.current.FindKeyOnCurrenKeyboardLayout("W")){
            MoveMessage(DIRECTION_TYPE.FRONT);
            return;
        }
        if (Keyboard.current.FindKeyOnCurrenKeyboardLayout("S")){
            MoveMessage(DIRECTION_TYPE.BACK);
            return;
        }
        if (Keyboard.current.FindKeyOnCurrenKeyboardLayout("D")){
            MoveMessage(DIRECTION_TYPE.RIGHT);
            return;
        }

    }

    void MoveMessage(DIRECTION_TYPE dir)
    {
        isMovable = false;
        foreach (var chara in ControllPlayers)
        {
            chara.Move(dir);
        }

        EnemyController.StartMoveEvetn();
    }
}
