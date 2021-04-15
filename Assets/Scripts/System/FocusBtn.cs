using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FocusBtn : MonoBehaviour, IFocusable
{
    [SerializeField] private Image btn_Name;
    public Image[] btn_Image;

    void Awake()
    {
        btn_Name.gameObject.SetActive(false);
        for(int i = 0; i < btn_Image.Length; i++)
        {
            if(btn_Image[i] != null)
            {
                btn_Image[0].gameObject.SetActive(true);
                btn_Image[1].gameObject.SetActive(false);
            }
        }
    }

    public void OnFocusEnter()
    {
        StartCoroutine(ShowBtnName(0.5f));
        //btn_Image[0].gameObject.SetActive(false);
        //btn_Image[1].gameObject.SetActive(true);
    }

    public void OnFocusExit()
    {
        StopAllCoroutines();
        if (btn_Name.gameObject.activeSelf)
            btn_Name.gameObject.SetActive(false);

        //btn_Image[0].gameObject.SetActive(true);
        //btn_Image[1].gameObject.SetActive(false);
    }

    IEnumerator ShowBtnName(float _time)
    {
        yield return new WaitForSeconds(_time);
        if (!btn_Name.gameObject.activeSelf)
            btn_Name.gameObject.SetActive(true);
        StartCoroutine(OffBtnName(3.0f));
    }

    IEnumerator OffBtnName(float _time)
    {
        yield return new WaitForSeconds(_time);
        if (btn_Name.gameObject.activeSelf)
            btn_Name.gameObject.SetActive(false);
    }
    
}
