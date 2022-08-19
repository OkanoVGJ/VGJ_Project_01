using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_StageSelect : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI StageText;
    [SerializeField]
    private Image RightCursor;
    [SerializeField]
    private Image LeftCursor;

    [SerializeField]
    StageSelectData[] StageList;

    [SerializeField]
    SceneController sceneController;

    [SerializeField]
    EventSystem eventSystem;


    int CurrentSelectStageIndex = 0;
    bool isTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        SetStageData();
    }

    // Update is called once per frame
    void Update()
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }

    public void OnMove(BaseEventData eventData)
    {
        switch ((eventData as AxisEventData).moveDir)
        {
            case MoveDirection.Left:
                MoveSelectStageIndex(-1);
                break;
            case MoveDirection.Right:
                MoveSelectStageIndex(1);
                break;
            default:
                break;
        }
        SetStageData();
    }


    private void SetStageData()
    {
        LeftCursor.enabled = CurrentSelectStageIndex > 0;
        RightCursor.enabled = CurrentSelectStageIndex < StageList.Length - 1;

        StageText.text = StageList[CurrentSelectStageIndex].StageName;
    }

    public void DecideStage(BaseEventData eventData)
    {
        if (!isTransition)
        {
            sceneController.TransitionNextScene(StageList[CurrentSelectStageIndex].StageSceneName);
            isTransition = true;
        }
    }

    public void MoveSelectStageIndex(int direction)
    {
        CurrentSelectStageIndex = Mathf.Clamp(CurrentSelectStageIndex + direction, 0, StageList.Length - 1);
    }


}
