using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{
    public class TutorialBtn : MonoBehaviour, IFocusable
    {
        public GameObject[] guideMsg;
        public Image tabIcon;

        public void OnEnable()
        {
            guideMsg[0].SetActive(true);
            guideMsg[1].SetActive(false);
            tabIcon.gameObject.SetActive(false);
        }

        public void OnFocusEnter()
        {
            guideMsg[0].SetActive(false);
            guideMsg[1].SetActive(true);
            if(!tabIcon.gameObject.activeSelf)
            {
                tabIcon.gameObject.SetActive(true);
            }
        }

        public void OnFocusExit()
        {
            guideMsg[0].SetActive(true);
            guideMsg[1].SetActive(false);
            if (tabIcon.gameObject.activeSelf)
            {
                tabIcon.gameObject.SetActive(false);
            }
        }
    }
}

