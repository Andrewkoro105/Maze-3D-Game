using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hint : MonoBehaviour
{
    public GameObject Hint;
    LineRenderer L;
    bool off;
    SpriteRenderer color;
    public GameObject OnOff;

    private void Start()
    {
        L = Hint.GetComponent<LineRenderer>();
        color = OnOff.GetComponent<SpriteRenderer>();
    }

    public void On()
    {
        off = !off;
        L.enabled = off;
        time.b = false;

        if (off) color.color = new Color(0, 255, 0);
        else color.color = new Color(255, 0, 0);
    }
}
