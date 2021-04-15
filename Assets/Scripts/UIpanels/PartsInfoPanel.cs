using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;
using UnityEngine.Networking;

public class PartsInfoPanel : MonoBehaviour
{
    [SerializeField] private AxRButton[] m_btnPartsNew;

    [Header("ObjCtrl")]
    [SerializeField] private AxRButton m_btnRotUp;
    [SerializeField] private AxRButton m_btnRotDown;
    [SerializeField] private AxRButton m_btnRotLeft;
    [SerializeField] private AxRButton m_btnRotRight;
    [SerializeField] private AxRButton m_btnScaleUp;
    [SerializeField] private AxRButton m_btnScaleDown;
    [SerializeField] private AxRButton m_btnReturn;

    [Header("Target")]
    [SerializeField] private GameObject[] m_target;

    private Action[] m_closeBtn;
    public Action CLOSE_BTN
    {
        set
        {
            for(int i = 0; i<m_closeBtn.Length; i++)
            {
                m_closeBtn[i] = value;
            }
        }
    }
    private Action m_closeBtnSub;
    public Action CLOSE_BTNSUB { set { m_closeBtnSub = value; } }
    private Action m_partsBtn;
    public Action PARTS_BTN { set { m_partsBtn = value; } }

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

    private Action m_returnBtn;
    public Action RETURNBTN { set { m_returnBtn = value; } }

    public Text[] _InfoSummaries; 

    private Action[] m_partsBtnNew;
    public Action PARTS_BTNNEW
    {
        set
        {
            for (int i = 0; i < m_partsBtnNew.Length; i++)
            {
                m_partsBtnNew[i] = value;
            }
        }
    }
    
    public GameObject partsInfoSub;
    public Text partsInfoTxt;

    public GameObject[] non_btns;

    public Vector3[] m_partsPos;
    public Vector3[] m_partsRot;
    public Vector3[] m_partsScale;

    #region legacy 데이터 로딩
    //private Dictionary<int, InfoKeys> m_dicPartsInfo;

    //private InfoKeys m_infoKey;
    //public InfoKeys INFOKEY { get { return m_infoKey; } set { m_infoKey = value; } }
    #endregion

    //public void LoadData()
    //{
    //    if (m_dicPartsInfo != null)
    //        m_dicPartsInfo.Clear();
    //    m_dicPartsInfo = PartsInfoLoader.GetData();
    //}

    private void Awake()
    {
        m_btnRotUp.ACT_CLICK = RotUp;
        m_btnRotDown.ACT_CLICK = RotDown;
        m_btnRotLeft.ACT_CLICK = RotLeft;
        m_btnRotRight.ACT_CLICK = RotRight;
        m_btnReturn.ACT_CLICK = OnReturn;

        m_btnScaleUp.ACT_CLICK = ScaleUp;
        m_btnScaleDown.ACT_CLICK = ScaleDown;
        //partsInfoSub.SetActive(false);
        //partsInfoSub.GetComponent<Canvas>().enabled = false;

        //m_btnPartsNew[0].ACT_CLICK = OnClickPartsNew1;
        //m_btnPartsNew[1].ACT_CLICK = OnClickPartsNew2;
        //m_btnPartsNew[2].ACT_CLICK = OnClickPartsNew3;
        //m_btnPartsNew[3].ACT_CLICK = OnClickPartsNew4;
        //m_btnPartsNew[4].ACT_CLICK = OnClickPartsNew5;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[0].ACT_CLICK = OnClickParts1;
            m_btnPartsNew[1].ACT_CLICK = OnClickParts2;
            m_btnPartsNew[2].ACT_CLICK = OnClickParts3;
            m_btnPartsNew[3].ACT_CLICK = OnClickParts4;
            m_btnPartsNew[4].ACT_CLICK = OnClickParts5;
        }
        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[0].ACT_CLICK = OnClickParts1;
            m_btnPartsNew[1].ACT_CLICK = OnClickParts2;
            m_btnPartsNew[2].ACT_CLICK = OnClickParts3;
            m_btnPartsNew[3].ACT_CLICK = OnClickParts4;
            m_btnPartsNew[4].ACT_CLICK = OnClickParts5;
            m_btnPartsNew[5].ACT_CLICK = OnClickParts6;
            m_btnPartsNew[6].ACT_CLICK = OnClickParts7;
            m_btnPartsNew[7].ACT_CLICK = OnClickParts8;
        }

        for (int i = 0; i < m_btnPartsNew.Length; i++)
        {
            m_partsPos[i] = m_btnPartsNew[i].transform.localPosition;
            //m_partsPos[i] = new Vector3(m_btnPartsNew[i].transform.localPosition.x + 0.0045f, m_btnPartsNew[i].transform.localPosition.y + 0.0114f, m_btnPartsNew[i].transform.localPosition.z - 0.0006f);
            m_partsRot[i] = m_btnPartsNew[i].transform.localEulerAngles;
            m_partsScale[i] = m_btnPartsNew[i].transform.localScale;
        }
    }
    
    private void Start()
    {
        InitPartsInfo();
    }

    #region xml파일에서 파츠인포 데이터 받아오는 기능(추후 수정예정)
    //private XmlNode titleNode1, titleNode2, titleNode3, titleNode4, titleNode5;

    //public void xmlDataLoad(string FileName)
    //{
    //    TextAsset _textAsset = (TextAsset)Resources.Load(URL.xmlURL.XML_URL + "PartsInfo");
    //    XmlDocument xmlDocument = new XmlDocument();
    //    xmlDocument.LoadXml(_textAsset.text);

    //    XmlNodeList keyLists = xmlDocument.DocumentElement.SelectNodes("InfoDetail");

    //    titleNode1 = keyLists[0].SelectSingleNode("InfoText");
    //    titleNode2 = keyLists[1].SelectSingleNode("InfoText");
    //    titleNode3 = keyLists[2].SelectSingleNode("InfoText");
    //    titleNode4 = keyLists[5].SelectSingleNode("InfoText");
    //    titleNode5 = keyLists[6].SelectSingleNode("InfoText");
    //}
    #endregion

    private void OnEnable()
    {
        //xmlDataLoad("PartsInfo");
        //LoadData();
    }

    //public void OnClickParts(AxRButton _button)
    //{
    //    if (!partsInfoSub.activeSelf)
    //    {
    //        partsInfoSub.SetActive(true);
    //        if (CloseFrame.activeSelf)
    //            CloseFrame.SetActive(false);
    //    }

    //    //if (INFOKEY == null)
    //    //    return;
    //    //partsInfoTxt[0].text = INFOKEY.key;
    //    //partsInfoTxt[1].text = INFOKEY.key;
        

    //    Debug.Log("파츠 클릭!!");
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
            for(int i = 0; i < m_target.Length; i++)
            {
                m_target[i].transform.localScale = Vector3.Lerp(m_target[i].transform.localScale,
                new Vector3(m_target[i].transform.localScale.x * 1.1f,
                m_target[i].transform.localScale.y * 1.1f, m_target[i].transform.localScale.z * 1.1f), Time.deltaTime);

                if (m_target[i].transform.localScale.x > 0.25f)
                    m_target[i].transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            
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
            for(int i = 0; i<m_target.Length; i++)
            {
                m_target[i].transform.localScale = Vector3.Lerp(m_target[i].transform.localScale,
                new Vector3(m_target[i].transform.localScale.x * 0.9f,
                m_target[i].transform.localScale.y * 0.9f, m_target[i].transform.localScale.z * 0.9f), Time.deltaTime);

                if (m_target[i].transform.localScale.x < 0.15f)
                    m_target[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }
            
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
            for(int i = 0; i < m_target.Length; i++)
            {
                //m_target[i].transform.localEulerAngles = Vector3.Lerp(m_target[i].transform.localEulerAngles,
                //new Vector3(m_target[i].transform.localEulerAngles.x - 30.0f,
                //m_target[i].transform.localEulerAngles.y, m_target[i].transform.localEulerAngles.z), Time.deltaTime);
                m_target[i].transform.Rotate(Vector3.up*0.4f);
            }
            
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
            for(int i = 0; i < m_target.Length; i++)
            {
                //m_target[i].transform.localEulerAngles = Vector3.Lerp(m_target[i].transform.localEulerAngles,
                //new Vector3(m_target[i].transform.localEulerAngles.x + 30.0f,
                //m_target[i].transform.localEulerAngles.y, m_target[i].transform.localEulerAngles.z), Time.deltaTime);
                m_target[i].transform.Rotate(Vector3.down * 0.4f);
            }
            
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
            for(int i =0; i<m_target.Length; i++)
            {
               m_target[i].transform.localEulerAngles = Vector3.Lerp(m_target[i].transform.localEulerAngles,
               new Vector3(m_target[i].transform.localEulerAngles.x,
               m_target[i].transform.localEulerAngles.y + 30.0f, m_target[i].transform.localEulerAngles.z), Time.deltaTime);
            }
           
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
            for(int i = 0; i<m_target.Length; i++)
            {
                m_target[i].transform.localEulerAngles = Vector3.Lerp(m_target[i].transform.localEulerAngles,
                new Vector3(m_target[i].transform.localEulerAngles.x,
                m_target[i].transform.localEulerAngles.y - 30.0f, m_target[i].transform.localEulerAngles.z), Time.deltaTime);
            }
            
        }
        Debug.Log("로테잇라이트!!");
    }

    #region 모델링 제어 기능
    void OnClickParts1(AxRButton _button)
    {
        StartCoroutine(ClickedParts1(1.0f, new Vector3(m_btnPartsNew[0].gameObject.transform.localPosition.x, m_btnPartsNew[0].gameObject.transform.localPosition.y, m_btnPartsNew[0].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts2(AxRButton _button)
    {
        StartCoroutine(ClickedParts2(1.0f, new Vector3(m_btnPartsNew[1].gameObject.transform.localPosition.x, m_btnPartsNew[1].gameObject.transform.localPosition.y, m_btnPartsNew[1].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts3(AxRButton _button)
    {
        StartCoroutine(ClickedParts3(1.0f, new Vector3(m_btnPartsNew[2].gameObject.transform.localPosition.x, m_btnPartsNew[2].gameObject.transform.localPosition.y, m_btnPartsNew[2].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts4(AxRButton _button)
    {
        StartCoroutine(ClickedParts4(1.0f, new Vector3(m_btnPartsNew[3].gameObject.transform.localPosition.x, m_btnPartsNew[3].gameObject.transform.localPosition.y, m_btnPartsNew[3].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts5(AxRButton _button)
    {
        StartCoroutine(ClickedParts5(1.0f, new Vector3(m_btnPartsNew[4].gameObject.transform.localPosition.x, m_btnPartsNew[4].gameObject.transform.localPosition.y, m_btnPartsNew[4].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts6(AxRButton _button)
    {
        StartCoroutine(ClickedParts6(1.0f, new Vector3(m_btnPartsNew[5].gameObject.transform.localPosition.x, m_btnPartsNew[5].gameObject.transform.localPosition.y, m_btnPartsNew[5].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts7(AxRButton _button)
    {
        StartCoroutine(ClickedParts7(1.0f, new Vector3(m_btnPartsNew[6].gameObject.transform.localPosition.x, m_btnPartsNew[6].gameObject.transform.localPosition.y, m_btnPartsNew[6].gameObject.transform.localPosition.z - 0.04f)));
    }

    void OnClickParts8(AxRButton _button)
    {
        StartCoroutine(ClickedParts8(1.0f, new Vector3(m_btnPartsNew[7].gameObject.transform.localPosition.x, m_btnPartsNew[7].gameObject.transform.localPosition.y, m_btnPartsNew[7].gameObject.transform.localPosition.z - 0.04f)));
    }

    //Animator animator;

    IEnumerator ClickedParts1(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[0].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[0].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[0].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[0].gameObject.transform.localPosition = end;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
        }

        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
            m_btnPartsNew[5].gameObject.SetActive(false);
            m_btnPartsNew[6].gameObject.SetActive(false);
            m_btnPartsNew[7].gameObject.SetActive(false);
        }

        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        if (_InfoSummaries.Length == 5)
        {
            _InfoSummaries[0].gameObject.SetActive(true);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
        }

        else if (_InfoSummaries.Length == 8)
        {
            _InfoSummaries[0].gameObject.SetActive(true);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
            _InfoSummaries[5].gameObject.SetActive(false);
            _InfoSummaries[6].gameObject.SetActive(false);
            _InfoSummaries[7].gameObject.SetActive(false);
        }

        //animator = m_animModel.GetComponent<Animator>();
        //animator.SetBool("isPartsClicked", true);
    }

    IEnumerator ClickedParts2(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[1].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[1].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[1].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[1].gameObject.transform.localPosition = end;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
        }

        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
            m_btnPartsNew[5].gameObject.SetActive(false);
            m_btnPartsNew[6].gameObject.SetActive(false);
            m_btnPartsNew[7].gameObject.SetActive(false);
        }


        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        if (_InfoSummaries.Length == 5)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(true);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
        }

        else if (_InfoSummaries.Length == 8)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(true);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
            _InfoSummaries[5].gameObject.SetActive(false);
            _InfoSummaries[6].gameObject.SetActive(false);
            _InfoSummaries[7].gameObject.SetActive(false);
        }


    }

    IEnumerator ClickedParts3(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[2].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[2].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[2].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[2].gameObject.transform.localPosition = end;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
        }

        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
            m_btnPartsNew[5].gameObject.SetActive(false);
            m_btnPartsNew[6].gameObject.SetActive(false);
            m_btnPartsNew[7].gameObject.SetActive(false);
        }


        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        if (_InfoSummaries.Length == 5)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(true);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
        }

        else if (_InfoSummaries.Length == 8)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(true);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(false);
            _InfoSummaries[5].gameObject.SetActive(false);
            _InfoSummaries[6].gameObject.SetActive(false);
            _InfoSummaries[7].gameObject.SetActive(false);
        }


    }

    IEnumerator ClickedParts4(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[3].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[3].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[3].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[3].gameObject.transform.localPosition = end;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
        }

        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[4].gameObject.SetActive(false);
            m_btnPartsNew[5].gameObject.SetActive(false);
            m_btnPartsNew[6].gameObject.SetActive(false);
            m_btnPartsNew[7].gameObject.SetActive(false);
        }

        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        if (_InfoSummaries.Length == 5)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(true);
            _InfoSummaries[4].gameObject.SetActive(false);
        }

        else if (_InfoSummaries.Length == 8)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(true);
            _InfoSummaries[4].gameObject.SetActive(false);
            _InfoSummaries[5].gameObject.SetActive(false);
            _InfoSummaries[6].gameObject.SetActive(false);
            _InfoSummaries[7].gameObject.SetActive(false);
        }


    }

    IEnumerator ClickedParts5(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[4].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[4].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[4].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[4].gameObject.transform.localPosition = end;

        if (m_btnPartsNew.Length == 5)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
        }

        else if (m_btnPartsNew.Length == 8)
        {
            m_btnPartsNew[0].gameObject.SetActive(false);
            m_btnPartsNew[1].gameObject.SetActive(false);
            m_btnPartsNew[2].gameObject.SetActive(false);
            m_btnPartsNew[3].gameObject.SetActive(false);
            m_btnPartsNew[5].gameObject.SetActive(false);
            m_btnPartsNew[6].gameObject.SetActive(false);
            m_btnPartsNew[7].gameObject.SetActive(false);
        }


        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);


        if (_InfoSummaries.Length == 5)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(true);
        }

        else if (_InfoSummaries.Length == 8)
        {
            _InfoSummaries[0].gameObject.SetActive(false);
            _InfoSummaries[1].gameObject.SetActive(false);
            _InfoSummaries[2].gameObject.SetActive(false);
            _InfoSummaries[3].gameObject.SetActive(false);
            _InfoSummaries[4].gameObject.SetActive(true);
            _InfoSummaries[5].gameObject.SetActive(false);
            _InfoSummaries[6].gameObject.SetActive(false);
            _InfoSummaries[7].gameObject.SetActive(false);
        }

    }

    IEnumerator ClickedParts6(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[5].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[5].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[5].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[5].gameObject.transform.localPosition = end;
        m_btnPartsNew[0].gameObject.SetActive(false);
        m_btnPartsNew[1].gameObject.SetActive(false);
        m_btnPartsNew[2].gameObject.SetActive(false);
        m_btnPartsNew[3].gameObject.SetActive(false);
        m_btnPartsNew[4].gameObject.SetActive(false);
        m_btnPartsNew[6].gameObject.SetActive(false);
        m_btnPartsNew[7].gameObject.SetActive(false);
        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        _InfoSummaries[0].gameObject.SetActive(false);
        _InfoSummaries[1].gameObject.SetActive(false);
        _InfoSummaries[2].gameObject.SetActive(false);
        _InfoSummaries[3].gameObject.SetActive(false);
        _InfoSummaries[4].gameObject.SetActive(false);
        _InfoSummaries[5].gameObject.SetActive(true);
        _InfoSummaries[6].gameObject.SetActive(false);
        _InfoSummaries[7].gameObject.SetActive(false);
    }

    IEnumerator ClickedParts7(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[6].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[6].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[6].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[6].gameObject.transform.localPosition = end;
        m_btnPartsNew[0].gameObject.SetActive(false);
        m_btnPartsNew[1].gameObject.SetActive(false);
        m_btnPartsNew[2].gameObject.SetActive(false);
        m_btnPartsNew[3].gameObject.SetActive(false);
        m_btnPartsNew[4].gameObject.SetActive(false);
        m_btnPartsNew[5].gameObject.SetActive(false);
        m_btnPartsNew[7].gameObject.SetActive(false);

        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        _InfoSummaries[0].gameObject.SetActive(false);
        _InfoSummaries[1].gameObject.SetActive(false);
        _InfoSummaries[2].gameObject.SetActive(false);
        _InfoSummaries[3].gameObject.SetActive(false);
        _InfoSummaries[4].gameObject.SetActive(false);
        _InfoSummaries[5].gameObject.SetActive(false);
        _InfoSummaries[6].gameObject.SetActive(true);
        _InfoSummaries[7].gameObject.SetActive(false);
    }

    IEnumerator ClickedParts8(float duration, Vector3 end)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 start = m_btnPartsNew[7].gameObject.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            m_btnPartsNew[7].gameObject.transform.localPosition = Vector3.Lerp(start, end, elapsed / duration);
            yield return wait;
        }

        m_btnPartsNew[7].gameObject.GetComponent<BoxCollider>().enabled = false;

        m_btnPartsNew[7].gameObject.transform.localPosition = end;
        m_btnPartsNew[0].gameObject.SetActive(false);
        m_btnPartsNew[1].gameObject.SetActive(false);
        m_btnPartsNew[2].gameObject.SetActive(false);
        m_btnPartsNew[3].gameObject.SetActive(false);
        m_btnPartsNew[4].gameObject.SetActive(false);
        m_btnPartsNew[5].gameObject.SetActive(false);
        m_btnPartsNew[6].gameObject.SetActive(false);
        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(false);
        }

        partsInfoSub.SetActive(true);

        _InfoSummaries[0].gameObject.SetActive(false);
        _InfoSummaries[1].gameObject.SetActive(false);
        _InfoSummaries[2].gameObject.SetActive(false);
        _InfoSummaries[3].gameObject.SetActive(false);
        _InfoSummaries[4].gameObject.SetActive(false);
        _InfoSummaries[5].gameObject.SetActive(false);
        _InfoSummaries[6].gameObject.SetActive(false);
        _InfoSummaries[7].gameObject.SetActive(true);
    }
    #endregion

    private void InitPartsInfo()
    {
        partsInfoSub.SetActive(false);
        for (int i = 0; i < _InfoSummaries.Length; i++)
            _InfoSummaries[i].gameObject.SetActive(false);
        for (int i = 0; i < m_btnPartsNew.Length; i++)
        {
            if (!m_btnPartsNew[i].gameObject.activeSelf)
            {
                m_btnPartsNew[i].gameObject.SetActive(true);
            }
            m_btnPartsNew[i].gameObject.transform.localPosition = m_partsPos[i];
            m_btnPartsNew[i].transform.localEulerAngles = m_partsRot[i];
            m_btnPartsNew[i].transform.localScale = m_partsScale[i];
            if(!m_btnPartsNew[i].GetComponent<BoxCollider>().enabled)
            {
                m_btnPartsNew[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
        for (int i = 0; i < non_btns.Length; i++)
        {
            non_btns[i].gameObject.SetActive(true);
        }
    }
    
    public void OnReturn(AxRButton _button)
    {
        InitPartsInfo();
    }
}
