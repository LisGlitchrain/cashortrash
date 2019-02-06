using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushLikeBtn()
    {
        if (uiController.StatusUI == UIController.Status.neutral)
            uiController.SetUIStatus(UIController.Status.like);
    }

    public void PushDislikeBtn()
    {
        if (uiController.StatusUI == UIController.Status.neutral)
            uiController.SetUIStatus(UIController.Status.dislike);
    }
}
