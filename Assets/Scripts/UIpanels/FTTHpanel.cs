using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class FTTHpanel : MonoBehaviour
{
    [SerializeField] private GameObject[] m_innerPanel;
    public Text[] FTTH_General_Data;
    public Text[] FTTH_Port_Data;
    //[SerializeField] private GameObject[] m_detailInfoTitle;

    [Header("Buttons")]
    [SerializeField] private AxRButton m_btnProfileUpdate;
    [SerializeField] private AxRButton m_btnCheck;
    [SerializeField] private AxRButton m_btnUpdate;
    [SerializeField] private AxRButton m_btnFTTH;
    [SerializeField] private AxRButton m_btnQRrecog;
    
    private Action m_profileUpdateBtn;
    public Action PROFILE_UPDATEBTN { set { m_profileUpdateBtn = value; } }
    private Action m_checkBtn;
    public Action CHECK_BTN { set { m_checkBtn = value; } }
    private Action m_updateBtn;
    public Action UPDATE_BTN { set { m_updateBtn = value; } }
    private Action m_FTTHbtn;
    public Action BTN_FTTH { set { m_FTTHbtn = value; } }
    private Action m_QRrecogBTN;
    public Action BTN_QRRECOG { set { m_QRrecogBTN = value; } }
    
    private string JsonString;
    private string ProfileJson;

    public GameObject FTTHmain;
    public GameObject Dashboard;
    public Text chkTime;

    public GameObject UpdateMsg;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_audioClip;
    

    private void Awake()
    {
        JsonString = Resources.Load<TextAsset>("Json/FTTH_General").text;
        ProfileJson = Resources.Load<TextAsset>("Json/FTTH_PortProfile_pre").text;
        GeneralFTTHdata();
        FTTHportData();
        
        m_btnQRrecog.ACT_CLICK = Onclose;
        m_btnProfileUpdate.ACT_CLICK = OnClickProfileBtn;
        m_btnCheck.ACT_CLICK = OnClickCheck;
        m_btnUpdate.ACT_CLICK = OnClickUpdate;
        m_btnFTTH.ACT_CLICK = OnClickFTTH;

        UpdateMsg.SetActive(false);

        if (m_audioSource == null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
            for (int i = 0; i < m_audioClip.Length; i++)
            {
                m_audioSource.clip = m_audioClip[i];
            }
        }
    }

    void Start()
    {
        SetManualPanel();
    }

    void SetManualPanel()
    {
        chkTime.text = DateTime.Now.ToString("yyyy-MM-dd tt:hh:mm");
        FTTHmain.SetActive(false);
        Dashboard.SetActive(true);
        m_innerPanel[0].SetActive(true);
        m_innerPanel[1].SetActive(false);
    }

    public void GeneralFTTHdata()
    {
        JsonData sampleData = JsonMapper.ToObject(JsonString);
        GetSampleData(sampleData);
    }

    public void FTTHportData()
    {
        JsonData profileSampleData = JsonMapper.ToObject(ProfileJson);
        GetPortData(profileSampleData);
    }

    void GetSampleData(JsonData name)
    {
        for(int i = 0; i < name.Count; i++)
        {
            FTTH_General_Data[0].text = name[i]["nescode"].ToString();
            FTTH_General_Data[1].text = name[i]["neAlias"].ToString();
            FTTH_General_Data[2].text = name[i]["neUsgDesc"].ToString();
            FTTH_General_Data[3].text = name[i]["officeName"].ToString();
            FTTH_General_Data[4].text = name[i]["instdt"].ToString();
            FTTH_General_Data[5].text = name[i]["muxPOffice1G"].ToString();
            FTTH_General_Data[6].text = name[i]["muxPNe1G"].ToString();
            FTTH_General_Data[7].text = name[i]["muxPPort1G"].ToString();
            FTTH_General_Data[8].text = name[i]["muxPOffice10G"].ToString();
            FTTH_General_Data[9].text = name[i]["muxPNe10G"].ToString();
            FTTH_General_Data[10].text = name[i]["muxPPort10G"].ToString();
            FTTH_General_Data[11].text = name[i]["addr"].ToString();
        }
    }

    void GetPortData(JsonData name)
    {
        for(int i =0; i<name.Count; i++)
        {
            FTTH_Port_Data[0].text = name[i]["portList"]["portalias"][1].ToString();
            FTTH_Port_Data[1].text = name[i]["portList"]["svctypecodedesc"][1].ToString();
            FTTH_Port_Data[2].text = DateTime.Now.ToString();
            FTTH_Port_Data[3].text = name[i]["portList"]["chksvcprdname"].ToString();
            if (name[i]["portList"]["chkbtnyn"][1].ToString() == "Y")
            {
                m_btnCheck.gameObject.SetActive(true);
            }
            else
            {
                m_btnCheck.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickProfileBtn(AxRButton _button)
    {
        if(m_innerPanel[0].activeSelf && !m_innerPanel[1].activeSelf)
        {
            m_innerPanel[0].SetActive(false);
            m_innerPanel[1].SetActive(true);
        }
        m_audioSource.clip = m_audioClip[0];
        m_audioSource.Play();
        //FTTHportData();
    }

    public void Onclose(AxRButton _button)
    {
        if (m_QRrecogBTN != null)
            m_QRrecogBTN();
        //Debug.Log("NeOSS Off!!");
        Destroy(gameObject);

        //MainSystem.INSTANCE.fSetQRCodeCheckTime(MainSystem.mQRCodeSkipTimer);   //*LKH* - 20190813, QR코드 재인식 문제 수정
        MainSystem.INSTANCE.ReRecogQR();
    }

    public void OnClickCheck(AxRButton _button)
    {
        if (m_checkBtn != null)
            m_checkBtn();
        if (!m_btnUpdate.gameObject.activeSelf)
            m_btnUpdate.gameObject.SetActive(true);
        m_audioSource.clip = m_audioClip[0];
        m_audioSource.Play();
    }

    public void OnClickUpdate(AxRButton _button)
    {
        if (m_updateBtn != null)
            m_updateBtn();

        if(!UpdateMsg.activeSelf)
            UpdateMsg.SetActive(true);

        m_audioSource.clip = m_audioClip[1];
        m_audioSource.Play();
    }

    public void OnClickFTTH(AxRButton _button)
    {
        Dashboard.SetActive(false);
        FTTHmain.SetActive(true);
    }
}
