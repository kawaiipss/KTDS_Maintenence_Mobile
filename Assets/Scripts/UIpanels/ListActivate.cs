using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ListActivate : MonoBehaviour
{
    public void OnClickList()
    {
        this.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }
}
