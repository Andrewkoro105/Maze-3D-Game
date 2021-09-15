using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class restart : MonoBehaviour
{
    public static int rekord = 0;
    public GameObject M;
    string Path;


    public void OnCollisionEnter(Collision collision)
    {

        Path = Application.persistentDataPath + "/rkord.txt";

#if UNITY_EDITOR && !UNITY_ANDRID
        Path = "C:/Project/data/Maze/rkord.txt";
#endif

        M.SetActive(true);
        M.transform.parent.GetChild(1).gameObject.SetActive(false);
        time.c = false;

        Menu.n ++;

        M.GetComponent<Menu>().menu();
    }

    public void Re()
    {
        SceneManager.LoadScene(0);
        time.c = true;
        time.b= true;
        time.tim = 0;
    }
}
