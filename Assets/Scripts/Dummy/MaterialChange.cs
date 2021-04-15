using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MaterialChange : MonoBehaviour, IFocusable
{
    public Material[] materials;

    void Awake()
    {
        materials[0] = Resources.Load("dummy1", typeof(Material)) as Material;
        materials[1] = Resources.Load("dummy2", typeof(Material)) as Material;
    }

    void Start()
    {

    }

    public void OnFocusEnter()
    {

    }

    public void OnFocusExit()
    {

    }

    void IFocusable.OnFocusEnter()
    {
        throw new NotImplementedException();
    }

    void IFocusable.OnFocusExit()
    {
        throw new NotImplementedException();
    }
}
