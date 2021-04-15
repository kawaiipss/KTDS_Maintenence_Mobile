using UnityEngine;

public class CursorControl : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private static bool CursorVisible = false;
    public static bool CURSOR_VISIBLE { get { return CursorVisible; } }


    /// <summary>
    /// 커서를 화면 중앙에 멈추고 보이지 않게 설정
    /// /// </summary>
    /// <param name="_CursorState"></param>
    public void CursorActivation(bool _CursorState)
    {
        if (_CursorState)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        UnityEngine.Cursor.visible = _CursorState;
    }
    private void Start()
    {
        CursorActivation(CursorVisible);
    }
    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Escape))
        {
            CursorVisible = !CursorVisible;
            CursorActivation(CursorVisible);
        }
    }
#endif

}