using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidCheck : MonoBehaviour
{
    public Camera mainCam;
    public Text camPos;
    public Text camRot;

    // Update is called once per frame
    void Update()
    {
        camPos.text = "CamPos : " + mainCam.gameObject.transform.position.ToString();
        camRot.text = "CamRot : " + mainCam.gameObject.transform.localEulerAngles.ToString();
    }
}
