using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResultDataAsset", menuName="ScriptableObjects/CreateResultDataAsset")]
public class ResultData : ScriptableObject
{
    public string StageName = "TestScene";
    public int SaveChestNum = 0;
    public float ClearTime = 0.0f;
    public int ClearTurn = 0;
    public bool IsClear = false;
}
