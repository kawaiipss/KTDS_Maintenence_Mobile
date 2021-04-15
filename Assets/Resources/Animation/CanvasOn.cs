using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOn : MonoBehaviour
{
    public void ShowCanvas()
    {
        if(!this.gameObject.transform.root.GetComponent<Canvas>().enabled)
            this.gameObject.transform.root.GetComponent<Canvas>().enabled = true;
        print("캔버스 ON!!");
    }
}
