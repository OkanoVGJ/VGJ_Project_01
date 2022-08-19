using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiWD_ResultTreasureBox : MonoBehaviour
{
    public enum TreasureType
    {
        None,
        Lost,
        Save
    }

    [SerializeField]
    private Image LostTreasureImage;

    [SerializeField]
    private Image SaveTreasureImage;

    [SerializeField]
    private TreasureType type = TreasureType.Save;

    public void SetTreasureImage(TreasureType treasureType)
    {
        switch (treasureType)
        {
            case TreasureType.None:
                LostTreasureImage.enabled = false;
                SaveTreasureImage.enabled = false;
                break;
            case TreasureType.Lost:
                LostTreasureImage.enabled = true;
                SaveTreasureImage.enabled = false;
                break;
            case TreasureType.Save:
                LostTreasureImage.enabled = false;
                SaveTreasureImage.enabled = true;
                break;
            default:
                LostTreasureImage.enabled = false;
                SaveTreasureImage.enabled = false;
                break;
        }
    }
}
