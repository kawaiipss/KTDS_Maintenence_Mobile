using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inheritance : MonoBehaviour
{
    public GameObject root;
    [SerializeField] private AxRButton m_btnCube;
    public bool isInherit;

    // Start is called before the first frame update
    void Start()
    {
        isInherit = true;
        m_btnCube.ACT_CLICK = Inheritence;
    }

    public void Inheritence(AxRButton _button)
    {
        if(isInherit)
        {
            isInherit = false;
            gameObject.transform.parent = null;
        }
        if(!isInherit)
        {
            isInherit = true;
            gameObject.transform.parent = root.transform;
        }
    }
}
