using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UWPxmlEx : MonoBehaviour
{
    private void Start()
    {
        XmlRead();
    }

    void XmlRead()
    {
        TextAsset xmlAsset;
        xmlAsset = (TextAsset)Resources.Load(URL.xmlURL.XML_URL + "PartsInfo");
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(xmlAsset.text);
        Debug.Log(xmlAsset.ToString());

        //XmlElement Element = xDoc.DocumentElement;
        //int nSize = Element.ChildNodes.Count;

        //XmlNode node;
        //XmlAttributeCollection attrCol;
        //XmlAttribute xmlAttr;

        //for(int i = 0; i< nSize; i++)
        //{
        //    node = Element.ChildNodes[i];
        //    attrCol = node.Attributes;
        //    xmlAttr = attrCol["InfoDetail"];
        //    Debug.Log(xmlAttr.ToString());
        //}
    }
}
