using UnityEngine;

public class PlayerControls3D : MonoBehaviour
{
    public float Speed = 2;
    public float SpeedRot = 1;

    public GameObject Cam;

    Vector3 A = new Vector3();
    int Rot = 0;

    private Rigidbody Control;
    Touch touch2;
    bool touching = false;

    private void Start()
    {
        Control = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        int S = Input.touchCount;
        int j = -1;

        for (int i = 0; i < S; i++)
        {
            if (Input.GetTouch(i).position.x >= 640)
            {
                j = i;
                break;
            }
        }
        if (Input.touchCount > 0 && j != -1 && !raycastPaint.start)
        {
            if (!touching)
            {
                touching = true;
                touch2 = Input.GetTouch(j);
            }
            else
            {
                transform.Rotate(0, (touch2.position.x - Input.GetTouch(j).position.x) * SpeedRot, 0);
                Cam.transform.Rotate(touch2.position.y - Input.GetTouch(j).position.y, 0, 0);
                touch2 = Input.GetTouch(j);
            }
        }
        else touching = false;

        if (Rot == 1) A = transform.TransformDirection(Vector3.left);
        else if (Rot == 2) A = transform.TransformDirection(Vector3.right);
        else if (Rot == 3) A = transform.TransformDirection(Vector3.forward);
        else if (Rot == 4) A = transform.TransformDirection(Vector3.back);
        else A = new Vector3(0, 0, 0);

        Control.velocity += A * Speed;
    }

    public void left()
    {
        Rot = 1;
    }

    public void right()
    {
        Rot = 2;
    }

    public void forward()
    {
        Rot = 3;
    }

    public void back()
    {
        Rot = 4;
    }

    public void Up()
    {
        Rot = 0;
    }
}