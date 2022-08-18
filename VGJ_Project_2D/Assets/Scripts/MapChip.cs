using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapChip : MonoBehaviour
{
    //ENUM
    public enum MAPCHIP_TYPE
    {
        Type1,
        Type2,
        Type3,
        Type4,
    };

    //変数
    [SerializeField] public List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public MAPCHIP_TYPE mapchipType;


    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if((int)mapchipType < sprites.GetRange())
          spriteRenderer.sprite = sprites[(int)mapchipType];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("aaa");
    }
}
