using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace HoloToolkit.Unity.InputModule
{
    public class DragAndDrop : MonoBehaviour, IFocusable, IInputHandler, ISourceStateHandler
    {
        public static Vector2 defaultposition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            defaultposition = this.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 currentPos = Input.mousePosition;
            this.transform.position = currentPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = defaultposition;
        }

        public void OnFocusEnter()
        {
            throw new NotImplementedException();
        }

        public void OnFocusExit()
        {
            throw new NotImplementedException();
        }

        public void OnInputDown(InputEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnInputUp(InputEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnSourceDetected(SourceStateEventData eventData)
        {
            throw new NotImplementedException();
        }

        public void OnSourceLost(SourceStateEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}


