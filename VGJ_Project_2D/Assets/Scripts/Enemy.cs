using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    PlayerController playerController = new PlayerController();

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    void TurnEnd()
    {
        isMove = false;
        if (playerController != null)
            playerController.enableInput = true;
    }

}
