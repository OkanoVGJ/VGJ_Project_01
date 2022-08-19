using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    //====================================================================================================
    // 変数
    //====================================================================================================
    // protected List<Enemy> ControllEnemys = new List<Enemy>();
    //PlayerController playerController = new PlayerController();

    //====================================================================================================
    // 初期化
    //====================================================================================================
    void Start()
    {
        var enemys = GameObject.FindObjectsOfType<Enemy>();
        foreach(var e in enemys)
        {
            controllCharacters.Add(e);
        }

       // playerController = GameObject.FindObjectOfType<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   
}

