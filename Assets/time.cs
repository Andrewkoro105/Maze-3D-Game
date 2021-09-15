using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    public static bool b = true;
    public static bool c = true;
    TMP_Text text;
    public static float tim = 0;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (c)
        {
            if (b) tim += Time.deltaTime;
            else tim = 0;

            text.text = tim.ToString();
        }
    }
}
