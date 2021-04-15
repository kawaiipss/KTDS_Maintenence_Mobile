using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaController : MonoBehaviour
{
    [SerializeField] private List<KTDS_TrackableEventHandler> m_listTarget;
    public List<KTDS_TrackableEventHandler> LISTTARGET { get { return m_listTarget; } }
    private VuforiaBehaviour m_vuforiaBehaviour;
    public VuforiaBehaviour VUFORIABEHAVIOUR { set { m_vuforiaBehaviour = value; } get { return m_vuforiaBehaviour; } }

    private Action<string, bool> m_actTracked;
    public Action<string, bool> ACT_TRACKED { set { m_actTracked = value; } }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        ActiveRecognize(false);

        if (m_listTarget != null)
        {
            for (int i = 0; i < m_listTarget.Count; i++)
            {
                m_listTarget[i].ACT_TRACK = TrackedTarget;
                //m_listTarget[i].gameObject.SetActive(false);
            }
        }

        //gameObject.transform.GetChild(0).position = new Vector3(gameObject.transform.GetChild(0).position.x * 0.1f - 0.2f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.x, gameObject.transform.GetChild(0).position.y * 0.1f, gameObject.transform.GetChild(0).position.z * 0.1f + MainSystem.INSTANCE.CAMERA_MAIN.transform.position.z + 0.3f);
    }

    public void fSetQRCodeCheckTime(float _milliSecondsOffset)  //*LKH* - 20190813, QR코드 재인식 문제 수정
    {
        if (m_listTarget != null)
        {
            for (int i = 0; i < m_listTarget.Count; i++)
            {
                m_listTarget[i].fSetQRCodeCheckTime(_milliSecondsOffset);
            }
        }
    }

    public void ActiveRecognize(bool _isRecog)
    {
        if(m_vuforiaBehaviour == null)
        {
            m_vuforiaBehaviour = MainSystem.INSTANCE.CAMERA_MAIN.GetComponent<VuforiaBehaviour>();
            //m_vuforiaBehaviour = MainSystem.INSTANCE.CAMERA_MAIN.gameObject.AddComponent<VuforiaBehaviour>();
        }

        m_vuforiaBehaviour.enabled = _isRecog;

        for (int i = 0; i < m_listTarget.Count; i++)
        {
            m_listTarget[i].gameObject.SetActive(_isRecog);
        }
    }

    private void TrackedTarget(string _id, bool _isTracked)
    {
        if (m_actTracked != null)
        {
            m_actTracked(_id, _isTracked);
            print("트랙트 타겟에서 _isTracked " + _isTracked.ToString());
        }
    }

    private void OnDestroy()
    {
        ActiveRecognize(false);
    }
}
