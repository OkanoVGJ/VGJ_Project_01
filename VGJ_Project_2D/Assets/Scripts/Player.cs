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
