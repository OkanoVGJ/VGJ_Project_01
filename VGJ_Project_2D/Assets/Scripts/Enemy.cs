using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Vector2 nowPos = new Vector2();
    public Vector2 fieldSize = new Vector2();
    Vector2 goalPos = new Vector2(99,99);
    public GameObject goalObject = null;
    List<Vector2> unmovablePos = new List<Vector2>();
    DIRECTION_TYPE prevDir = DIRECTION_TYPE.NONE;

    PlayerController playerController = new PlayerController();

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        if(goalObject != null)
        {
            goalPos = new Vector2(goalObject.transform.position.x, goalObject.transform.position.y);
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            UpdateMove();
            Debug.Log("UpdateMove");
        }
        if (isAttack)
        {
            UpdateAttack();
        }
    }

    protected override void TurnEnd()
    {
        Debug.Log("TurnEnd");
        isMove = false;

        if (IsGoal())
        {
            GoalEvent();
        }

        if (playerController != null)
            playerController.enableInput = true;
    }

    public override void MoveAuto()
    {
        Debug.Log("MoveAuto");
        nowPos = new Vector2(transform.position.x, transform.position.y);
        DIRECTION_TYPE dir =  SearchForNextDir();
        Move(dir);
        prevDir = dir;
    }

    //====================================================================================================
    // 経路探索
    //====================================================================================================
    DIRECTION_TYPE SearchForNextDir()
    {
        List<DIRECTION_TYPE> DirPriority = new List<DIRECTION_TYPE>();
        Vector2 targetDir = (goalPos - nowPos).normalized;
        
        // 横向き優先
        if(Mathf.Abs(targetDir.x)> Mathf.Abs(targetDir.y))
        {
            //右はいかない
            DirPriority.Add(DIRECTION_TYPE.LEFT);
            if(targetDir.y >= 0)
            {
                DirPriority.Add(DIRECTION_TYPE.FRONT);
                DirPriority.Add(DIRECTION_TYPE.BACK);
            }
            else
            {
                DirPriority.Add(DIRECTION_TYPE.BACK);
                DirPriority.Add(DIRECTION_TYPE.FRONT);
            }
        }
        else
        {
            if (targetDir.y >= 0)
            {
                DirPriority.Add(DIRECTION_TYPE.FRONT);
                //右はいかない
                DirPriority.Add(DIRECTION_TYPE.LEFT);
                DirPriority.Add(DIRECTION_TYPE.BACK);
            }
            else
            {
                DirPriority.Add(DIRECTION_TYPE.BACK);
                //右はいかない
                DirPriority.Add(DIRECTION_TYPE.LEFT);
                DirPriority.Add(DIRECTION_TYPE.FRONT);
            }
           
        }

        foreach(var d in DirPriority)
        {
            if (CheckMovableDir(d))
            {
                return d;
            }
        }

       return DIRECTION_TYPE.NONE;
    }

    bool IsGoal()
    {
        if(nowPos == goalPos)
        {
            return true;
        }
        return false;
    }

    void GoalEvent()
    {

    }
}
