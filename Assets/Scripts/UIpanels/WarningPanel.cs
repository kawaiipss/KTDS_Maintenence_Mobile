using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    [SerializeField] private AxRButton m_btnStart;
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    private Action m_startBtn;
    public Action START_BTN { set { m_startBtn = value; } }

    //public GameObject[] guideTxt;

    private RecogQR m_QRRecog;
    public GameObject warningContent;

    private void Awake()
    {
        if (m_audioSource == null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }
        m_btnStart.ACT_CLICK = OnStart;
        //for (int i = 0; i < guideTxt.Length; i++)
        //{
        //    guideTxt[i].SetActive(false);
        //}
        //tabIcon.gameObject.SetActive(false);
    }

    private void Start()
    {
        m_btnStart.gameObject.SetActive(true);
        warningContent.SetActive(true);
        //guideTxt[0].SetActive(true);
    }
    
    public void OnStart(AxRButton _button)
    {
        StartCoroutine(ClickSound());
        MainSystem.INSTANCE.StartQRRecog(); //2
        StartCoroutine(StartRecog());
    }

    IEnumerator ClickSound()
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
        yield return null;
    }

    IEnumerator StartRecog()
    {
        yield return ClickSound();
        Destroy(gameObject);
    }
}

