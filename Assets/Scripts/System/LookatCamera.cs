using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    public enum type {
        forward,
        back,
        left,
        right,
        up,
        down

    }

    public type ty = type.forward;

    private Transform camera_pos;
    // Start is called before the first frame update
    void Start()
    {
        camera_pos = MainSystem.INSTANCE.CAMERA_MAIN.transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (camera_pos != null)
        {
            this.transform.LookAt(camera_pos.localPosition);
            //switch (ty)
            //{
            //    case type.forward:
            //        //this.transform.LookAt(camera_pos, Vector3.forward);
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.forward);
            //        break;
            //    case type.back:
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.forward);
            //        //this.transform.LookAt(camera_pos, -Vector3.back);
            //        break;
            //    case type.left:
            //        this.transform.LookAt(camera_pos, Vector3.left);
            //        break;
            //    case type.right:
            //        this.transform.LookAt(camera_pos, Vector3.right);
            //        break;
            //    case type.up:
            //        this.transform.LookAt(camera_pos, Vector3.up);
            //        break;
            //    case type.down:
            //        //this.transform.LookAt(camera_pos, Vector3.down);
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.forward);

            //        break;
            //}
            //switch (ty)
            //{
            //    case type.forward:
            //        //this.transform.LookAt(camera_pos, Vector3.forward);
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.forward);
            //        break;
            //    case type.back:
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.back);
            //        //this.transform.LookAt(camera_pos, -Vector3.back);
            //        break;
            //    case type.left:
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.left);
            //        break;
            //    case type.right:
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.right);
            //        break;
            //    case type.up:
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.up);
            //        break;
            //    case type.down:
            //        //this.transform.LookAt(camera_pos, Vector3.down);
            //        this.transform.localRotation.SetLookRotation(camera_pos.localPosition, Vector3.down);

            //        break;
            //}
            //this.transform.rotation.SetLookRotation(camera_pos.position);

        }
    }
}
