using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanal : MonoBehaviour
{
    private static ControlPanal instance;
    public static ControlPanal INSTANCE { get { return instance; } }

    [Header("Buttons")]
    [SerializeField] private AxRButton btn_QR;
    [SerializeField] private AxRButton btn_NeOSS;
    [SerializeField] private AxRButton btn_CheckLists;
    public AxRButton CHECKLISTS_BTN { get { return btn_CheckLists; } }
    [SerializeField] private AxRButton btn_RemoteSupporting;
    public AxRButton REMORTESUPPORTING_BTN { get { return btn_RemoteSupporting; } }
    [SerializeField] private AxRButton btn_PartsInfo;
    [SerializeField] private AxRButton btn_Manual;

    public AxRButton[] btn_NeOSSpos;
    public AxRButton[] btn_CheckListsPos;
    public AxRButton[] btn_PartsInfoPos;
    public AxRButton[] btn_ManualPos;

    private Action m_actQR;
    public Action ACT_QR { set { m_actQR = value; } }
    private Action m_actNeOSS;
    public Action ACT_NEOSS { set { m_actNeOSS = value; } }
    private Action m_actCheckLists;
    public Action ACT_CHECKLISTS { set { m_actCheckLists = value; } }
    private Action m_actRemoteSupporting;
    public Action ACT_REMOTESUPPORTING { set { m_actRemoteSupporting = value; } }
    private Action m_actPartsInfo;
    public Action ACT_PARTSINFO { set { m_actPartsInfo = value; } }
    private Action m_actManual;
    public Action ACT_MANUAL { set { m_actManual = value; } }
    private Action m_actNeOSSpos;
    public Action ACT_NEOSSPOS { set { m_actNeOSSpos = value; } }
    private Action m_actCheckListsPos;
    public Action ACT_CHECKLISTSPOS { set { m_actCheckListsPos = value; } }
    private Action m_actPartsInfoPos;
    public Action ACT_PARTSINFOPOS { set { m_actPartsInfoPos = value; } }
    private Action m_actManualPos;
    public Action ACT_MANUALPOS { set { m_actManualPos = value; } }

    private GameObject m_QRpanel;
    private GameObject m_NeOSSpanel;
    private GameObject m_CheckListspanel;
    public GameObject CHECKLISTS_PANEL { set { m_CheckListspanel = value; } get { return m_CheckListspanel; } }
    private GameObject m_RemoteSupportingpanel;
    public GameObject REMOTESUPPORTING_PANEL { set { m_RemoteSupportingpanel = value; } get { return m_RemoteSupportingpanel; } }
    private GameObject m_PartsInfopanel;
    private GameObject m_Manualpanel;

    private int isNeOSS;
    private int isCheckLists;
    public int ISCHECKLISTS { set { isCheckLists = value; } }
    public int isRemoteSup;
    public int ISREMOTESUP { set { isRemoteSup = value; } }
    private int isPartsInfo;
    private int isManual;
    public bool isBase;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    public Vector3 startPos;

    private Vector3 NeOSS_posInit;
    private Vector3 ChkLists_posInit;
    private Vector3 PartsInfo_posInit;
    private Vector3 Manual_posInit;

    public GameObject NeOSS_pos;
    public GameObject ChkLists_pos;
    public GameObject PartsInfo_pos;
    public GameObject Manual_pos;
    public GameObject Main_pos;

    private VuforiaController m_vufoCon;

    public GameObject remote_Warning;

    //public GameObject Panel_root;
    //public GameObject PANEL_ROOT { set { Panel_root = value; } get { return Panel_root; } }

    private void Awake()
    {
        btn_QR.ACT_CLICK = OnClickQR;
        btn_NeOSS.ACT_CLICK = OnClickNeOSS;
        btn_CheckLists.ACT_CLICK = OnClickCheckLists;
        btn_RemoteSupporting.ACT_CLICK = OnClickRemoteSupporting;
        btn_PartsInfo.ACT_CLICK = OnClickPartsInfo;
        btn_Manual.ACT_CLICK = OnClickManual;
        btn_NeOSSpos[0].ACT_CLICK = OnClickNeossPos;
        btn_NeOSSpos[1].ACT_CLICK = OnClickNeOSSReturnPos;
        
        btn_CheckListsPos[0].ACT_CLICK = OnClickCheckListsPos;
        btn_CheckListsPos[1].ACT_CLICK = OnClickCheckListsReturnPos;

        btn_PartsInfoPos[0].ACT_CLICK = OnClickPartsInfoPos;
        btn_PartsInfoPos[1].ACT_CLICK = OnClickPartsInfoReturnPos;
        
        btn_ManualPos[0].ACT_CLICK = OnClickManualPos;
        btn_ManualPos[1].ACT_CLICK = OnClickManualReturnPos;

        isNeOSS = 0;
        isCheckLists = 0;
        isRemoteSup = 0;
        isPartsInfo = 0;
        isManual = 0;
        isBase = false;

        if (m_audioSource == null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        //startPos = gameObject.transform.position;
        //gameObject.transform.localScale *= 1.1f;

        //btn_NeOSS.transform.GetChild(0).gameObject.SetActive(false);
        //btn_NeOSS.transform.GetChild(1).gameObject.SetActive(true);
        btn_CheckLists.transform.GetChild(0).gameObject.SetActive(false);
        btn_CheckLists.transform.GetChild(1).gameObject.SetActive(true);

        btn_NeOSSpos[0].gameObject.SetActive(false);
        btn_NeOSSpos[1].gameObject.SetActive(false);

        btn_CheckListsPos[0].gameObject.SetActive(false);
        btn_CheckListsPos[1].gameObject.SetActive(false);

        btn_PartsInfoPos[0].gameObject.SetActive(false);
        btn_PartsInfoPos[1].gameObject.SetActive(false);

        btn_ManualPos[0].gameObject.SetActive(false);
        btn_ManualPos[1].gameObject.SetActive(false);

        m_vufoCon = GameObject.Find("VuforiaController").GetComponent<VuforiaController>();

        remote_Warning.SetActive(false);

        //if (gameObject.transform.parent != null)
        //{
        //    gameObject.transform.parent = null;
        //    gameObject.transform.localPosition = (startPos + MainSystem.INSTANCE.CAMERA_MAIN.transform.position) * 0.03f;
        //    //gameObject.transform.localPosition = new Vector3((startPos.x + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x) * 0.015f, (startPos.y + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.y) * 0.015f, (startPos.z + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z) * 0.015f);
        //    gameObject.transform.localScale *= 0.11f;
        //}
        //gameObject.transform.position = (startPos - MainSystem.INSTANCE.CAMERA_MAIN.transform.position) * 0.01f;
        //gameObject.transform.position = new Vector3((startPos.x - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x) * 0.02f, (startPos.y - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.y) * 0.01f, (startPos.z - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z) * 0.02f);
        //Matching();

        //MainSystem.INSTANCE.mQRCooltime = true; //*LKH* - 20190807, QR code 인식 버튼제어 추가
    }

    //private void Update()
    //{
    //    startPos = gameObject.transform.position;
    //}

    //private void Update()   //*LKH* - 20190807, QR code 인식 버튼제어 추가
    //{
    //    if ((btn_QR.enabled && MainSystem.INSTANCE.mQRCooltime) || CRemoteProcess.Instance.mRemoteSupportMode)
    //    {
    //        btn_QR.enabled = false;
    //        btn_QR.GetComponent<Button>().interactable = false;
    //        btn_QR.transform.GetChild(0).gameObject.SetActive(false);
    //        btn_QR.transform.GetChild(1).gameObject.SetActive(true);
    //    }
    //    else if ((!btn_QR.enabled && !MainSystem.INSTANCE.mQRCooltime) && !CRemoteProcess.Instance.mRemoteSupportMode)
    //    {
    //        btn_QR.transform.GetChild(0).gameObject.SetActive(true);
    //        btn_QR.transform.GetChild(1).gameObject.SetActive(false);
    //        btn_QR.enabled = true;
    //        btn_QR.GetComponent<Button>().interactable = true;
    //    }

    //    if (btn_RemoteSupporting.enabled && (MainSystem.INSTANCE.mQRCooltime || CRemoteProcess.Instance.mRemoteSupportMode))
    //    {
    //        btn_RemoteSupporting.enabled = false;
    //        btn_RemoteSupporting.GetComponent<Button>().interactable = false;
    //        btn_RemoteSupporting.transform.GetChild(0).gameObject.SetActive(false);
    //        btn_RemoteSupporting.transform.GetChild(1).gameObject.SetActive(true);
    //    }
    //    else if (!btn_RemoteSupporting.enabled && !MainSystem.INSTANCE.mQRCooltime && !CRemoteProcess.Instance.mRemoteSupportMode)
    //    {
    //        btn_RemoteSupporting.transform.GetChild(0).gameObject.SetActive(true);
    //        btn_RemoteSupporting.transform.GetChild(1).gameObject.SetActive(false);
    //        btn_RemoteSupporting.enabled = true;
    //        btn_RemoteSupporting.GetComponent<Button>().interactable = true;
    //        if(isRemoteSup == 1)
    //        {
    //            btn_RemoteSupporting.transform.GetChild(0).gameObject.SetActive(false);
    //            btn_RemoteSupporting.transform.GetChild(1).gameObject.SetActive(true);
    //        }
    //    }
        
    //}

    //public void Matching()
    //{
    //    if (MainSystem.INSTANCE.camRotVal.x < 0 && MainSystem.INSTANCE.camRotVal.y < 0 && MainSystem.INSTANCE.camRotVal.z < 0)
    //    {
    //        gameObject.transform.position = new Vector3((startPos.x - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x) * 0.02f, (startPos.y - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.y) * 0.01f, (startPos.z - MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z) * 0.02f);
    //    }
    //    else
    //    {
    //        gameObject.transform.position = new Vector3((MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x - startPos.x) * 0.02f, (MainSystem.INSTANCE.CAMERA_MAIN.transform.position.y - startPos.y) * 0.01f, (MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z - startPos.z) * 0.02f);
    //    }
    //}

    private void OnEnable()
    {
        //m_NeOSSpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "NeOSS_Info", null);
        //m_NeOSSpanel.gameObject.transform.position = gameObject.transform.position;
        //isNeOSS = 1;
        StartCoroutine(DashBoard());
        isCheckLists = 1;
        isBase = true;
    }

    public void OnClickNeossPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actNeOSSpos != null)
            m_actNeOSSpos();
        if(m_NeOSSpanel != null)
        {
            if(isNeOSS == 1)
            {
                //m_NeOSSpanel.gameObject.transform.position = new Vector3(m_NeOSSpanel.gameObject.transform.position.x - 0.21f, m_NeOSSpanel.gameObject.transform.position.y + 0.03f, m_NeOSSpanel.gameObject.transform.position.z);
                m_NeOSSpanel.gameObject.transform.position = NeOSS_pos.transform.position;
                m_NeOSSpanel.gameObject.transform.rotation = NeOSS_pos.transform.rotation;
                //m_NeOSSpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_NeOSSpanel.gameObject.transform.position = new Vector3(-0.6f, 0.3f, 0.4f);
                //m_NeOSSpanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isNeOSS = 2;
                btn_NeOSSpos[0].gameObject.SetActive(false);
                btn_NeOSSpos[1].gameObject.SetActive(true);
            }
        }

        if (isBase)
        {
            isBase = false;
        }
    }

    public void OnClickNeOSSReturnPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (!isBase)
        {
            isBase = true;
            if (isNeOSS == 2)
            {
                //m_NeOSSpanel.gameObject.transform.position = new Vector3(m_NeOSSpanel.gameObject.transform.position.x + 0.21f, m_NeOSSpanel.gameObject.transform.position.y - 0.03f, m_NeOSSpanel.gameObject.transform.position.z);
                //m_NeOSSpanel.gameObject.transform.position = Main_pos.transform.position;
                m_NeOSSpanel.gameObject.transform.position = Main_pos.transform.position;
                //m_NeOSSpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                m_NeOSSpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_NeOSSpanel.gameObject.transform.position = new Vector3(0.01f, 0.216f, 0.5f);
                //m_NeOSSpanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isNeOSS = 1;
                btn_NeOSSpos[0].gameObject.SetActive(true);
                btn_NeOSSpos[1].gameObject.SetActive(false);
            }
        }

        if (isBase)
            return;
    }

    public void OnClickCheckListsPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actCheckListsPos != null)
            m_actCheckListsPos();
        if (m_CheckListspanel != null)
        {
            if(isCheckLists == 1)
            {
                //m_CheckListspanel.gameObject.transform.position = new Vector3(m_CheckListspanel.gameObject.transform.position.x - 0.21f, m_CheckListspanel.gameObject.transform.position.y - 0.07f, m_CheckListspanel.gameObject.transform.position.z);
                m_CheckListspanel.gameObject.transform.position = ChkLists_pos.transform.position;
                m_CheckListspanel.gameObject.transform.rotation = ChkLists_pos.transform.rotation;
                //m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_CheckListspanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isCheckLists = 2;
                btn_CheckListsPos[0].gameObject.SetActive(false);
                btn_CheckListsPos[1].gameObject.SetActive(true);
            }
        }

        if (isBase)
            isBase = false;
    }

    public void OnClickCheckListsReturnPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (!isBase)
        {
            isBase = true;
            if (isCheckLists == 2)
            {
                //m_CheckListspanel.gameObject.transform.position = new Vector3(m_CheckListspanel.gameObject.transform.position.x + 0.21f, m_CheckListspanel.gameObject.transform.position.y + 0.07f, m_CheckListspanel.gameObject.transform.position.z);
                //m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
                //m_CheckListspanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
                m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_CheckListspanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isCheckLists = 1;
                btn_CheckListsPos[0].gameObject.SetActive(true);
                btn_CheckListsPos[1].gameObject.SetActive(false);
            }
        }

        if (isBase)
            return;
        
    }

    public void OnClickPartsInfoPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actPartsInfoPos != null)
            m_actPartsInfoPos();
        if (m_PartsInfopanel != null)
        {
            if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
            {
                if (isPartsInfo == 1)
                {
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(m_PartsInfopanel.gameObject.transform.position.x + 0.16f, m_PartsInfopanel.gameObject.transform.position.y + 0.02f, m_PartsInfopanel.gameObject.transform.position.z);
                    m_PartsInfopanel.gameObject.transform.position = PartsInfo_pos.transform.position;
                    m_PartsInfopanel.gameObject.transform.rotation = PartsInfo_pos.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                    isPartsInfo = 2;
                    btn_PartsInfoPos[0].gameObject.SetActive(false);
                    btn_PartsInfoPos[1].gameObject.SetActive(true);

                }
            }
            
            if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
            {
                if (isPartsInfo == 1)
                {
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(m_PartsInfopanel.gameObject.transform.position.x + 0.16f, m_PartsInfopanel.gameObject.transform.position.y + 0.02f, m_PartsInfopanel.gameObject.transform.position.z);
                    m_PartsInfopanel.gameObject.transform.localPosition = new Vector3(2.22f, 0.3f, -2.82f);
                    m_PartsInfopanel.gameObject.transform.rotation = PartsInfo_pos.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                    isPartsInfo = 2;
                    btn_PartsInfoPos[0].gameObject.SetActive(false);
                    btn_PartsInfoPos[1].gameObject.SetActive(true);

                }
            }
        }

        if (isBase)
            isBase = false;
        
    }

    public void OnClickPartsInfoReturnPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
        {
            if (!isBase)
            {
                isBase = true;
                if (isPartsInfo == 2)
                {
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(m_PartsInfopanel.gameObject.transform.position.x - 0.16f, m_PartsInfopanel.gameObject.transform.position.y - 0.02f, m_PartsInfopanel.gameObject.transform.position.z);
                    //m_PartsInfopanel.gameObject.transform.position = Main_pos.transform.position;
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.01f, gameObject.transform.position.z);
                    m_PartsInfopanel.gameObject.transform.position = new Vector3(Main_pos.transform.position.x + 0.3f, Main_pos.transform.position.y + 2.4f, Main_pos.transform.position.z + 2.6f);
                    m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                    isPartsInfo = 1;
                    btn_PartsInfoPos[0].gameObject.SetActive(true);
                    btn_PartsInfoPos[1].gameObject.SetActive(false);
                }
            }

            if (isBase)
                return;
        }
        
        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
        {
            if (!isBase)
            {
                isBase = true;
                if (isPartsInfo == 2)
                {
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(m_PartsInfopanel.gameObject.transform.position.x - 0.16f, m_PartsInfopanel.gameObject.transform.position.y - 0.02f, m_PartsInfopanel.gameObject.transform.position.z);
                    //m_PartsInfopanel.gameObject.transform.position = Main_pos.transform.position;
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.01f, gameObject.transform.position.z);
                    m_PartsInfopanel.gameObject.transform.localPosition = new Vector3(-0.15f, 0, -3.1f);
                    //m_PartsInfopanel.gameObject.transform.position = new Vector3(Main_pos.transform.position.x + 0.3f, Main_pos.transform.position.y + 2.4f, Main_pos.transform.position.z + 2.6f);
                    m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                    //m_PartsInfopanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                    isPartsInfo = 1;
                    btn_PartsInfoPos[0].gameObject.SetActive(true);
                    btn_PartsInfoPos[1].gameObject.SetActive(false);
                }
            }

            if (isBase)
                return;
        }
        
    }

    public void OnClickManualPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actManualPos != null)
            m_actManualPos();
        if (m_Manualpanel != null)
        {
            if(isManual == 1)
            {
                //m_Manualpanel.gameObject.transform.position = new Vector3(m_Manualpanel.gameObject.transform.position.x + 0.21f, m_Manualpanel.gameObject.transform.position.y - 0.07f, m_Manualpanel.gameObject.transform.position.z);
                m_Manualpanel.gameObject.transform.position = Manual_pos.transform.position;
                m_Manualpanel.gameObject.transform.rotation = Manual_pos.transform.rotation;
                //m_Manualpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_Manualpanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isManual = 2;
                btn_ManualPos[0].gameObject.SetActive(false);
                btn_ManualPos[1].gameObject.SetActive(true);
            }
        }

        if (isBase)
            isBase = false;
        
    }

    public void OnClickManualReturnPos(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (!isBase)
        {
            isBase = true;
            if (isManual == 2)
            {
                //m_Manualpanel.gameObject.transform.position = new Vector3(m_Manualpanel.gameObject.transform.position.x - 0.21f, m_Manualpanel.gameObject.transform.position.y + 0.07f, m_Manualpanel.gameObject.transform.position.z);
                //m_Manualpanel.gameObject.transform.position = Main_pos.transform.position;
                //m_Manualpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                m_Manualpanel.gameObject.transform.position = Main_pos.transform.position;
                m_Manualpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                //m_Manualpanel.gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
                isManual = 1;
                btn_ManualPos[0].gameObject.SetActive(true);
                btn_ManualPos[1].gameObject.SetActive(false);
            }
        }

        if (isBase)
            return;
        
    }

    public void OnClickQR(AxRButton _button)
    {
        //if (MainSystem.INSTANCE.mQRCooltime) //*LKH* - 20190807, 준비될때까지 QR코드 버튼 무시
        //    return;

        StartCoroutine(ClickSound());
        if (m_actQR != null)
            m_actQR();
        if (m_NeOSSpanel != null)
        {
            Destroy(m_NeOSSpanel.gameObject);
            m_NeOSSpanel = null;
        }
        if (m_CheckListspanel != null)
        {
            Destroy(m_CheckListspanel.gameObject);
            m_CheckListspanel = null;
        }
        if (m_RemoteSupportingpanel != null)
        {
            Destroy(m_RemoteSupportingpanel.gameObject);
            m_RemoteSupportingpanel = null;
        }
        if (m_PartsInfopanel != null)
        {
            Destroy(m_PartsInfopanel.gameObject);
            m_PartsInfopanel = null;
        }
        if (m_Manualpanel != null)
        {
            Destroy(m_Manualpanel.gameObject);
            m_Manualpanel = null;
        }

        //MainSystem.INSTANCE.ActiveVuforia(true);
        //MainSystem.INSTANCE.fSetQRCodeCheckTime(MainSystem.mQRCodeSkipTimer);   //*LKH* - 20190813, QR코드 재인식 문제 수정
        MainSystem.INSTANCE.StartQRRecog();
        StartCoroutine(StartRerecog());
        Debug.Log("QR코드 ON!");

    }
    public void OnClickNeOSS(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actNeOSS != null)
            m_actNeOSS();

        if (m_NeOSSpanel != null)
        {
            if(isNeOSS == 1)
            {
                isBase = false;
                isNeOSS = 0;
                Destroy(m_NeOSSpanel.gameObject);
                btn_NeOSS.transform.GetChild(0).gameObject.SetActive(true);
                btn_NeOSS.transform.GetChild(1).gameObject.SetActive(false);
                //[SSPARK] 패널이 destroy되어있는 상태일 때는 해당 패널의 navigation버튼 또한 비활성화
                btn_NeOSSpos[0].gameObject.SetActive(false);
                btn_NeOSSpos[1].gameObject.SetActive(false);
            }

            else if (isNeOSS == 2)
            {
                return;
            }
        }

        if(!isBase)
        {
            if (m_NeOSSpanel == null)
            {
                if (isNeOSS == 0)
                {
                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
                    {
                        isBase = true;
                        isNeOSS = 1;
                        m_NeOSSpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "NeOSS_Info", m_vufoCon.transform.GetChild(0).gameObject);
                        m_NeOSSpanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_NeOSSpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        m_NeOSSpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_NeOSS.transform.GetChild(0).gameObject.SetActive(false);
                        btn_NeOSS.transform.GetChild(1).gameObject.SetActive(true);
                        btn_NeOSSpos[0].gameObject.SetActive(true);
                        btn_NeOSSpos[1].gameObject.SetActive(false);
                    }

                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
                    {
                        isBase = true;
                        isNeOSS = 1;
                        m_NeOSSpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "NeOSS_Info", m_vufoCon.transform.GetChild(2).gameObject);
                        m_NeOSSpanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_NeOSSpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        m_NeOSSpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_NeOSS.transform.GetChild(0).gameObject.SetActive(false);
                        btn_NeOSS.transform.GetChild(1).gameObject.SetActive(true);
                        btn_NeOSSpos[0].gameObject.SetActive(true);
                        btn_NeOSSpos[1].gameObject.SetActive(false);
                    }
                    
                }
                
            }
        }

        if(isBase)
        {
            return;
        }
    }
    public void OnClickCheckLists(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actCheckLists != null)
            m_actCheckLists();
        
        if (m_CheckListspanel != null)
        {
            if(isCheckLists == 1)
            {
                isBase = false;
                isCheckLists = 0;
                Destroy(m_CheckListspanel.gameObject);
                btn_CheckLists.transform.GetChild(0).gameObject.SetActive(true);
                btn_CheckLists.transform.GetChild(1).gameObject.SetActive(false);
                //[SSPARK] 패널이 destroy되어있는 상태일 때는 해당 패널의 navigation버튼 또한 비활성화
                btn_CheckListsPos[0].gameObject.SetActive(false);
                btn_CheckListsPos[1].gameObject.SetActive(false);
            }
            else if(isCheckLists == 2)
            {
                return;
            }
        }

        if (!isBase)
        {
            if (m_CheckListspanel == null)
            {
                if (isCheckLists == 0)
                {
                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
                    {
                        isBase = true;
                        isCheckLists = 1;
                        m_CheckListspanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "CheckLists", m_vufoCon.transform.GetChild(0).gameObject);
                        m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_CheckListspanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        //m_CheckListspanel.gameObject.transform.position = new Vector3(m_CheckListspanel.gameObject.transform.position.x, m_CheckListspanel.gameObject.transform.position.y, m_CheckListspanel.gameObject.transform.position.z - 0.36f);
                        m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_CheckLists.transform.GetChild(0).gameObject.SetActive(false);
                        btn_CheckLists.transform.GetChild(1).gameObject.SetActive(true);
                        btn_CheckListsPos[0].gameObject.SetActive(true);
                        btn_CheckListsPos[1].gameObject.SetActive(false);
                    }
                    
                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
                    {
                        isBase = true;
                        isCheckLists = 1;
                        m_CheckListspanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "CheckLists", m_vufoCon.transform.GetChild(2).gameObject);
                        m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_CheckListspanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        //m_CheckListspanel.gameObject.transform.position = new Vector3(m_CheckListspanel.gameObject.transform.position.x, m_CheckListspanel.gameObject.transform.position.y, m_CheckListspanel.gameObject.transform.position.z - 0.36f);
                        m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_CheckLists.transform.GetChild(0).gameObject.SetActive(false);
                        btn_CheckLists.transform.GetChild(1).gameObject.SetActive(true);
                        btn_CheckListsPos[0].gameObject.SetActive(true);
                        btn_CheckListsPos[1].gameObject.SetActive(false);
                    }
                }
            }
        }
        
        if(isBase)
        {
            return;
        }
    }

    public void OnClickRemoteSupporting(AxRButton _button)
    {
        //if (MainSystem.INSTANCE.mQRCooltime || CRemoteProcess.Instance.mRemoteSupportMode)  //*LKH* - 20190807, QR쿨타임 또는 이미 원격지원중인 경우 원격지원 버튼 무시
        //    return;
#if UNITY_WSA_10_0
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actRemoteSupporting != null)
            m_actRemoteSupporting();

        if (m_RemoteSupportingpanel != null)
        {
            if(isRemoteSup == 1)
            {
                isBase = false;
                isRemoteSup = 0;
                if (!isBase && isRemoteSup == 0)
                {
                    Destroy(m_RemoteSupportingpanel.gameObject);
                    btn_RemoteSupporting.transform.GetChild(0).gameObject.SetActive(true);
                    btn_RemoteSupporting.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if(isRemoteSup == 2)
            {
                return;
            }
            
        }

        if(!isBase)
        {
            if (m_RemoteSupportingpanel == null)
            {
                if (isRemoteSup == 0)
                {
                    isBase = true;
                    isRemoteSup = 1;
                    m_RemoteSupportingpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Remote_Supporting", m_vufoCon.transform.GetChild(0).gameObject);
                    m_RemoteSupportingpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                    m_RemoteSupportingpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                    btn_RemoteSupporting.transform.GetChild(0).gameObject.SetActive(false);
                    btn_RemoteSupporting.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        
        if(isBase)
        {
            return;
        }
#endif

#if UNITY_EDITOR || UNITY_ANDROID

        if (remote_Warning.activeSelf)
            remote_Warning.SetActive(false);
        else
            remote_Warning.SetActive(true);

#endif

    }

    public void OnClickPartsInfo(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actPartsInfo != null)
            m_actPartsInfo();
        
        if (m_PartsInfopanel != null)
        {
            if(isPartsInfo == 1)
            {
                isBase = false;
                isPartsInfo = 0;
                Destroy(m_PartsInfopanel.gameObject);
                btn_PartsInfo.transform.GetChild(0).gameObject.SetActive(true);
                btn_PartsInfo.transform.GetChild(1).gameObject.SetActive(false);
                //[SSPARK] 패널이 destroy되어있는 상태일 때는 해당 패널의 navigation버튼 또한 비활성화
                btn_PartsInfoPos[0].gameObject.SetActive(false);
                btn_PartsInfoPos[1].gameObject.SetActive(false);
            }
            else if(isPartsInfo == 2)
            {
                return;
            }
        }
        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
        {
            if (!isBase)
            {
                if (m_PartsInfopanel == null)
                {
                    if (isPartsInfo == 0)
                    {
                        isBase = true;
                        isPartsInfo = 1;
                        m_PartsInfopanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "PartsInfo", m_vufoCon.transform.GetChild(0).gameObject);
                        //m_PartsInfopanel.gameObject.transform.position = new Vector3(m_PartsInfopanel.gameObject.transform.position.x, m_PartsInfopanel.gameObject.transform.position.y, m_PartsInfopanel.gameObject.transform.position.z + 0.5f);
                        m_PartsInfopanel.gameObject.transform.position = new Vector3(Main_pos.transform.position.x + 0.3f, Main_pos.transform.position.y + 2.4f, Main_pos.transform.position.z + 2.6f);
                        //m_PartsInfopanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.01f, gameObject.transform.position.z);
                        m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_PartsInfo.transform.GetChild(0).gameObject.SetActive(false);
                        btn_PartsInfo.transform.GetChild(1).gameObject.SetActive(true);
                        btn_PartsInfoPos[0].gameObject.SetActive(true);
                        btn_PartsInfoPos[1].gameObject.SetActive(false);
                    }
                }
            }

            if (isBase)
            {
                return;
            }
        }
        
        if (MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
        {
            if (!isBase)
            {
                if (m_PartsInfopanel == null)
                {
                    if (isPartsInfo == 0)
                    {
                        isBase = true;
                        isPartsInfo = 1;
                        m_PartsInfopanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "PartsInfo2", m_vufoCon.transform.GetChild(2).gameObject);
                        m_PartsInfopanel.gameObject.transform.localPosition = new Vector3(-0.15f, 0, -3.1f);
                        //m_PartsInfopanel.gameObject.transform.position = new Vector3(Main_pos.transform.position.x, Main_pos.transform.position.y, Main_pos.transform.position.z - 22.5f);
                        m_PartsInfopanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_PartsInfo.transform.GetChild(0).gameObject.SetActive(false);
                        btn_PartsInfo.transform.GetChild(1).gameObject.SetActive(true);
                        btn_PartsInfoPos[0].gameObject.SetActive(true);
                        btn_PartsInfoPos[1].gameObject.SetActive(false);
                    }
                }
            }

            if (isBase)
            {
                return;
            }
        }
    }

    public void OnClickManual(AxRButton _button)
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();

        if (m_actManual != null)
            m_actManual();
        if (m_Manualpanel != null)
        {
            if(isManual == 1)
            {
                isBase = false;
                isManual = 0;
                Destroy(m_Manualpanel.gameObject);
                btn_Manual.transform.GetChild(0).gameObject.SetActive(true);
                btn_Manual.transform.GetChild(1).gameObject.SetActive(false);
                //[SSPARK] 패널이 destroy되어있는 상태일 때는 해당 패널의 navigation버튼 또한 비활성화
                btn_ManualPos[0].gameObject.SetActive(false);
                btn_ManualPos[1].gameObject.SetActive(false);
            }
            else if(isManual == 2)
            {
                return;
            }
        }

        if(!isBase)
        {
            if (m_Manualpanel == null)
            {
                if (isManual == 0)
                {
                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
                    {
                        isBase = true;
                        isManual = 1;
                        m_Manualpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Manual", m_vufoCon.transform.GetChild(0).gameObject);
                        m_Manualpanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_Manualpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        m_Manualpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_Manual.transform.GetChild(0).gameObject.SetActive(false);
                        btn_Manual.transform.GetChild(1).gameObject.SetActive(true);
                        btn_ManualPos[0].gameObject.SetActive(true);
                        btn_ManualPos[1].gameObject.SetActive(false);
                    }
                    
                    if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
                    {
                        isBase = true;
                        isManual = 1;
                        m_Manualpanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Manual", m_vufoCon.transform.GetChild(2).gameObject);
                        m_Manualpanel.gameObject.transform.position = Main_pos.transform.position;
                        //m_Manualpanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
                        m_Manualpanel.gameObject.transform.rotation = gameObject.transform.rotation;
                        btn_Manual.transform.GetChild(0).gameObject.SetActive(false);
                        btn_Manual.transform.GetChild(1).gameObject.SetActive(true);
                        btn_ManualPos[0].gameObject.SetActive(true);
                        btn_ManualPos[1].gameObject.SetActive(false);
                    }
                }
            }
        }
        
        if(isBase)
        {
            return;
        }
    }

    IEnumerator ClickSound()
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
        yield return null;
    }

    IEnumerator StartRerecog()
    {
        yield return ClickSound();
        Destroy(gameObject);
    }

    IEnumerator DashBoard()
    {
        yield return new WaitForEndOfFrame();
        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
        {
            m_CheckListspanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "CheckLists", m_vufoCon.transform.GetChild(0).gameObject);
            m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
            m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
        }
        
        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
        {
            m_CheckListspanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "CheckLists", m_vufoCon.transform.GetChild(2).gameObject);
            m_CheckListspanel.gameObject.transform.position = Main_pos.transform.position;
            m_CheckListspanel.gameObject.transform.rotation = gameObject.transform.rotation;
        }
        //m_CheckListspanel.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.03f, gameObject.transform.position.z);
        
        //m_CheckListspanel.gameObject.transform.position = new Vector3(m_CheckListspanel.gameObject.transform.position.x, m_CheckListspanel.gameObject.transform.position.y, m_CheckListspanel.gameObject.transform.position .z -  0.36f);
        
        btn_CheckListsPos[0].gameObject.SetActive(true);
        btn_CheckListsPos[1].gameObject.SetActive(false);
    }
}
