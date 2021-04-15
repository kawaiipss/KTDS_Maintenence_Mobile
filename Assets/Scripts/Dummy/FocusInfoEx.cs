using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HoloToolkit.Unity.InputModule
{
    public class FocusInfoEx : MonoBehaviour, IFocusable
    {
        public void OnFocusEnter()
        {
            //Debug.Log("Focus!!");
            StartCoroutine(OnFocusOn(3.0f));
        }

        public void OnFocusExit()
        {
            Debug.Log("Focus Out!!");
        }

        IEnumerator OnFocusOn(float _time)
        {
            yield return new WaitForSeconds(_time);
            Debug.Log("Focusing!!");
        }
    }
}


