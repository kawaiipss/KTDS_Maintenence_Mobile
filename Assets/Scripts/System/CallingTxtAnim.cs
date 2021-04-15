using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallingTxtAnim : MonoBehaviour
{
    public float delay;
    private float Skip_delay;
    private int cnt;

    public string[] fulltext;
    private int dialog_cnt = 1;
    string currentText;

    private bool text_exit;
    private bool text_full;
    private bool text_cut;

    public float playtime;

    public GameObject CircleAnimMgr;

    private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;

    private GameObject m_remoteProcess;

    private void Awake()
    {
        if (m_audioSource == null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
            m_audioSource.loop = true;
        }
    }

    private void Start()
    {
        m_remoteProcess = GameObject.Find("REMOTE_MAIN");
    }

    private void OnEnable()
    {
        GetTyping(dialog_cnt, fulltext);
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        CircleAnimMgr.GetComponent<Animation>().Play();
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
    }

    //private void Update()
    //{
    //    //추후 통화연결되면 애니메이션 종료하도록 변경
    //    //if (text_exit)
    //    //    gameObject.SetActive(false);
    //    if (CRemoteProcess.Instance.mRemoteSupportMode)
    //    {
    //        gameObject.SetActive(false);
    //        if (CircleAnimMgr.GetComponent<Animation>().isPlaying)
    //        {
    //            CircleAnimMgr.GetComponent<Animation>().Stop();
    //        }
    //        if (m_audioSource.isPlaying)
    //        {
    //            m_audioSource.Stop();
    //        }
    //    }
    //}

    public void EndTyping()
    {
        if (text_full)
        {
            cnt++;
            text_full = false;
            text_cut = false;
            StartCoroutine(ShowText(fulltext));
        }
        else
            text_cut = true;
    }

    public void GetTyping(int _dialog_cnt, string[] _fullText)
    {
        text_exit = false;
        text_full = false;
        text_cut = false;
        cnt = 0;

        dialog_cnt = _dialog_cnt;
        fulltext = new string[dialog_cnt];
        fulltext = _fullText;

        StartCoroutine(ShowText(fulltext));
        //[SSPARK] 원격 연결이 되지 않아도 저절로 꺼지지 않도록 코루틴 호출 끔
        //StartCoroutine(ShowTextStop(playtime));
    }

    IEnumerator ShowText(string[] _fullText)
    {
        if(cnt >= dialog_cnt)
        {
            text_exit = true;
            StopCoroutine("ShowText");
        }
        else
        {
            while(true)
            {
                currentText = "";

                for (int i = 0; i < _fullText[cnt].Length; i++)
                {
                    if (text_cut == true)
                    {
                        break;
                    }

                    currentText = _fullText[cnt].Substring(0, i + 1);
                    this.GetComponent<Text>().text = currentText;
                    yield return new WaitForSeconds(delay);
                }

                //Debug.Log("Typing 종료");
                //this.GetComponent<Text>().text = _fullText[cnt];
                //yield return new WaitForSeconds(Skip_delay);

                text_full = true;
            }
            
        }
    }

    IEnumerator ShowTextStop(float _playtime)
    {
        yield return new WaitForSeconds(_playtime);
        StopAllCoroutines();
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        if (CircleAnimMgr.GetComponent<Animation>().isPlaying)
            CircleAnimMgr.GetComponent<Animation>().Stop();
    }
}
