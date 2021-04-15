using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsInfoSub : MonoBehaviour
{
    [SerializeField] private AxRButton m_btnClose;
    [Header("ObjCtrl")]
    [SerializeField] private AxRButton m_btnRotUp;
    [SerializeField] private AxRButton m_btnRotDown;
    [SerializeField] private AxRButton m_btnRotLeft;
    [SerializeField] private AxRButton m_btnRotRight;
    [SerializeField] private AxRButton m_btnScaleUp;
    [SerializeField] private AxRButton m_btnScaleDown;

    private Action m_closeBtn;
    public Action CLOSE_BTN { set { m_closeBtn = value; } }

    private Action m_rotUpBtn;
    public Action ROTUP_BTN { set { m_rotUpBtn = value; } }
    private Action m_rotDownBtn;
    public Action ROTDOWN_BTN { set { m_rotDownBtn = value; } }
    private Action m_rotLeftBtn;
    public Action ROTLEFT_BTN { set { m_rotLeftBtn = value; } }
    private Action m_rotRightBtn;
    public Action ROTUPRIGHT_BTN { set { m_rotRightBtn = value; } }

    private Action m_scaleUpBtn;
    public Action SCALEUP_BTN { set { m_scaleUpBtn = value; } }
    private Action m_scaleDownBtn;
    public Action SCALEDOWN_BTN { set { m_scaleDownBtn = value; } }

    [Header("Target")]
    [SerializeField] private GameObject m_target;
    [SerializeField] private GameObject m_deactivated;


    private void Awake()
    {
        m_btnClose.ACT_CLICK = Onclose;

        m_btnRotUp.ACT_CLICK = RotUp;
        m_btnRotDown.ACT_CLICK = RotDown;
        m_btnRotLeft.ACT_CLICK = RotLeft;
        m_btnRotRight.ACT_CLICK = RotRight;

        m_btnScaleUp.ACT_CLICK = ScaleUp;
        m_btnScaleDown.ACT_CLICK = ScaleDown;

        gameObject.GetComponent<Canvas>().enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAnim());
        //StartCoroutine(CanvasOn());
    }

    IEnumerator PlayAnim()
    {
        yield return null;
        m_target.gameObject.GetComponent<Animation>().Play();
        m_deactivated.gameObject.GetComponent<Animation>().Play();
    }

    IEnumerator CanvasOn()
    {
        yield return StartCoroutine("PlayAnim");
        if (!gameObject.GetComponent<Canvas>().enabled)
            gameObject.GetComponent<Canvas>().enabled = true;
    }

    //void PlayAnim()
    //{

    //}

    private void ScaleUp(AxRButton _Button)
    {
        StartCoroutine(ScaleUp());
    }
    private void ScaleDown(AxRButton _Button)
    {
        StartCoroutine(ScaleDown());
    }
    private void RotUp(AxRButton _Button)
    {
        StartCoroutine(RotateUp());
    }
    private void RotDown(AxRButton _Button)
    {
        StartCoroutine(RotateDown());
    }
    private void RotLeft(AxRButton _Button)
    {
        StartCoroutine(RotateLeft());
    }
    private void RotRight(AxRButton _Button)
    {
        StartCoroutine(RotateRight());
    }


    IEnumerator ScaleUp()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localScale = Vector3.Lerp(m_target.transform.localScale,
                new Vector3(m_target.transform.localScale.x + .5f,
                m_target.transform.localScale.y + .5f, m_target.transform.localScale.z + .5f), Time.deltaTime);


            if (m_target.transform.localScale.x > 2.0f)
                m_target.transform.localScale = new Vector3(2, 2, 2);
        }
        Debug.Log("스케일업!!");
    }
    IEnumerator ScaleDown()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localScale = Vector3.Lerp(m_target.transform.localScale,
                new Vector3(m_target.transform.localScale.x - 0.5f,
                m_target.transform.localScale.y - 0.5f, m_target.transform.localScale.z - 0.5f), Time.deltaTime);
            //[SSPARK] 오브젝트 스케일 하한선을 낮춤 (0.5 -> 0.3)
            if (m_target.transform.localScale.x < 0.3f)
                m_target.transform.localScale = new Vector3(.3f, .3f, .3f);
            //
        }
        Debug.Log("스케일다운!!");
    }
    IEnumerator RotateUp()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localEulerAngles = Vector3.Lerp(m_target.transform.localEulerAngles,
                new Vector3(m_target.transform.localEulerAngles.x,
                m_target.transform.localEulerAngles.y, m_target.transform.localEulerAngles.z + 30.0f), Time.deltaTime);
        }
        Debug.Log("로테잇업!!");
    }
    IEnumerator RotateDown()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localEulerAngles = Vector3.Lerp(m_target.transform.localEulerAngles,
                new Vector3(m_target.transform.localEulerAngles.x,
                m_target.transform.localEulerAngles.y, m_target.transform.localEulerAngles.z - 30.0f), Time.deltaTime);
        }
        Debug.Log("로테잇다운!!");
    }
    IEnumerator RotateLeft()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localEulerAngles = Vector3.Lerp(m_target.transform.localEulerAngles,
                new Vector3(m_target.transform.localEulerAngles.x,
                m_target.transform.localEulerAngles.y + 30.0f, m_target.transform.localEulerAngles.z), Time.deltaTime);
        }
        Debug.Log("로테잇레프트!!");
    }
    IEnumerator RotateRight()
    {
        float _curTime = 0.0f;
        float _maxTime = 1.0f;
        while (_curTime < _maxTime)
        {
            _curTime += Time.deltaTime;
            if (_curTime > _maxTime)
                _curTime = _maxTime;
            yield return null;
            m_target.transform.localEulerAngles = Vector3.Lerp(m_target.transform.localEulerAngles,
                new Vector3(m_target.transform.localEulerAngles.x,
                m_target.transform.localEulerAngles.y - 30.0f, m_target.transform.localEulerAngles.z), Time.deltaTime);
        }
        Debug.Log("로테잇라이트!!");
    }

    public void Onclose(AxRButton _button)
    {
        if (m_closeBtn != null)
            m_closeBtn();
        Debug.Log("PartsInfo Off!!");
        Destroy(this.gameObject);
    }
}
