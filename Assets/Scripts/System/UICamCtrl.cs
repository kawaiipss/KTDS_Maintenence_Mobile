using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamCtrl : MonoBehaviour
{

    [SerializeField] private Camera _MainCamera;

    void FixedUpdate()
    {
        if (_MainCamera != null)
        {
            transform.localPosition = _MainCamera.transform.localPosition;
            transform.localEulerAngles = _MainCamera.transform.localEulerAngles;
        }
    }
}
