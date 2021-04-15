using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingTest : MonoBehaviour
{
    private float fade = 0.0f;
    private float time = 0;
    public float playTime;
    public Image[] Circle;

    private void Start()
    {
        StartCoroutine(FadeAnim());
        for (int i = 0; i < Circle.Length; i++)
        {
            print("");
        }
    }

    IEnumerator FadeAnim()
    {
        while (true)
        {
            yield return null;

            time += Time.deltaTime;
            if (fade >= 0.0f && time >= 0.1f)
            {
                fade += 0.1f;
                for (int i = 0; i < Circle.Length; i++)
                {
                    Circle[i].color = new Color(0, 255, 255, fade);
                }
                time = 0;
            }
            if (fade >= 1.0f)
                fade = 0;
        }
    }

    //private void Start()
    //{
    //    for (int i = 0; i < Circle.Length; i++)
    //        print("써클의 알파는" + Circle[i].color.a.ToString());
    //}
}
