using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DIRECTION_TYPE
{
    NONE,
    FRONT,
    RIGHT,
    LEFT,
    BACK
}

public class CharacterController : MonoBehaviour
{
    //====================================================================================================
    // 変数
    //====================================================================================================
    protected List<Character> controllCharacters = new List<Character>();
    protected bool isMovable;


    //====================================================================================================
    // 初期化
    //====================================================================================================
    void Start()
    {
        
    }






    // Update is called once per frame
    void Update()
    {
        //if (isMove)
        //{

        //}
    }

    //public void Move(DIRECTION_TYPE dir)
    //{
    //    ////　グラフィック更新
    //    //spriteRenderer.sprite = textureMap[dir];
    //    //nextMovePos = mapchipSize * moveVectors[dir];
    //}

    //public void MoveAuto()
    //{
    //    ////　グラフィック更新
    //    //spriteRenderer.sprite = textureMap[dir];
    //    //nextMovePos = mapchipSize * moveVectors[dir];
    //}

    public void StartAutoMoveEvent()
    {
        foreach (var chara in controllCharacters)
        {
            chara.MoveAuto();
        }
    }

}
