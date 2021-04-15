//using UnityEngine;
//using System.Collections;
//using LitJson;
//using System.IO;
//using System.Collections.Generic;

////http://lbv.github.io/litjson/  JSon Dll 받는 곳. 
//public class Item

//{
//    public int ID;
//    public string Name;
//    public string Dis;

//    public Item(int id, string name, string dis)
//    {
//        ID = id;
//        Name = name;
//        Dis = dis;

//    }


//}

//public class JsonManager : MonoBehaviour {


//    public List<Item> itemList = new List<Item>();

//    public List<Item> MyInventory  = new List<Item>() ;
//	// Use this for initialization
//	void Start () {
//        itemList.Add(new Item(0, "검", "검이다."));
//        itemList.Add(new Item(1, "바지", "바지이다."));
//        itemList.Add(new Item(2, "돈", "돈이다."));

//    }



//    public void SaveBtn()
//    {

//        StartCoroutine(SaveItemData());
//    }


//    IEnumerator SaveItemData()
//    {
//        JsonData ItemJson = JsonMapper.ToJson(itemList);
//        // 새 파일을 만들고 지정된 문자열을 파일에 쓴다음 닫습니다.  대상 파일이 이미 있으면 덮어씁니다. 
//        File.WriteAllText(Application.dataPath + "/Resource/Item.json", ItemJson.ToString());
//        Debug.Log(ItemJson);
        
//        yield return null;
//    }

//    // 불러오기 .. 
//    public void LoadBtn()
//    {
//        StartCoroutine(LoadItemData());


//    }


//    IEnumerator LoadItemData()
//    {

//        string Jsonstring = File.ReadAllText(Application.dataPath + "/Resource/Item.json");
//        Debug.Log(Jsonstring);

//        JsonData itemData = JsonMapper.ToObject(Jsonstring);
//        // Debug.Log(itemData)
//        // 
//        GetItem(itemData);
//        yield return null;
//    }

//    private void  GetItem(JsonData name)
//    {

//        for (int i = 0; i < name.Count; i++)
//        {
//            Debug.Log(name[i]["ID"]);
//            string TmpID = name[i]["ID"].ToString();

//            for (int j = 0; j < itemList.Count; j++)
//            {
//                if (TmpID == itemList[j].ID.ToString())
//                {
//                    MyInventory.Add(itemList[j]);
//                }

//            }
            
//        }



//        for (int i = 0; i < MyInventory.Count; i++)
//        {
//            Debug.Log("불러오기 성공!"+ MyInventory[i].Name);
        
//        }



//    }

//}


