using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Character : MonoBehaviour
{


    //====================================================================================================
    // 移動パラメタ
    //====================================================================================================
    public int ID = 0;
    float actionTimer = 0.0f;
    public float moveTime = 2.0f;
    public float attackTime = 2.0f;
    public Vector2 mapchipSize = new Vector2(0.8f, 0.8f);

    Vector2 startPos = new Vector2();
    public Vector2 nowPos = new Vector2();

    protected bool isMove = false;
    protected bool isAttack = false;
    public bool isGameOver = false;

    protected DIRECTION_TYPE directionType = DIRECTION_TYPE.NONE;
    protected Vector2 nextMovePos = new Vector2(0.0f, 0.0f);
    protected Vector2 prevPos = new Vector2(0, 0);
    protected Dictionary<DIRECTION_TYPE, Vector2> moveVectors = new Dictionary<DIRECTION_TYPE, Vector2>(){
        {DIRECTION_TYPE.NONE, new Vector2(0,0)},
        {DIRECTION_TYPE.FRONT, new Vector2(0,1)},
        {DIRECTION_TYPE.RIGHT, new Vector2(1,0)},
        {DIRECTION_TYPE.LEFT, new Vector2(-1,0)},
        {DIRECTION_TYPE.BACK, new Vector2(0,-1)},
    };

    public bool isAttacked = false;
    public DIRECTION_TYPE knockbackDir = DIRECTION_TYPE.NONE;

    //====================================================================================================
    // グラフィック
    //====================================================================================================
    protected SpriteRenderer spriteRenderer = null;
    public Dictionary<DIRECTION_TYPE, Sprite> textureMap = new Dictionary<DIRECTION_TYPE, Sprite>();
   
    // 攻撃範囲
    public GameObject attackRange = null;
    public GameObject attackEffect = null;
    private GameObject activeEffect = null;

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
            UpdateMove();
           
        }
        if (isAttack)
        {
            UpdateAttack();
        }
    }

    public void Move(DIRECTION_TYPE dir)
    {
        //　グラフィック更新
        //spriteRenderer.sprite = textureMap[dir];

        actionTimer = 0;
        prevPos = transform.position;

        if (CheckMovableDir(dir))
        {
           
            nextMovePos = mapchipSize * moveVectors[dir];
            //Debug.Log("NextMove = " + nextMovePos);
           
        }
        else
        {

            nextMovePos = new Vector2(0, 0);
            //Debug.Log("NextMove = " + nextMovePos);
           
        }
        isMove = true;
        // moveFlow.OnNext(0);
        //Debug.Log("StartMove");

    }

    public virtual void MoveAuto()
    {
        ////　グラフィック更新
        //spriteRenderer.sprite = textureMap[dir];
        //nextMovePos = mapchipSize * moveVectors[dir];
    }

    public bool CheckMovableDir(DIRECTION_TYPE dir)
    {
        Vector2 targetDir = moveVectors[dir];
        Vector2 nowPos = this.transform.position;
        float maxDistance = mapchipSize.x;

 
        //RaycastHit2D hit = Physics2D.Raycast(nowPos, targetDir, maxDistance);

        foreach (RaycastHit2D hit in Physics2D.RaycastAll(nowPos, targetDir, maxDistance))
        {
            if (hit && hit.collider.gameObject.name != this.gameObject.name)
            {
                Debug.Log("衝突" + hit.collider.gameObject.name);
                return false;
            }
        }
       
        return true;
    }

    public void Attack(DIRECTION_TYPE dir)
    {
        actionTimer = 0;
        prevPos = transform.position;
        isAttack = true;

        Vector2 v = CheckAttackDir(dir);
        if(attackEffect != null)
            activeEffect = Instantiate(attackEffect, v, Quaternion.identity);
    }

    public Vector2 CheckAttackDir(DIRECTION_TYPE dir)
    {
        Vector2 targetDir = moveVectors[dir];
        Vector2 nowPos = this.transform.position;
        float maxDistance = mapchipSize.x;

        Vector2 effectPos = nowPos + targetDir * 2;


        foreach (RaycastHit2D hit in Physics2D.RaycastAll(nowPos, targetDir, maxDistance * 2))
        {
            if (hit && hit.collider.gameObject.GetComponent<Enemy>() != null)
            {
                Debug.Log("Hit Enemy");
                Character e = hit.collider.gameObject.GetComponent<Character>();
                e.isAttacked = true;
                e.knockbackDir = dir;
                effectPos = new Vector2(e.transform.position.x, e.transform.position.y);

                return effectPos;
            }
        }
        return effectPos;
    }

    //====================================================================================================
    // コルーチン(的なものになった）
    //====================================================================================================
    public void UpdateMove()
    {
        actionTimer += Time.deltaTime;
        //Debug.Log(actionTimer);
        if (actionTimer > moveTime)
        {
            //Debug.Log(moveTime);
            actionTimer = moveTime;
        }

        Vector2 targetPos = prevPos + nextMovePos * actionTimer/moveTime;
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        //Debug.Log(transform.position);

        if (actionTimer >= moveTime)
        {
            //Debug.Log(actionTimer);
            TurnEnd();
            isMove = false;
            //Debug.Log("EndMove");
        }
    }

    public void UpdateAttack()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer > attackTime)
        {
            TurnEnd();
            isAttack = false;
            if(activeEffect != null)
                Destroy(activeEffect);
        }
    }

    protected virtual void TurnEnd()
    {
        isMove = false;
    }

    public void GameOverEvent()
    {
        isGameOver = true;
        transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
    }

    public void AttackRangeActive(bool enable)
    {
        if (attackRange != null)
            attackRange.SetActive(enable);
    }
}
