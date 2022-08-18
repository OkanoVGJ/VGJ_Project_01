using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController
{
    //====================================================================================================
    // 変数
    //====================================================================================================
   
    public bool enableInput = false;
    bool isAttackWait = false;

    //TestInputActions testInputActions;

    //EnemyController enemyController = new EnemyController();

    //InputAction moveAction = new InputAction(
    //type: InputActionType.Value,
    // binding: "<Keyboard>/WASD");


    //InputAction fireAction = new InputAction(
    //type: InputActionType.PassThrough,
    //binding: "<Keyboard>/<Fire>");

   

 

    //====================================================================================================
    // 初期化
    //====================================================================================================
    //void Awake()
    //{
      

    //    moveAction.started +=
    //        ctx =>
    //        {
    //            Debug.Log("--- Stick Starts ---");
    //        };
    //    moveAction.performed +=
    //        ctx =>
    //        {
    //            //Debug.Log("Stick Value: " + ctx.ReadValue<Vector2D>());
    //        };
    //    moveAction.canceled +=
    //        ctx =>
    //        {
    //            Debug.Log("# Stick Released");
    //        };

    //    moveAction.Enable();



    //    fireAction.performed +=
    //ctx =>
    //{
    //    Debug.Log("fire");
    //    //var button = (ButtonControl)ctx.control;
    //    //if (button.wasPressedThisFrame)
    //    //    Debug.Log($"Button {ctx.control} was pressed");
    //    //else if (button.wasReleasedThisFrame)
    //    //    Debug.Log($"Button {ctx.control} was released");
    //};

    //    fireAction.Enable();
    //}

    void Start()
    {
        var players = GameObject.FindObjectsOfType<Player>();
        foreach(var p in players)
        {
            controllCharacters.Add(p);
            Debug.Log(p.name);
        }

        //enemyController = GameObject.FindObjectOfType<EnemyController>();
        isMovable = true;
        enableInput = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (UpdateInput())
            enableInput = false;
    }

    bool UpdateInput()
    {
        if (!enableInput) 
        {
            return false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire");
            isAttackWait = true;
            return true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttackWait = false;
            return true;
        }


        DIRECTION_TYPE dir = DIRECTION_TYPE.NONE;

        if (Input.GetKey(KeyCode.W))
        {
            dir = DIRECTION_TYPE.FRONT;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir = DIRECTION_TYPE.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir = DIRECTION_TYPE.BACK;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = DIRECTION_TYPE.RIGHT;
        }

        if(dir != DIRECTION_TYPE.NONE)
        {
            if (isAttackWait)
            {
                AttackMessage(dir);
                return true;
            }
            else
            {
                MoveMessage(dir);
                return true;
            }
        }

        return false;

    }

    void MoveMessage(DIRECTION_TYPE dir)
    {
        Debug.Log("Move");
        isMovable = false;
        foreach (var chara in controllCharacters)
        {
            if(!chara.isGameOver)
             chara.Move(dir);
        }

        //if(enemyController != null)
        //    enemyController.StartAutoMoveEvent();
    }

    void AttackMessage(DIRECTION_TYPE dir)
    {
        Debug.Log("Attack");
        isMovable = false;
        foreach (var chara in controllCharacters)
        {
            if (!chara.isGameOver)
                chara.Move(dir);
        }

        //if (enemyController != null)
        //    enemyController.StartAutoMoveEvent();
    }

    public void GameOverPlayer(int id)
    {
        controllCharacters[id].GameOverEvent();

        foreach(var p in controllCharacters)
        {
            if (!p.isGameOver)
                return;
        }
        GameOverAll();

    }
    

    public void GameOverAll()
    {

    }

    //====================================================================================================
    // 入力イベント
    //====================================================================================================
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    if (!enableInput)
    //    {
    //        return;
    //    }

    //    if (context.started)
    //    {
    //        DIRECTION_TYPE dir = DIRECTION_TYPE.RIGHT;
    //        if (isAttackWait)
    //        {
    //            AttackMessage(dir);
    //        }
    //        else
    //        {
    //            MoveMessage(dir);
    //        }
    //        Debug.Log("Move");
    //    }
    //}

    //public void OnFire(InputAction.CallbackContext context)
    //{
    //    if (!enableInput)
    //    {
    //        return;
    //    }


    //    if (context.canceled)
    //    {

    //        isAttackWait = false;
    //    }

    //    if (!context.performed)
    //    {
    //        return;
    //    }

    //    if (context.started)
    //    {
    //        isAttackWait = true;
    //        Debug.Log("fire");
    //    }


    //}

}
