using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class Menu : MonoBehaviour
{
    public GameObject rekord;
    public GameObject N;

    TMP_Text T_rekord;
    TMP_Text T_N;

    public static int n = 0;
    string Path;
    float Time = 0;

    private void Start()
    {
        Path = Application.persistentDataPath + "/rkord.txt";

#if UNITY_EDITOR && !UNITY_ANDRID
        Path = "C:/Project/data/Maze/rkord.txt";
#endif

        if (!File.Exists(Path))
            using (StreamWriter S = new StreamWriter(Path))
                S.Write("0,00 0");
        else
            using (StreamReader S = new StreamReader(Path))
            {
                bool b = false;
                string text = S.ReadToEnd();
                string A = "";
                string B = "";

                for (int i = 0; i < text.Length; i++)
                {
                    char T = text[i];

                    if (T == ' ') b = true;
                    else if (!b) A += T;
                    else B += T;
                }

                Time = (float)(Convert.ToDouble(A));
                n = Convert.ToInt32(B);
            }

        T_rekord = rekord.GetComponent<TMP_Text>();
        T_N = N.GetComponent<TMP_Text>();
        gameObject.SetActive(false);
    }

    public void menu()
    {
        Time = Time < time.tim && Time != 0 ? Time : time.tim;

        T_N.text ="пройдено : " + n.ToString();
        T_rekord.text = "лучшее время : " + Time.ToString();

        using (StreamWriter S = new StreamWriter(Path))
            S.WriteLine(Time + " " + n);

    }
}
