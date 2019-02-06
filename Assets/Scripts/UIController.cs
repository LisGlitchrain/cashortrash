using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject screenHolder;
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject likeScreen;
    [SerializeField] GameObject disLikeScreen;
    [SerializeField] GameObject light1;
    [SerializeField] GameObject light2;
    [SerializeField] GameObject likeBtn;
    [SerializeField] GameObject dislikeBtn;
    [SerializeField] GameObject returnBtn;
    [SerializeField] float targetLightIntensity;
    [SerializeField] float lightSpeed;
    [SerializeField] float lightFloating;
    [SerializeField] Color green;
    [SerializeField] Color red;
    [SerializeField] Color neutral;
    [SerializeField] float fadeSpeed;
    [SerializeField] float floating;
    [SerializeField] float screenSpeed;
    [SerializeField] float screenReturnSpeed;
    [SerializeField] float colorFloating;
    [SerializeField] Status statusUI = Status.neutral;

    public Status StatusUI { get { return statusUI; }}

    public enum Status
    {
        like, dislike, neutral, moveToLike, moveToDislike, lightening, await
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (statusUI)
        {
            case Status.neutral:
                mainScreen.GetComponent<SpriteRenderer>().color = neutral;
                MoveToOtherScreen(mainScreen, floating, screenReturnSpeed);
                Lightening(light1, light2, 0f, lightFloating, lightSpeed);
                returnBtn.SetActive(false);
                likeBtn.SetActive(true);
                dislikeBtn.SetActive(true);
                break;
            case Status.like:
                if (FadeColor(mainScreen, green, fadeSpeed)) statusUI = Status.moveToLike;
                break;
            case Status.dislike:
                if (FadeColor(mainScreen, red, fadeSpeed)) statusUI = Status.moveToDislike;
                break;
            case Status.moveToLike:
                if (MoveToOtherScreen(likeScreen, floating, screenSpeed)) statusUI = Status.lightening;
                break;
            case Status.moveToDislike:
                if (MoveToOtherScreen(disLikeScreen, floating, screenSpeed)) statusUI = Status.lightening;
                break;
            case Status.lightening:
                if (Lightening(light1, light2, targetLightIntensity, lightFloating, lightSpeed)) statusUI = Status.await;
                break;
            case Status.await:
                likeBtn.SetActive(false);
                dislikeBtn.SetActive(false);
                returnBtn.SetActive(true);
                break;
        }
    }

    bool FadeColor(GameObject screen,Color color, float speed)
    {
        SpriteRenderer sr = screen.GetComponent<SpriteRenderer>();
        if (!CompareColors(sr.color, color, colorFloating) )
        {
            sr.color = Color.Lerp(sr.color, color, speed);
            return false;
        }
        return true;
    }

    public void SetUIStatus(Status stat)
    {
        statusUI = stat;
    }

    bool MoveToOtherScreen(GameObject screen, float floating, float screenSpeed)
    {
        if (screenHolder.transform.position.y < -screen.transform.localPosition.y - floating ||
            screenHolder.transform.position.y > -screen.transform.localPosition.y + floating)
        {
            screenHolder.transform.position = new Vector3(
                screenHolder.transform.position.x, 
                Mathf.Lerp(screenHolder.transform.position.y, -screen.transform.localPosition.y, screenSpeed),
                screenHolder.transform.position.z);
            return false;
        }
        return true;
    }

    bool CompareColors(Color col1, Color col2, float floating)
    {
        return col1.r < col2.r + floating &&
                col1.r > col2.r - floating &&
                col1.g < col2.g + floating &&
                col1.g > col2.g - floating &&
                col1.b < col2.b + floating &&
                col1.b > col2.b - floating;
    }

    bool Lightening(GameObject light1, GameObject light2, float targetIntensity, float floating, float lightSpeed)
    {
        if (!CompareLightIntensity(light1, targetIntensity, floating))
        {
            light1.GetComponent<Light>().intensity = Mathf.Lerp(light1.GetComponent<Light>().intensity, targetIntensity, lightSpeed);
            light2.GetComponent<Light>().intensity = Mathf.Lerp(light2.GetComponent<Light>().intensity, targetIntensity, lightSpeed);
            return false;
        }
        return true;
    }

    bool CompareLightIntensity(GameObject light, float targetIntensity, float floating)
    {
        return light.GetComponent<Light>().intensity < targetIntensity + floating &&
            light.GetComponent<Light>().intensity > targetIntensity - floating;
    }


}
