using System;
using System.IO;
using System.Collections;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{
    public class CheckListsPanel : MonoBehaviour
    {
        [SerializeField] private AxRButton[] m_btnClose;

        [Header("Errors")]
        //[SerializeField] private AxRButton m_btnServiceCheck;
        [SerializeField] private AxRButton m_btnPhysicalCheck;
        //[SerializeField] private AxRButton m_btnFanError;

        private Action m_closeBtn;
        public Action CLOSE_BTN { set { m_closeBtn = value; } }
        private Action m_serviceCheck;
        public Action SERVICE_CHECK { set { m_serviceCheck = value; } }
        private Action m_physicalCheck;
        public Action PHYSICAL_CHECK { set { m_physicalCheck = value; } }
        //private Action m_fanError;
        //public Action FAN_ERROR { set { m_fanError = value; } }

        private GameObject m_connectErrorPanel;
        private GameObject m_powerErrorPanel;
        private GameObject m_fanErrorPanel;

        //public Image btnTransform;

        //public GameObject[] typeOfCheckList;
        public GameObject typeOfCheckLists;
        public GameObject m_askRemotePanel;
        public GameObject m_connectRemote;
        public GameObject mainTitle;

        //[SerializeField] private AxRButton[] m_Service_errorCheckLists;
        [SerializeField] private AxRButton[] m_Physical_errorCheckLists;
        [SerializeField] private AxRButton m_btnRemoteConnect;
        [SerializeField] private AxRButton m_btnNoRemote;

        private Action m_actServiceCheckLists;
        public Action ACT_SERVICECHECKLISTS { set { m_actServiceCheckLists = value; } }
        private Action m_actPhysicalCheckLists;
        public Action ACT_PHYSICALCHECKLISTS { set { m_actPhysicalCheckLists = value; } }

        private GameObject m_remoteSupport;
        private GameObject m_chkList;
        public Text m_now;

        private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;

        private ControlPanal m_controlPanel;

        //임시주석처리
        //public int serviceCheck = 0;
        //public int physicalCheck = 0;

        private void Awake()
        {
            m_btnClose[0].ACT_CLICK = Onclose;
            //m_btnServiceCheck.ACT_CLICK = OnServiceCheck;
            m_btnPhysicalCheck.ACT_CLICK = OnPhysicalCheck;
            //m_btnFanError.ACT_CLICK = OnFanError;
            //m_btnServiceCheck.gameObject.SetActive(true);
            m_btnPhysicalCheck.gameObject.SetActive(true);
            m_askRemotePanel.SetActive(false);
            m_connectRemote.SetActive(false);
            //m_btnFanError.gameObject.SetActive(true);
            //for(int i = 0; i < typeOfCheckList.Length; i++)
            //{
            //    typeOfCheckList[i].SetActive(false);
            //}
            typeOfCheckLists.SetActive(false);
            //for(int i=0; i<m_Service_errorCheckLists.Length-1; i++)
            //{
            //    m_Service_errorCheckLists[i].ACT_CLICK = OnClickCheckLists;
            //}
            for(int i = 0; i < m_Physical_errorCheckLists.Length-1; i++)
            {
                m_Physical_errorCheckLists[i].ACT_CLICK = OnClickCheckLists;
            }
            //m_Service_errorCheckLists[m_Service_errorCheckLists.Length-1].ACT_CLICK = OnClickLastCheck;
            m_Physical_errorCheckLists[m_Physical_errorCheckLists.Length-1].ACT_CLICK = OnClickLastCheck;
            m_btnNoRemote.ACT_CLICK = OnClickNoRemote;

            if (m_audioSource == null)
            {
                m_audioSource = gameObject.AddComponent<AudioSource>();
                m_audioSource.playOnAwake = false;
            }
            
            m_btnPhysicalCheck.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }

        private void Start()
        {
            m_controlPanel = GameObject.Find("Control_Panel").GetComponent<ControlPanal>();
            m_now.text = DateTime.Now.ToString("yyyy-MM-dd tt:hh:mm");
            m_btnPhysicalCheck.GetComponent<Animation>().Play();
        }

        //private void Update()
        //{
        //    gameObject.transform.position = btnTransform.gameObject.transform.position;
        //}

        public void OnServiceCheck(AxRButton _button)
        {
            if (m_serviceCheck != null)
                m_serviceCheck();

            if (m_btnPhysicalCheck.gameObject.activeSelf)
            {
                //m_btnServiceCheck.gameObject.SetActive(false);
                m_btnPhysicalCheck.gameObject.SetActive(false);
                mainTitle.SetActive(false);
            }

            //if (m_connectErrorPanel == null)
            //    m_connectErrorPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "ServiceCheck", null);

            //for (int i = 0; i < typeOfCheckList.Length; i++)
            //{
            //    if (!typeOfCheckList[i].activeSelf)
            //        typeOfCheckList[0].SetActive(true);
            //}
            
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();

            //Destroy(gameObject);
            Debug.Log("Connect Error!!");
        }

        public void OnPhysicalCheck(AxRButton _button)
        {
            if (m_physicalCheck != null)
                m_physicalCheck();

            if (m_btnPhysicalCheck.gameObject.activeSelf)
            {
                //m_btnServiceCheck.gameObject.SetActive(false);
                m_btnPhysicalCheck.gameObject.SetActive(false);
                mainTitle.SetActive(false);
            }

            //for (int i = 0; i < typeOfCheckList.Length; i++)
            //{
            //    if (!typeOfCheckList[i].activeSelf)
            //        typeOfCheckList[1].SetActive(true);
            //}

            typeOfCheckLists.SetActive(true);
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();

            //if (m_powerErrorPanel == null)
            //    m_powerErrorPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "PhysicalCheck", null);

            //Destroy(gameObject);
            Debug.Log("Power Error!!");
        }

        public void OnClickLastCheck(AxRButton _button)
        {
            if(!m_askRemotePanel.activeSelf)
                m_askRemotePanel.SetActive(true);
            
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
            for(int i = 0; i < m_Physical_errorCheckLists.Length; i++)
            {
                m_Physical_errorCheckLists[i].gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void OnClickRemoteSupport(AxRButton _button)
        {
            StartCoroutine(ClickSound());
            if (m_askRemotePanel.activeSelf)
                m_askRemotePanel.SetActive(false);

            if (typeOfCheckLists.activeSelf)
                typeOfCheckLists.SetActive(false);

            if (m_connectRemote.activeSelf)
                m_connectRemote.SetActive(false);

            if (!mainTitle.activeSelf)
                mainTitle.SetActive(true);
        }

        public void OnClickNoRemote(AxRButton _button)
        {
            m_askRemotePanel.SetActive(false);
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
        }

        public void OnClickCheckLists(AxRButton _button)
        {
            gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
        }

        public void Onclose(AxRButton _button)
        {
            //if (m_closeBtn != null)
            //    m_closeBtn();
            //Debug.Log("CheckLists Off!!");
            //transform.position = new Vector3(-1.5f, -0.364f, 1.0f);

            //ControlPanal m_controlPanel = GameObject.Find("Control_Panel").GetComponent<ControlPanal>();
            //if (!m_controlPanel.btn_CheckListsPos.gameObject.activeSelf)
            //    m_controlPanel.btn_CheckListsPos.gameObject.SetActive(true);

            ////m_chkList = ControlPanal.INSTANCE.Panel_root;
            ////m_chkList = null;
            ////Destroy(gameObject);
            ///
            if (m_askRemotePanel.activeSelf)
                m_askRemotePanel.SetActive(false);
            //for (int i = 0; i < typeOfCheckList.Length; i++)
            //{
            //    if (typeOfCheckList[i].activeSelf)
            //        typeOfCheckList[i].SetActive(false);
            //}
            typeOfCheckLists.SetActive(false);
            if (!mainTitle.activeSelf)
                mainTitle.SetActive(true);
            //if (!m_btnServiceCheck.gameObject.activeSelf) m_btnServiceCheck.gameObject.SetActive(true);
            if (!m_btnPhysicalCheck.gameObject.activeSelf) m_btnPhysicalCheck.gameObject.SetActive(true);
        }

        IEnumerator ClickSound()
        {
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
            yield return null;
        }

        IEnumerator GotoRemote()
        {
            yield return ClickSound();
            //gameObject.SetActive(false);
            m_controlPanel.ISCHECKLISTS = 0;
            m_controlPanel.CHECKLISTS_BTN.transform.GetChild(0).gameObject.SetActive(true);
            m_controlPanel.CHECKLISTS_BTN.transform.GetChild(1).gameObject.SetActive(false);
            m_controlPanel.REMOTESUPPORTING_PANEL = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Remote_Supporting", null);
            m_controlPanel.REMOTESUPPORTING_PANEL.gameObject.transform.position = gameObject.transform.position;
            m_controlPanel.REMOTESUPPORTING_PANEL.gameObject.transform.rotation = gameObject.transform.rotation;
            m_controlPanel.ISREMOTESUP = 1;
            m_controlPanel.isBase = true;
            m_controlPanel.REMORTESUPPORTING_BTN.transform.GetChild(0).gameObject.SetActive(false);
            m_controlPanel.REMORTESUPPORTING_BTN.transform.GetChild(1).gameObject.SetActive(true);
            m_controlPanel.btn_CheckListsPos[0].gameObject.SetActive(false);
            m_controlPanel.btn_CheckListsPos[1].gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}


