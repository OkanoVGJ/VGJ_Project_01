using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NowLoading : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private float AnimationDuration;

    [SerializeField]
    private string[] NowLoadingText;

    int ListIndex = 0;

    private float AnimTimer = 0.0f;

    private bool IsLoading = true;
    
    void Start()
    {
        AnimTimer = 0.0f;
        text.text = NowLoadingText[ListIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLoading)
        {
            UpdateLoadingText();
        }
    }

    void UpdateLoadingText()
    {
        AnimTimer += Time.deltaTime;

        if (AnimTimer >= AnimationDuration)
        {
            AnimTimer -= AnimationDuration;
            ListIndex = (ListIndex + 1) % NowLoadingText.Length;
            text.text = NowLoadingText[ListIndex];
        }
    }
}
