using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AxRButton : MonoBehaviour, IFocusable
{

    private Button m_Button;
    private Action<AxRButton> m_actClick;
    public Action<AxRButton> ACT_CLICK { set { m_actClick = value; } }
    private Action<AxRButton> m_actHover;
    public Action<AxRButton> ACT_HOVER { set { m_actHover = value; } }
    private Action<AxRButton> m_actHoverExit;
    public Action<AxRButton> ACT_HOVER_EXIT { set { m_actHoverExit = value; } }


    void Start()
    {
        m_Button = transform.GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickButton);
    }


    public void OnClickButton()
    {
        if (m_actClick != null)
            m_actClick(this);
        //if (this.gameObject.transform.GetChild(0) != null)
        //    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnFocusEnter()
    {
        if (m_actHover != null)
            m_actHover(this);
    }

    public void OnFocusExit()
    {
        if (m_actHoverExit != null)
            m_actHoverExit(this);
    }
}
