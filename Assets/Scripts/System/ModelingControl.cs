using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelingControl : MonoBehaviour
{
    [SerializeField] private AxRButton m_partsBtn1;
    [SerializeField] private AxRButton m_partsBtn2;
    [SerializeField] private AxRButton m_partsBtn3;
    [SerializeField] private AxRButton m_partsBtn4;
    [SerializeField] private AxRButton m_partsBtn5;

    private Action m_actParts;
    public Action ACT_PARTS { set { m_actParts = value; } }

    public GameObject non_btns;

    private Transform cur_pos1;
    private Transform target_pos1;

    private void Awake()
    {
        m_partsBtn1.ACT_CLICK = OnClickParts1;
        m_partsBtn2.ACT_CLICK = OnClickParts2;
        m_partsBtn3.ACT_CLICK = OnClickParts3;
        m_partsBtn4.ACT_CLICK = OnClickParts4;
        m_partsBtn5.ACT_CLICK = OnClickParts5;
    }

    void OnClickParts1(AxRButton _button)
    {
        StartCoroutine(ClickedParts1(1.0f, new Vector3(m_partsBtn1.gameObject.transform.position.x, m_partsBtn1.gameObject.transform.position.y, m_partsBtn1.gameObject.transform.position.z-0.1f)));
    }

    void OnClickParts2(AxRButton _button)
    {
        StartCoroutine(ClickedParts2(1.0f, new Vector3(m_partsBtn2.gameObject.transform.position.x, m_partsBtn2.gameObject.transform.position.y, m_partsBtn2.gameObject.transform.position.z - 0.1f)));
    }

    void OnClickParts3(AxRButton _button)
    {
        StartCoroutine(ClickedParts3(1.0f, new Vector3(m_partsBtn3.gameObject.transform.position.x, m_partsBtn3.gameObject.transform.position.y, m_partsBtn3.gameObject.transform.position.z - 0.1f)));
    }

    void OnClickParts4(AxRButton _button)
    {
        StartCoroutine(ClickedParts4(1.0f, new Vector3(m_partsBtn4.gameObject.transform.position.x, m_partsBtn4.gameObject.transform.position.y, m_partsBtn4.gameObject.transform.position.z - 0.1f)));
    }

    void OnClickParts5(AxRButton _button)
    {
        StartCoroutine(ClickedParts5(1.0f, new Vector3(m_partsBtn5.gameObject.transform.position.x, m_partsBtn5.gameObject.transform.position.y, m_partsBtn5.gameObject.transform.position.z - 0.1f)));
    }

    IEnumerator ClickedParts1(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_partsBtn1.gameObject.transform.position;
        float elapsed = 0.0f;

        while(elapsed<duration)
        {
            elapsed += Time.deltaTime;
            m_partsBtn1.gameObject.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_partsBtn1.gameObject.GetComponent<BoxCollider>().enabled = false;

        m_partsBtn1.gameObject.transform.position = end;
        m_partsBtn2.gameObject.SetActive(false);
        m_partsBtn3.gameObject.SetActive(false);
        m_partsBtn4.gameObject.SetActive(false);
        m_partsBtn5.gameObject.SetActive(false);
        non_btns.gameObject.SetActive(false);
    }

    IEnumerator ClickedParts2(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_partsBtn2.gameObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_partsBtn2.gameObject.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_partsBtn2.gameObject.GetComponent<BoxCollider>().enabled = false;

        m_partsBtn2.gameObject.transform.position = end;
        m_partsBtn1.gameObject.SetActive(false);
        m_partsBtn3.gameObject.SetActive(false);
        m_partsBtn4.gameObject.SetActive(false);
        m_partsBtn5.gameObject.SetActive(false);
        non_btns.gameObject.SetActive(false);
    }

    IEnumerator ClickedParts3(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_partsBtn3.gameObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_partsBtn3.gameObject.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_partsBtn3.gameObject.GetComponent<BoxCollider>().enabled = false;

        m_partsBtn3.gameObject.transform.position = end;
        m_partsBtn1.gameObject.SetActive(false);
        m_partsBtn2.gameObject.SetActive(false);
        m_partsBtn4.gameObject.SetActive(false);
        m_partsBtn5.gameObject.SetActive(false);
        non_btns.gameObject.SetActive(false);
    }

    IEnumerator ClickedParts4(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_partsBtn4.gameObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_partsBtn4.gameObject.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_partsBtn4.gameObject.GetComponent<BoxCollider>().enabled = false;

        m_partsBtn4.gameObject.transform.position = end;
        m_partsBtn1.gameObject.SetActive(false);
        m_partsBtn2.gameObject.SetActive(false);
        m_partsBtn3.gameObject.SetActive(false);
        m_partsBtn5.gameObject.SetActive(false);
        non_btns.gameObject.SetActive(false);
    }

    IEnumerator ClickedParts5(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_partsBtn5.gameObject.transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_partsBtn5.gameObject.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_partsBtn5.gameObject.GetComponent<BoxCollider>().enabled = false;

        m_partsBtn5.gameObject.transform.position = end;
        m_partsBtn1.gameObject.SetActive(false);
        m_partsBtn2.gameObject.SetActive(false);
        m_partsBtn3.gameObject.SetActive(false);
        m_partsBtn4.gameObject.SetActive(false);
        non_btns.gameObject.SetActive(false);
    }
}
