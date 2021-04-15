using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using LitJson;


public class URLdataRead : MonoBehaviour
{
    public Text txt;

    private void Start()
    {
        //GET("http://14.63.248.191/WS/SO/MR/api/NE/CBCJ09423");
        GET("http://14.63.248.191/WS/SO/MR/api/NE/F33774419");
        //GET("http://14.63.248.191/WS/SO/MR/api/Port/F33774419");
        //GET("http://14.63.248.191/WS/SO/MR/api/Port/F33774419/003");
    }
    public WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        //ProcessPlayer(www.text);
        return www;
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            Debug.Log("WWW OK!!: " + www.text);
            ProcessPlayer(www.text);
        }
        else
            Debug.Log("WWW error!: " + www.error);
    }

    private void ProcessPlayer(string jsonString)
    {
        JsonData jsonPlayer = JsonMapper.ToObject(jsonString);
        txt.text = jsonPlayer["muxPOffice1G"].ToString();
        //Debug.Log(jsonPlayer["neType"]);
    }
}
