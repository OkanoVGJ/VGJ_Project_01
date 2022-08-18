using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableObject : MonoBehaviour
{
    //ENUM
    public enum OBJECT_TYPE
    {
       OBJ_1,
       OBJ_2,
       OBJ_3
    };

    //変数
    [SerializeField] public List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public OBJECT_TYPE objType;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[(int)objType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
