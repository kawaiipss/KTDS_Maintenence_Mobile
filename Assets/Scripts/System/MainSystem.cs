using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour
{
    private static MainSystem instance;
    public static MainSystem INSTANCE { get { return instance; } }

    //private GameObject m_showLogo;
    private GameObject m_QRRecog;

    private RecogQR m_recogQR;

    private VuforiaController m_vuforiaController;

    [SerializeField] private Camera m_main_Camera;
    public Camera CAMERA_MAIN { get { return m_main_Camera; } }

    private int m_isMSPPTracked;
    public int IS_MSPP_TRACKED { get { return m_isMSPPTracked; } }
    private bool m_isFTTHTracked;
    public bool IS_FTTH_TRACKED { get { return m_isFTTHTracked; } }

    private Vector3 startPos = new Vector3();
    //public Text camRot;
    //public Text camPos;

    //public Vector3 camRotVal;

    //[HideInInspector]
    //public bool mQRCooltime = true;                     //*LKH* - 20190807, 추가
    //public const float mQRCodeSkipTimer = 3000f;        //*LKH* - 20190813, QR코드 재인식 문제 수정, QR코드 인식시 mQRCodeCheckTime + mQRCodeSkipTimer 밀리초 이상 경과시 인식처리

    private void Awake()
    {
        m_isMSPPTracked = 0;
        m_isFTTHTracked = false;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        //*LKH* - 20190807, 주석처리
        //if(m_recogQR == null)
        //    m_recogQR = new RecogQR();
    }

    private void Start()
    {
        DontDestroyOnLoad(CAMERA_MAIN);
        //ActiveVuforia(true);
        //m_showLogo = InstantiateAndAddTo(URL.PrefabURL.PANELS, "ViewLogo", null);
        if (CAMERA_MAIN.GetComponent<VuforiaBehaviour>())
            CAMERA_MAIN.GetComponent<VuforiaBehaviour>().enabled = false; // 1
    }

    private void Update()
    {
#if UNITY_ANDROID
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }

    //private void Update()
    //{
    //    if (m_vuforiaController != null)
    //    {
    //        //camRot.text = (Mathf.Atan2(m_vuforiaController.transform.GetChild(0).localPosition.y - m_main_Camera.transform.localPosition.y, m_vuforiaController.transform.GetChild(0).localPosition.x - m_main_Camera.transform.localPosition.x) * 180 / Mathf.PI).ToString();
    //        ////camRotVal = Mathf.Atan2(m_vuforiaController.transform.GetChild(0).localPosition.y - m_main_Camera.transform.localPosition.y, m_vuforiaController.transform.GetChild(0).localPosition.x - m_main_Camera.transform.localPosition.x) * 180 / Mathf.PI;
    //        //camPos.text = (m_vuforiaController.transform.GetChild(0).position - m_main_Camera.transform.position).ToString();
    //        //camRotVal = m_vuforiaController.transform.GetChild(0).position - m_main_Camera.transform.position;
    //        camRot.text = (new Vector3(m_main_Camera.transform.rotation.x, m_main_Camera.transform.rotation.y, m_main_Camera.transform.rotation.z)).ToString();
    //    }
    //}

    public static GameObject InstantiateAndAddTo(string _Path, string _Name, GameObject _Parents)
    {
        UnityEngine.Object res = Resources.Load(_Path + _Name);

        if (res == null)
        {
#if UNITY_EDITOR
            Debug.Log("EmptyResource");
#endif
            return null;
        }

        GameObject _GObj = Instantiate(res) as GameObject;

        Vector3 localPos = _GObj.transform.localPosition;
        Vector3 localEulerAngles = _GObj.transform.localEulerAngles;
        Vector3 localScale = _GObj.transform.localScale;

        if (_Parents != null)
            _GObj.transform.parent = _Parents.transform;

        _GObj.transform.localPosition = localPos;
        _GObj.transform.localScale = localScale;
        _GObj.transform.localEulerAngles = localEulerAngles;
        _GObj.name = _Name;

        return _GObj;
    }

    string isActiveString;

    public void ActiveVuforia(bool _isActive)
    {
        if(_isActive && m_vuforiaController == null)
        {
            m_vuforiaController = InstantiateAndAddTo(URL.PrefabURL.PREFABS, "VuforiaController", null).GetComponent<VuforiaController>();
            fSetQRCodeCheckTime(0); //*LKH* - 20190813, QR코드 재인식 문제 수정
            m_vuforiaController.ACT_TRACKED = TrackedVuforiaTarget;
        }
        if(m_vuforiaController != null)
        {
            m_vuforiaController.ActiveRecognize(_isActive);
        }

    }

    public void StartQRRecog()
    {
        Debug.Log("StartQRRecog!!!");
        //mQRCooltime = true;  //*LKH* - 20190807, 추가
        //if (CRemoteProcess.Instance.mRemoteSupportMode) //*LKH* - 20190807, 원격 지원 추가
        //{
        //    CRemoteProcess.Instance.fStopNetRemote();
        //}

        m_recogQR = InstantiateAndAddTo(URL.PrefabURL.PANELS, "QR_Recog", m_main_Camera.gameObject).GetComponent<RecogQR>();
        ActiveVuforia(true);
    }

    public void ReRecogQR()
    {
        Debug.Log("ReRecogQR!!!");
        //mQRCooltime = true;  //*LKH* - 20190807, 원격 지원 추가
        //if (CRemoteProcess.Instance.mRemoteSupportMode) //*LKH* - 20190807, 원격 지원 추가
        //{
        //    CRemoteProcess.Instance.fStopNetRemote();
        //}

        if (m_recogQR == null)
        {
            m_recogQR = InstantiateAndAddTo(URL.PrefabURL.PANELS, "QR_Recog", m_main_Camera.gameObject).GetComponent<RecogQR>();
            m_recogQR.InitRecog();
            ActiveVuforia(true);
        }
    }

    public void fSetQRCodeCheckTime(float _milliSecondsOffset = 0)  //*LKH* - 20190813, QR코드 재인식 문제 수정
    {
        if (m_vuforiaController != null)
        {
            m_vuforiaController.fSetQRCodeCheckTime(_milliSecondsOffset);
        }
        else
        {
            Debug.LogError("m_vuforiaController 설정 오류!");
        }
    }

    private void TrackedVuforiaTarget(string _id, bool _isTracked)
    {
        if(_isTracked)
        {
            if (m_recogQR == null)  //*LKH* - 20190807, 추가
                return;

            switch (_id)
            {
                case "MSPP":
                    m_isMSPPTracked = 1;
                    //InstantiateAndAddTo(URL.PrefabURL.PANELS, "Control_Panel", null);
                    m_recogQR.MSPPRecognized();

                    //if (m_isMSPPTracked == 1)
                    //    m_isMSPPTracked = 0;

                    Debug.Log("MSPP " + m_isMSPPTracked.ToString() + " 번째 장비!!");

                    break;
                case "OtherMSPP":
                    m_isMSPPTracked = 2;
                    m_recogQR.MSPPRecognized();

                    //if (m_isMSPPTracked == 2)
                    //    m_isMSPPTracked = 0;

                    Debug.Log("MSPP Other "+m_isMSPPTracked.ToString()+ " 번째 장비!!");
                    break;
                case "FTTH":
                    m_isFTTHTracked = true;
                    m_recogQR.FTTHRecognized();
                    if(m_isFTTHTracked)
                    {
                        m_isFTTHTracked = false;
                    }
                    break;
                default:
                    break;
            }
            //ActiveVuforia(false);
            //StartCoroutine(VuforiaActive());

            Debug.Log("1초뒤 호출!!!");
            //m_vuforiaController.gameObject.SetActive(false);    //*LKH* - 20190807, 추가
            //CRemoteProcess.Instance.fDelayCall(1, ChangeCameraPermission);  //*LKH* - 20190807, 원격 지원 추가
        }
        //isActiveString = "_isActive : " + _isTracked.ToString();
    }
    //private void ChangeCameraPermission()   //*LKH* - 20190807, 원격 지원 추가
    //{
    //    m_vuforiaController.gameObject.SetActive(true);

    //    CRemoteProcess.Instance.fDelayCall(1, QRButtonReady);
    //}
    //private void QRButtonReady()   //*LKH* - 20190807, 원격 지원 추가
    //{
    //    Debug.Log("----------------QRButtonReady!!!");
    //    if (CRemoteProcess.Instance.mRemoteSupportMode)
    //    {
    //        CRemoteProcess.Instance.fStartNetRemote();
    //    }

    //    mQRCooltime = false;
    //}

    public void DestroyVuforiaController()
    {
        if (m_vuforiaController != null)
        {
            Destroy(m_vuforiaController.gameObject);
        }
    }
}
