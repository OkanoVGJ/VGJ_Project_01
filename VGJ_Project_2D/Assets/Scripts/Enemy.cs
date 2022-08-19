using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
   
    //public Vector2 fieldSize = new Vector2();
    
   
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
        isAttacked = false;
        knockbackDir = DIRECTION_TYPE.NONE;

        if (IsGoal())
        {
            GoalEvent();
        }

        if (playerController != null)
            playerController.enableInput = true;
    }

    public override void MoveAuto()
    {
        //Debug.Log("MoveAuto");
        DIRECTION_TYPE dir = DIRECTION_TYPE.NONE;
        nowPos = new Vector2(transform.position.x, transform.position.y);
        if (!isAttacked)
        {
            dir = SearchForNextDir();
        }
        else
        {
            dir = knockbackDir;
           
        }
        Move(dir);
        prevDir = dir;
        //isAttacked = false;
        //knockbackDir = DIRECTION_TYPE.NONE;
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
