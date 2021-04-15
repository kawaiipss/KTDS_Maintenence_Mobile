using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class RecogQR : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] m_QRguide;

    private GameObject m_controlPanel;
    private VuforiaController m_vufoCon;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    private void Awake()
    {
        if (m_audioSource == null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        InitRecog();
        m_vufoCon = GameObject.Find("VuforiaController").GetComponent<VuforiaController>();
        //m_vufoCon.transform.GetChild(0).position = new Vector3(m_vufoCon.LISTTARGET[0].gameObject.transform.position.x * 0.1f - 0.2f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x, m_vufoCon.LISTTARGET[0].gameObject.transform.parent.position.y * 0.1f, m_vufoCon.LISTTARGET[0].gameObject.transform.parent.position.z * 0.1f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z + 0.3f);
        //m_vufoCon = VuforiaBehaviour.Instance
    }

    public void InitRecog()
    {
        if (!MainSystem.INSTANCE.CAMERA_MAIN.GetComponent<VuforiaBehaviour>().enabled)
            MainSystem.INSTANCE.CAMERA_MAIN.GetComponent<VuforiaBehaviour>().enabled = true;
        m_QRguide[0].gameObject.SetActive(true);
        m_QRguide[1].gameObject.SetActive(false);
    }
    
    //IEnumerator RecognizedQR()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    if (m_QRguide[0].gameObject.activeSelf)
    //        m_QRguide[0].gameObject.SetActive(false);
    //    if (!m_QRguide[1].gameObject.activeSelf)
    //        m_QRguide[1].gameObject.SetActive(true);
    //    //StartCoroutine(GoToDetailInfo());
    //}

    public void MSPPRecognized()
    {
        if (m_QRguide[0].gameObject.activeSelf)
            m_QRguide[0].gameObject.SetActive(false);
        if (!m_QRguide[1].gameObject.activeSelf)
            m_QRguide[1].gameObject.SetActive(true);
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
        StartCoroutine(GoToMSPPinfo());
    }

    public void FTTHRecognized()
    {
        if (m_QRguide[0].gameObject.activeSelf)
            m_QRguide[0].gameObject.SetActive(false);
        if (!m_QRguide[1].gameObject.activeSelf)
            m_QRguide[1].gameObject.SetActive(true);
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
        StartCoroutine(GoToFTTHinfo());
    }
    
    IEnumerator GoToMSPPinfo()
    {
        yield return new WaitForSeconds(1.0f);
        InitRecog();
        //StartCoroutine(StartMSPP());
        Destroy(gameObject);
        //MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Control_Panel", MainSystem.INSTANCE.CAMERA_MAIN.gameObject);
        //m_controlPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Control_Panel", null);

        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 1)
        {
            m_controlPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Control_Panel", m_vufoCon.transform.GetChild(0).gameObject);
        }

        if(MainSystem.INSTANCE.IS_MSPP_TRACKED == 2)
        {
            m_controlPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Control_Panel", m_vufoCon.transform.GetChild(2).gameObject);
        }
        
        //m_controlPanel.transform.position = m_vufoCon.LISTTARGET[0].transform.position;
        //////[SSPARK] 모바일 버전용 임시주석(수정가능성있음)
        //m_controlPanel.transform.position = MainSystem.INSTANCE.CAMERA_MAIN.transform.GetChild(0).transform.position;
        //m_controlPanel.transform.eulerAngles = new Vector3(MainSystem.INSTANCE.CAMERA_MAIN.transform.eulerAngles.x, MainSystem.INSTANCE.CAMERA_MAIN.transform.eulerAngles.y, 0);
        ///////
        //m_controlPanel.transform.localEulerAngles = new Vector3(MainSystem.INSTANCE.CAMERA_MAIN.transform.localEulerAngles.x, MainSystem.INSTANCE.CAMERA_MAIN.transform.localEulerAngles.y, 0); 
    }

    IEnumerator GoToFTTHinfo()
    {
        yield return new WaitForSeconds(1.0f);
        InitRecog();
        Destroy(gameObject);
        //StartCoroutine(StartFTTH());
        m_controlPanel = MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "FTTHinfo", m_vufoCon.transform.GetChild(1).gameObject);
        //////[SSPARK] 모바일 버전용 임시주석(수정가능성있음)
        //m_controlPanel.transform.position = MainSystem.INSTANCE.CAMERA_MAIN.transform.GetChild(0).transform.position;
        //m_controlPanel.transform.eulerAngles = new Vector3(MainSystem.INSTANCE.CAMERA_MAIN.transform.eulerAngles.x, MainSystem.INSTANCE.CAMERA_MAIN.transform.eulerAngles.y, 0);
        //////
        //m_controlPanel.transform.position = new Vector3(m_vufoCon.LISTTARGET[0].gameObject.transform.position.x * 0.1f - 0.2f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x, m_vufoCon.LISTTARGET[0].gameObject.transform.parent.position.y * 0.1f, m_vufoCon.LISTTARGET[0].gameObject.transform.parent.position.z * 0.1f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z + 0.3f);
        //m_controlPanel.transform.eulerAngles = new Vector3(0, m_vufoCon.LISTTARGET[0].gameObject.transform.eulerAngles.y, 0);
    }

    IEnumerator StartMSPP()
    {
        yield return GoToMSPPinfo();
        Destroy(gameObject);
    }

    IEnumerator StartFTTH()
    {
        yield return GoToFTTHinfo();
        Destroy(gameObject);
    }
}
