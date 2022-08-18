using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    EnemyController enemyController = new EnemyController();
    PlayerController playerController = new PlayerController();

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyController = GameObject.FindObjectOfType<EnemyController>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
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
        playerController.enableInput = true;
        if (enemyController != null)
            enemyController.StartAutoMoveEvent();
    }
}
