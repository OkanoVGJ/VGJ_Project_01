using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    //====================================================================================================
    // 変数
    //====================================================================================================
    protected List<Enemy> ControllEnemys = new List<Enemy>();

    //====================================================================================================
    // 初期化
    //====================================================================================================
    void Start()
    {
        ControllEnemys = GameObject.FindObjectsOfType<Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTarget()
    {

    }

    public void StartMoveEvent()
    {
        foreach (var chara in ControllEnemys)
        {
            chara.MoveAuto();
        }
    }
}

