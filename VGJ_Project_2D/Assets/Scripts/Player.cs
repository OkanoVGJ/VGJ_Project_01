using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   
    EnemyController enemyController = new EnemyController();
    PlayerController playerController = new PlayerController();

    // 攻撃範囲
    //public GameObject attackRange = null;


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


        enemyController = GameObject.FindObjectOfType<EnemyController>();
        playerController = GameObject.FindObjectOfType<PlayerController>();

        //attackRange = this.gameobject.transform.GetChild(0);

        if (goalObject != null)
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

        UpdateAnim();
    }

    protected override void TurnEnd()
    {
        isMove = false;

        //　仮
        //playerController.enableInput = true;

        if (enemyController != null)
        {
            Debug.Log("MoveAutoMessage");
            enemyController.StartAutoMoveEvent();
        }
        else
            playerController.enableInput = true;
    }

  
}
