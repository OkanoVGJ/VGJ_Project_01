using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDataViewer : MonoBehaviour
{

    [SerializeField]
    private UiWD_ResultTreasureBox[] tresureBoxWidget;
    [SerializeField]
    private ResultData resultData;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
