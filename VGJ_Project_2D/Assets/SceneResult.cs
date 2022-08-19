using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneResult : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private AudioClip DecideAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        var controller = GetComponent<SceneController>();
        controller.PlaySE(DecideAudio);
        controller.TransitionNextScene("Title");
    }
}
