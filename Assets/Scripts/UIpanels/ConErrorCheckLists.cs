using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConErrorCheckLists : MonoBehaviour
{
    [Header("CheckLists")]
    [SerializeField] private AxRButton[] m_errorCheckLists;

    [SerializeField] private AxRButton m_btnBack;

    private Action m_actCheckLists;
    public Action ACT_CHECKLISTS { set { m_actCheckLists = value; } }

    private Action m_backBtn;
    public Action BACK_BTN { set { m_backBtn = value; } }

    void Awake()
    {
        m_btnBack.ACT_CLICK = OnBack;
        m_errorCheckLists[0].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[1].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[2].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[3].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[4].ACT_CLICK = OnClickCheckLists;
        m_errorCheckLists[5].ACT_CLICK = OnClickCheckLists;
    }

    public void OnClickCheckLists(AxRButton _button)
    {
        gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void OnBack(AxRButton _button)
    {
        if (m_backBtn != null)
            m_backBtn();
        MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "CheckLists", null);
        Destroy(this.gameObject);
    }
}
