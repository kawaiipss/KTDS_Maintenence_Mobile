using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Camera m_MainCamera;

    private const float sensitivityX = 1.0f;
    private const float sensitivityY = 1.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private const float m_MinRot_X = -360F;
    private const float m_MaxRot_X = 360F;
    private const float m_MinRot_Y = -60F;
    private const float m_MaxRot_Y = 60F;
    Quaternion originalRotation;
    private void Start()
    {
        if (m_MainCamera == null)
            m_MainCamera = Camera.main;
    }
    void CameraAngleControl()
    {
        originalRotation = Quaternion.identity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        rotationY = ClampAngle(rotationY, m_MinRot_Y, m_MaxRot_Y);
        rotationX = ClampAngle(rotationX, m_MinRot_X, m_MaxRot_X);

        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);

        m_MainCamera.transform.localRotation = originalRotation * xQuaternion * yQuaternion;

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % m_MaxRot_X;
        if ((angle >= m_MinRot_X) && (angle <= m_MaxRot_X))
        {
            if (angle < m_MinRot_X)
            {
                angle += m_MaxRot_X;
            }
            if (angle > m_MaxRot_X)
            {
                angle -= m_MaxRot_X;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
#if UNITY_EDITOR
    private void FixedUpdate()
    {
        if (!CursorControl.CURSOR_VISIBLE)
            CameraAngleControl();
    }
#endif
}