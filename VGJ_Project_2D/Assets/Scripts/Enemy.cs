using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
   
    //public Vector2 fieldSize = new Vector2();
    
   
    List<Vector2> unmovablePos = new List<Vector2>();
    DIRECTION_TYPE prevDir = DIRECTION_TYPE.NONE;

    PlayerController playerController = new PlayerController();
    EnemyController enemyController = new EnemyController();


    //耐久
    int hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        textureMap1 = new Dictionary<DIRECTION_TYPE, Sprite>() {
            { DIRECTION_TYPE.NONE, animations1[0] },
            { DIRECTION_TYPE.FRONT, animations1[1] },
            { DIRECTION_TYPE.RIGHT, animations1[2] },
            { DIRECTION_TYPE.LEFT, animations1[3] },
            { DIRECTION_TYPE.BACK, animations1[4] },
        };

        textureMap2 = new Dictionary<DIRECTION_TYPE, Sprite>() {
            { DIRECTION_TYPE.NONE, animations2[0] },
            { DIRECTION_TYPE.FRONT, animations2[1] },
            { DIRECTION_TYPE.RIGHT, animations2[2] },
            { DIRECTION_TYPE.LEFT, animations2[3] },
            { DIRECTION_TYPE.BACK, animations2[4] },
        };


        playerController = GameObject.FindObjectOfType<PlayerController>();
        enemyController = GameObject.FindObjectOfType<EnemyController>();
        if (goalObject != null)
        {
            goalPos = new Vector2(goalObject.transform.position.x, goalObject.transform.position.y);
        }

        if(activateTurn > 0)
        {
            isMoveActive = false;
            spriteRenderer.enabled = false;
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

        UpdateAnim();
    }

    protected override void TurnEnd()
    {
        if (isAttacked)
        {
            hp--;
            spriteRenderer.color = new Color(1, 0.25f, 0.25f);
            if(hp <= 0)
            {
                if (enemyController)
                    enemyController.ReqRemove(this);
                Destroy(this.gameObject);

                if(enemyController && enemyController.GetTotalEnemy() <= 0)
                {
                    enemyController.ClearGame();
                }
            }
        }
        Debug.Log("TurnEnd");
        isMove = false;
        isAttacked = false;
        knockbackDir = DIRECTION_TYPE.NONE;

       

        if (IsGoal())
        {
            GoalEvent();
        }

        if (enemyController)
        {
            enemyController.ElapseTurn();
        }

        if (playerController != null)
        {
            playerController.enableInput = true;
        }


    }

    public override void MoveAuto()
    {
        if (!isMoveActive)
            return;

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

        float moveRate = 1.0f;
        if (hitPlayerWait)
            moveRate = 0;

        Move(dir, moveRate);

        if (isAttacked)
        {
            switch (dir)
            {
                case DIRECTION_TYPE.FRONT:
                    directionType = DIRECTION_TYPE.BACK;
                    break;
                case DIRECTION_TYPE.BACK:
                    directionType = DIRECTION_TYPE.FRONT;
                    break;
                        case DIRECTION_TYPE.RIGHT:
                    directionType = DIRECTION_TYPE.LEFT;
                    break;
                case DIRECTION_TYPE.LEFT:
                    directionType = DIRECTION_TYPE.RIGHT;
                    break;
                default:
                    break;
            }
            
        }
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
            if (CheckMovableDirEx(d))
            {
                return d;
            }
        }

       return DIRECTION_TYPE.NONE;
    }

    bool hitPlayerWait = false;
    public bool CheckMovableDirEx(DIRECTION_TYPE dir)
    {
        hitPlayerWait = false;

        Vector2 targetDir = moveVectors[dir];
        Vector2 nowPos = this.transform.position;
        float maxDistance = mapchipSize.x;


        //RaycastHit2D hit = Physics2D.Raycast(nowPos, targetDir, maxDistance);

        foreach (RaycastHit2D hit in Physics2D.RaycastAll(nowPos, targetDir, maxDistance))
        {
            if (hit && hit.collider.gameObject.name != this.gameObject.name)
            {
                Debug.Log("衝突" + hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetType() != typeof(Player))
                    return false;
                else
                    hitPlayerWait = true;
            }
        }

        return true;
    }

    bool IsGoal()
    {
        if((goalPos - nowPos).magnitude < mapchipSize.x)
        {
            return true;
        }
        return false;
    }

    void GoalEvent()
    {
        if (playerController != null)
        {
            playerController.GameOverPlayer(this.ID);
        }
        enemyController.ReqRemove(this as Character);
        gameObject.SetActive(false);
        goalObject.SetActive(false);
    }

  
}
