using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleFormAPI : MonoBehaviour
{
    [SerializeField] string BASE_URL;
    [SerializeField] string ENTRY_FIELD;
    [SerializeField] string likeValue;
    [SerializeField] string disLikeValue;

    IEnumerator Post(string value)
    {
        WWWForm form = new WWWForm();
        form.AddField(ENTRY_FIELD,value);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void SendLike()
    {
        StartCoroutine(Post(likeValue));
    }

    public void SendDislike()
    {
        StartCoroutine(Post(disLikeValue));
    }


}
