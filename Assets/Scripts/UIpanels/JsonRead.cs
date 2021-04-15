using System;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class JsonRead : MonoBehaviour
{
    public Text[] JSON_txt_Data;

    [SerializeField] private Image[] m_innerPanel;
    [SerializeField] private GameObject[] m_detailInfoTitle;

    [Header("Buttons")]
    [SerializeField] private AxRButton m_btnClose;
    [SerializeField] private AxRButton m_btnNext;
    [SerializeField] private AxRButton m_btnPrev;

    [Header("Text")]
    [SerializeField] private Text m_pageNum;

    private Action m_closeBtn;
    public Action CLOSE_BTN { set { m_closeBtn = value; } }
    private Action m_nextBtn;
    public Action NEXT_BTN { set { m_nextBtn = value; } }
    private Action m_prevBtn;
    public Action PREV_BTN { set { m_prevBtn = value; } }

    private int num_of_page;
    private int cur_page = 1;

    private string JsonString;

    private void Awake()
    {
        //JsonData = File.ReadAllText(Application.dataPath + "/Resources/Json/SampleJsonData.json");
        JsonString = Resources.Load<TextAsset>("Json/MSPP_General").text;
        
        LoadJSONdata();
        m_btnClose.ACT_CLICK = Onclose;
        m_btnNext.ACT_CLICK = OnClickNext;
        m_btnPrev.ACT_CLICK = OnClickPrev;
        num_of_page = 3;
    }
    
    void Start()
    {
        SetManualPanel();
    }

    void SetManualPanel()
    {
        m_pageNum.text = (cur_page + "  I  " + num_of_page);
        for (int i = 1; i < m_innerPanel.Length; i++)
            m_innerPanel[i].enabled = false;
        for (int j = 1; j < m_detailInfoTitle.Length; j++)
            m_detailInfoTitle[j].SetActive(false);
    }

    public void LoadJSONdata()
    {
        //StartCoroutine(JsonCoroutine());

        //string JsonData = File.ReadAllText(Application.dataPath + "/Resources/Json/SampleJsonData.json");
        //Debug.Log(JsonString);

        JsonData sampleData = JsonMapper.ToObject(JsonString);
        
        GetSampleData(sampleData);
    }
    
    private void GetSampleData(JsonData name)
    {
        for(int i = 0; i < JSON_txt_Data.Length; i++)
        {
            JSON_txt_Data[i].GetComponent<Text>().enabled = false;
        }

        for(int i = 0; i < name.Count; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (!JSON_txt_Data[j].GetComponent<Text>().enabled)
                    JSON_txt_Data[j].GetComponent<Text>().enabled = true;
            }
            //1페이지
            JSON_txt_Data[0].text = name[i]["nescode"].ToString();
            JSON_txt_Data[1].text = name[i]["officeName"].ToString();
            JSON_txt_Data[2].text = name[i]["instlocation"].ToString();
            JSON_txt_Data[3].text = name[i]["neAlias"].ToString();
            JSON_txt_Data[4].text = name[i]["neClass"].ToString();
            JSON_txt_Data[5].text = name[i]["modelName"].ToString();

            //2페이지
            JSON_txt_Data[6].text = name[i]["addr"].ToString();
            JSON_txt_Data[7].text = name[i]["detailAddr"].ToString();
            JSON_txt_Data[8].text = name[i]["instBldg"].ToString();
            JSON_txt_Data[9].text = name[i]["roadAddr"].ToString();

            //3페이지
            JSON_txt_Data[10].text = name[i]["equipArrangeDetail"]["system"][0].ToString();
            JSON_txt_Data[11].text = name[i]["equipArrangeDetail"]["unitName"][0].ToString();
            JSON_txt_Data[12].text = name[i]["equipArrangeDetail"]["transciever"][0].ToString();
            JSON_txt_Data[13].text = name[i]["equipArrangeDetail"]["usingState"][0].ToString();
            JSON_txt_Data[14].text = name[i]["equipArrangeDetail"]["carrierTransfer"][0].ToString();

            //타이틀
            //JSON_txt_Data[11].text = name[i]["neType"].ToString() + " 장치정보";
        }
    }

    private void ShowSampleData(JsonData name)
    {
        switch(cur_page)
        {
            case 1:
                for(int i = 0; i < 6; i++)
                {
                    if (!JSON_txt_Data[i].GetComponent<Text>().enabled)
                        JSON_txt_Data[i].GetComponent<Text>().enabled = true;
                }
                for(int j = 6; j < JSON_txt_Data.Length -1; j++)
                {
                    if (JSON_txt_Data[j].GetComponent<Text>().enabled)
                        JSON_txt_Data[j].GetComponent<Text>().enabled = false;
                }
                for (int k = 10; k < JSON_txt_Data.Length; k++)
                {
                    if (JSON_txt_Data[k].GetComponent<Text>().enabled)
                        JSON_txt_Data[k].GetComponent<Text>().enabled = false;
                }
                Debug.Log(cur_page.ToString() + "페이지");
                break;
            case 2:
                for(int i = 6; i < JSON_txt_Data.Length - 5; i++)
                {
                    if (!JSON_txt_Data[i].GetComponent<Text>().enabled)
                        JSON_txt_Data[i].GetComponent<Text>().enabled = true;
                }
                for(int j = 0; j < 6; j++)
                {
                    if (JSON_txt_Data[j].GetComponent<Text>().enabled)
                        JSON_txt_Data[j].GetComponent<Text>().enabled = false;
                }
                
                for (int k = 10; k < JSON_txt_Data.Length; k++)
                {
                    if (JSON_txt_Data[k].GetComponent<Text>().enabled)
                        JSON_txt_Data[k].GetComponent<Text>().enabled = false;
                }
                Debug.Log(cur_page.ToString() + "페이지");
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    if (JSON_txt_Data[i].GetComponent<Text>().enabled)
                        JSON_txt_Data[i].GetComponent<Text>().enabled = false;
                }
                for (int j = 6; j < JSON_txt_Data.Length - 1; j++)
                {
                    if (JSON_txt_Data[j].GetComponent<Text>().enabled)
                        JSON_txt_Data[j].GetComponent<Text>().enabled = false;
                }
                for (int k = 10; k < JSON_txt_Data.Length; k++)
                {
                    if (!JSON_txt_Data[k].GetComponent<Text>().enabled)
                        JSON_txt_Data[k].GetComponent<Text>().enabled = true;
                }
                Debug.Log(cur_page.ToString() + "페이지");
                break;
        }
        for(int i = 0; i < m_innerPanel.Length; i++)
        {
            if (i + 1 == cur_page)
                m_innerPanel[i].enabled = true;
            else
                m_innerPanel[i].enabled = false;
        }
        for(int j = 0; j < m_detailInfoTitle.Length; j++)
        {
            if (j + 1 == cur_page)
                m_detailInfoTitle[j].SetActive(true);
            else
                m_detailInfoTitle[j].SetActive(false);
        }
    }

    public void OnClickNext(AxRButton _button)
    {
        if (m_nextBtn != null)
            m_nextBtn();
        if (cur_page < num_of_page)
            cur_page += 1;
        m_pageNum.text = (cur_page + "  I  " + num_of_page);

        JsonData sampleData = JsonMapper.ToObject(JsonString);
        ShowSampleData(sampleData);

        Debug.Log("Next!!");
        Debug.Log("Cur_page = " + cur_page);
    }

    public void OnClickPrev(AxRButton _button)
    {
        if (m_prevBtn != null)
            m_prevBtn();
        if (cur_page > 1)
            cur_page -= 1;
        m_pageNum.text = (cur_page + "  I  " + num_of_page);

        JsonData sampleData = JsonMapper.ToObject(JsonString);
        ShowSampleData(sampleData);

        Debug.Log("Prev!!");
        Debug.Log("Cur_page = " + cur_page);
    }

    public void Onclose(AxRButton _button)
    {
        //if (m_closeBtn != null)
        //    m_closeBtn();
        //Debug.Log("NeOSS Off!!");

        //ControlPanal m_controlPanel = GameObject.Find("Control_Panel").GetComponent<ControlPanal>();
        //if (!m_controlPanel.btn_NeOSSpos.gameObject.activeSelf)
        //    m_controlPanel.btn_NeOSSpos.gameObject.SetActive(true);

        //transform.position = new Vector3(-1.5f, 0.275f, 1.0f);

        ////Destroy(this.gameObject);
    }
}
