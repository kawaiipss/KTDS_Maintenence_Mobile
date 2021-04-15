using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLogo : MonoBehaviour
{
    private const float MOVE_DOWN_POS = 0.2f;
    private static Color COLOR_DISABLE = new Color(1f, 1f, 1f, 0f);

    //[SerializeField] private Image m_sprLogo;
    //[SerializeField] private Image m_sprLogoSub;
    private Image[] m_arrImages;
    private IEnumerator _coTimer;

    private Action m_actSkip;
    public Action ACT_SKIP { set { m_actSkip = value; } }

    //public GameObject logoAnimMgr;

    void Awake()
    {
        initailize();
    }
    void Start()
    {
        //if (_coTimer == null)
        //    StartCoroutine(_coTimer = TimerShowMessage());
        //StartCoroutine(RotateLogo());
        //StartCoroutine(SkipTimer());
        StartCoroutine(SkipTimer());
    }

    private void initailize()
    {
        //m_sprLogo.color = COLOR_DISABLE;
        //m_sprLogoSub.color = COLOR_DISABLE;
        //m_sprLogo.transform.localPosition = Vector3.down * MOVE_DOWN_POS;
        //m_sprLogoSub.transform.localPosition = Vector3.down * MOVE_DOWN_POS;
    }

    private IEnumerator SkipTimer()
    {
        yield return new WaitForSeconds(6.0f);
        if (null != m_actSkip)
            m_actSkip();
        OnClose();
        MainSystem.InstantiateAndAddTo(URL.PrefabURL.PANELS, "Warning_Panel", null);
    }

    void OnClose()
    {
        Destroy(gameObject);
    }

    //private IEnumerator TimerShowMessage()
    //{
    //    Color _color = COLOR_DISABLE;
    //    m_sprLogo.color = _color;
    //    m_sprLogoSub.color = _color;
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    float _time = 0f;
    //    float _maxTime = 1f;
    //    while (_time < _maxTime)
    //    {
    //        _time += Time.deltaTime * Time.timeScale;
    //        if (_time >= _maxTime) _time = _maxTime;
    //        _color.a = Mathf.Sin(_time * Mathf.PI * 0.5f);
    //        m_sprLogo.color = _color;
    //        m_sprLogoSub.color = _color;
    //        m_sprLogo.transform.localPosition = Vector3.down * (1f - _color.a) * MOVE_DOWN_POS;
    //        m_sprLogoSub.transform.localPosition = Vector3.down * (1f - _color.a + 2.5f) * MOVE_DOWN_POS;
    //        yield return null;
    //    }
    //    _time = 0f;
    //    _color = COLOR_DISABLE;
    //    yield return null;
    //    while (_time < _maxTime)
    //    {
    //        _time += Time.deltaTime * Time.timeScale;
    //        if (_time >= _maxTime) _time = _maxTime;
    //        _color.a = Mathf.Sin(_time * Mathf.PI * 0.5f);
    //        yield return null;
    //    }
    //    _coTimer = null;
    //}

    //IEnumerator RotateLogo()
    //{
    //    yield return TimerShowMessage();
    //    logoAnimMgr.GetComponent<Animation>().Play();
    //    print("로테잇 로고");
    //    //m_sprLogoSub.transform.localRotation = Quaternion.Euler(0, 10f, 0);
    //}
}
