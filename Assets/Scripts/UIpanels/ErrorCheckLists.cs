using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorCheckLists : MonoBehaviour
{
    [Header("CheckLists")]
    [SerializeField] private AxRButton[] m_errorCheckLists;

    [SerializeField] private AxRButton m_btnClose;

    private Action m_actCheckLists;
    public Action ACT_CHECKLISTS { set { m_actCheckLists = value; } }

    private Action m_closeBtn;
    public Action CLOSE_BTN { set { m_closeBtn = value; } }

    [Header("Title")]
    [SerializeField] private Text[] m_errorTitle;

    void Awake()
    {
        m_btnClose.ACT_CLICK = Onclose;
        m_errorCheckLists[0].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[1].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[2].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[3].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[4].ACT_CLICK = OnClickCheckLists;
    }

    public void OnClickCheckLists(AxRButton _button)
    {
        gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void Onclose(AxRButton _button)
    {
        if (m_closeBtn != null)
            m_closeBtn();
        Debug.Log("CheckLists Off!!");
        Destroy(gameObject);
    }
}
