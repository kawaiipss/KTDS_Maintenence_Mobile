using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PartsInfoLoader
{
    private static PartsInfoLoader m_pInfoLoader;
    public static PartsInfoLoader PINFOLOADER
    {
        get
        {
            if(m_pInfoLoader == null)
            {
                m_pInfoLoader = new PartsInfoLoader();
            }
            return m_pInfoLoader;
        }
    }

    private static XmlDocument xmlDoc;

    private static void LoadXml()
    {
        TextAsset xmlAsset = (TextAsset)Resources.Load(URL.xmlURL.XML_URL + "PartsInfo");
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlAsset.text);
#if UNITY_EDITOR
        //Debug.Log("Call by LoadData() XML : " + xmlAsset.text);
#endif
    }

    public static Dictionary<int, InfoKeys> GetData()
    {
        if(null == xmlDoc)
        {
            LoadXml();
        }

        XmlElement Element = xmlDoc.DocumentElement;
        int nSize = Element.ChildNodes.Count;
        XmlNode node;
        XmlAttributeCollection attrColl;
        XmlAttribute _attrColl;

        Dictionary<int, InfoKeys> _dicReturn = new Dictionary<int, InfoKeys>();

        for(int i = 0; i < nSize; i++)
        {
            node = Element.ChildNodes[i];
            attrColl = node.Attributes;

            InfoKeys infoKeys = new InfoKeys();

            _attrColl = attrColl["idx"];
            if(_attrColl != null)
            {
                infoKeys.idx = Convert.ToInt32(_attrColl.Value);
                Debug.Log("Call by PartsInfoLoader idx: " + infoKeys.idx.ToString());
            }

            _attrColl = attrColl["key"];
            if(_attrColl != null)
            {
                infoKeys.key = _attrColl.Value;
                Debug.Log("Call by PartsInfoLoader key: " + infoKeys.idx.ToString());
            }

            _dicReturn.Add(infoKeys.idx, infoKeys);
            infoKeys = null;
        }
        return _dicReturn;
    }
}
