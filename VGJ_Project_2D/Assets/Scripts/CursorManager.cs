using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField]
    Texture2D defaultCursor = null;
    [SerializeField]
    Texture2D interactCursor = null;
    Camera mainCamera;
    RaycastHit hit;
    GameObject targetObject;
    void Start()
    {
        mainCamera = Camera.main;
        SetCursor(true);
    }
    void Update()
    {
        // マウスの左ボタンが押下されたら、対象オブジェクトを調べる処理
        if (Input.GetMouseButtonDown(0))
        {
            LookUpTargetObject();
            return;
        }
        CastRay();
    }
    void OnDisable()
    {
        SetCursor(true);
    }
    // マウスカーソルの位置から「レイ」を飛ばして、何かのコライダーに当たるかどうかをチェック
    void CastRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            targetObject = hit.collider.gameObject;
            SetCursor(false);
        }
        else
        {
            targetObject = null;
            SetCursor(true);
        }
    }
    // 対象のオブジェクトを調べる処理
    void LookUpTargetObject()
    {
        if (targetObject == null)
        {
            return;
        }
        SetCursor(true);
        //targetObject.GetComponent<InteractableObject>().LookUp();
        // このコンポーネントを無効にする（そうしないと調べている最中に別のオブジェクトを調べることができてしまう）
        enabled = false;
    }
    public void SetCursor(bool isDefault)
    {
        if (isDefault)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
