using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //====================================================================================================
    // Enum
    //====================================================================================================
    //public enum DIRECTION_TYPE
    //{
    //    NONE,
    //    FRONT,
    //    RIGHT,
    //    LEFT,
    //    BACK
    //}

    //====================================================================================================
    // 移動パラメタ
    //====================================================================================================
    public float movespeed = 1.0f;
    public Vector2 mapchipSize = new Vector2(16.0f, 16.0f);
    protected bool isMove = false;
    protected DIRECTION_TYPE directionType = DIRECTION_TYPE.NONE;
    protected Vector2 nextMovePos = new Vector2(0.0f, 0.0f);
    protected Dictionary<DIRECTION_TYPE, Vector2> moveVectors = new Dictionary<DIRECTION_TYPE, Vector2>(){
        {DIRECTION_TYPE.NONE, new Vector2(0,0)},
        {DIRECTION_TYPE.FRONT, new Vector2(1,0)},
        {DIRECTION_TYPE.RIGHT, new Vector2(0,1)},
        {DIRECTION_TYPE.LEFT, new Vector2(0,-1)},
        {DIRECTION_TYPE.BACK, new Vector2(-1,0)},
    };

    //====================================================================================================
    // グラフィック
    //====================================================================================================
    protected SpriteRenderer spriteRenderer = null;
    public Dictionary<DIRECTION_TYPE, Sprite> textureMap = new Dictionary<DIRECTION_TYPE, Sprite>();

    //====================================================================================================
    // 関数
    //====================================================================================================

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {

        }
    }

    public void Move(DIRECTION_TYPE dir)
    {
        //　グラフィック更新
        spriteRenderer.sprite = textureMap[dir];
        nextMovePos = mapchipSize * moveVectors[dir];
    }

    public void MoveAuto()
    {
        ////　グラフィック更新
        //spriteRenderer.sprite = textureMap[dir];
        //nextMovePos = mapchipSize * moveVectors[dir];
    }
}
