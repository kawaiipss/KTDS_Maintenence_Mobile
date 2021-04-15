using System;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class ManualPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private AxRButton m_btnClose;
    [SerializeField] private AxRButton m_btnNext;
    [SerializeField] private AxRButton m_btnPrev;

    [Header("Text")]
    [SerializeField] private Text m_pageNum;
    [SerializeField] private Text[] m_manualInfo;

    private Action m_closeBtn;
    public Action CLOSE_BTN { set { m_closeBtn = value; } }
    private Action m_nextBtn;
    public Action NEXT_BTN { set { m_nextBtn = value; } }
    private Action m_prevBtn;
    public Action PREV_BTN { set { m_prevBtn = value; } }    

    private int num_of_page;
    private int cur_page = 1;

    private void Awake()
    {
        m_btnClose.ACT_CLICK = Onclose;
        m_btnNext.ACT_CLICK = OnClickNext;
        m_btnPrev.ACT_CLICK = OnClickPrev;
        num_of_page = 3;
    }

    void Start()
    {
        SetManualPanel();
    }

    void SetManualPanel()
    {
        m_pageNum.text = (cur_page + "/" + num_of_page);
        for(int i = 0; i<m_manualInfo.Length; i++)
        {
            m_manualInfo[i].GetComponent<Text>().enabled = false;
            if(i==cur_page-1)
                m_manualInfo[i].GetComponent<Text>().enabled = true;
        }
        //m_manualInfo.text = ("Manual #" + cur_page);
    }

    public void OnClickNext(AxRButton _button)
    {
        if (m_nextBtn != null)
            m_nextBtn();
        if (cur_page < num_of_page)
            cur_page += 1;
        
        m_pageNum.text = (cur_page + "/" + num_of_page);
        for (int i = 0; i < m_manualInfo.Length; i++)
        {
            m_manualInfo[i].GetComponent<Text>().enabled = false;
            if (i == cur_page-1)
                m_manualInfo[i].GetComponent<Text>().enabled = true;
        }
        //m_manualInfo.text = ("Manual #" + cur_page);
        Debug.Log("Next!!");
        Debug.Log("Cur_page = " + cur_page);
    }

    public void OnClickPrev(AxRButton _button)
    {
        if (m_prevBtn != null)
            m_prevBtn();
        if (cur_page > 1)
            cur_page -= 1;
        m_pageNum.text = (cur_page + "/" + num_of_page);
        for (int i = 0; i < m_manualInfo.Length; i++)
        {
            m_manualInfo[i].GetComponent<Text>().enabled = false;
            if (i == cur_page-1)
                m_manualInfo[i].GetComponent<Text>().enabled = true;
        }
        //m_manualInfo.text = ("Manual #" + cur_page);
        Debug.Log("Prev!!");
        Debug.Log("Cur_page = " + cur_page);
    }

    public void Onclose(AxRButton _button)
    {
        //if (m_closeBtn != null)
        //    m_closeBtn();
        //Debug.Log("Manual Off!!");

        //ControlPanal m_controlPanel = GameObject.Find("Control_Panel").GetComponent<ControlPanal>();
        //if (!m_controlPanel.btn_ManualPos.gameObject.activeSelf)
        //    m_controlPanel.btn_ManualPos.gameObject.SetActive(true);

        //transform.position = new Vector3(1.5f, -0.364f, 1.0f);
        ////Destroy(this.gameObject);
    }
}
