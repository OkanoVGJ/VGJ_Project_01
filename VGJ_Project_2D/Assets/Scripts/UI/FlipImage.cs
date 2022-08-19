using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipImage : MonoBehaviour
{

    [SerializeField]
    private float FlipDuration = 0.5f;

    [SerializeField]
    private Image FlipImage01;

    [SerializeField]
    private Image FlipImage02;

    private float FlipTime = 0.0f;
    private bool IsFlip = true;

    void Start()
    {
        Flip();
        FlipTime = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        FlipTime += Time.deltaTime;
        if(FlipTime >= FlipDuration)
        {
            FlipTime -= FlipDuration;
            Flip();
        }
    }

    void Flip()
    {
        IsFlip = !IsFlip;
        FlipImage01.enabled = (IsFlip);
        FlipImage02.enabled = (!IsFlip);
    }
}
