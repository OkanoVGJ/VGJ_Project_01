using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObject : MonoBehaviour
{
    [SerializeField]
    private GameObject FlipTargetObject;
    [SerializeField]
    private float FlipDuration;

    private float FlipTimer = 0.0f;

    public void Update()
    {
        FlipTimer += Time.deltaTime;
        if(FlipTimer >= FlipDuration)
        {
            FlipTimer -= FlipDuration;
            FlipTargetObject.SetActive(!FlipTargetObject.activeSelf);
        }
    }
}
