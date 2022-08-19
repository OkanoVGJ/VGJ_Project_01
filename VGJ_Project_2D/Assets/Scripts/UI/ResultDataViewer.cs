using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image ClearImage;

    [SerializeField]
    private UiWD_ResultTreasureBox[] tresureBoxWidget;
    [SerializeField]
    private ResultData resultData;

    [SerializeField]
    private TMPro.TextMeshProUGUI ClearTimeText;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tresureBoxWidget.Length; i++)
        {
            if (i < resultData.SaveChestNum) {
                tresureBoxWidget[i].SetTreasureImage(UiWD_ResultTreasureBox.TreasureType.Save);
            } 
            else if( i < resultData.AllChestNum)
            {
                tresureBoxWidget[i].SetTreasureImage(UiWD_ResultTreasureBox.TreasureType.Lost);
            }
            else
            {
                tresureBoxWidget[i].SetTreasureImage(UiWD_ResultTreasureBox.TreasureType.None);

            }
        }
        ClearTimeText.text = "クリアタイム：" + ((int)resultData.ClearTime).ToString() + " びょう";
        ClearImage.enabled = resultData.IsClear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
