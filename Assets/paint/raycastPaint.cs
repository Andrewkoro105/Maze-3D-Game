using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class raycastPaint : MonoBehaviour
{
    public Shader WallShader;
    public GameObject But;
    public CustomRenderTexture CRenderTexture;

    RaycastHit raycast;
    public static bool start = false;
    int ButtonTouch = 0;
    Vector3 Pos;
    Vector2 PredPos;

    public void DrawOn()
    {
        start = true;
        Pos = But.transform.position;
        PredPos = Input.GetTouch(Input.touchCount - 1).position;
    }

    private void Update()
    {
        if (start)
        {
            start = false;
            for (int i = 0; i < Input.touchCount; i++)
            {

                Vector2 pos = Input.GetTouch(i).position;
                float x = pos.x;
                float y = pos.y;
                float x1 = PredPos.x;
                float y1 = PredPos.y;

                if (Mathf.Pow(200, 2) > Mathf.Pow(x - x1, 2) + Mathf.Pow(y - y1, 2) && pos.x > 400)
                {
                    ButtonTouch = i;
                    start = true;
                    PredPos = pos;
                    break;
                }
            }
        }
        if (start)
        {

            if (Input.GetTouch(ButtonTouch).position.x > 400)
            {
                But.transform.localPosition = new Vector3(Input.GetTouch(ButtonTouch).position.x - 640, Input.GetTouch(ButtonTouch).position.y - 360, 0);
            }

            Ray ray = new Ray(Pos + ((But.transform.position - Pos) * 2), But.transform.forward);

            if (Physics.Raycast(ray, out raycast, 10000, 1 << 3))
            {
                But.GetComponent<Image>().color = Color.green;
                Wall wall = raycast.collider.GetComponent<Wall>();

                if (wall.CustRenTex == null)
                {
                    Cell C = raycast.collider.transform.parent.GetComponent<Cell>();
                    wall.CustRenTex = NewCustomRenderTexture();
                    wall.CustRenTex.name = + C.cord.x + " " + C.cord.y;
                    raycast.collider.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", wall.CustRenTex);
                }

                CustomRenderTexture CRT = wall.CustRenTex;
                Material wallMaterial = CRT.material;
                if (wallMaterial != null)
                {
                    wallMaterial.SetFloat("x", raycast.textureCoord.x);
                    wallMaterial.SetFloat("y", raycast.textureCoord.y);

                    Debug.Log(CRT);

                    CRT.Update();


                }
            }
            else But.GetComponent<Image>().color = Color.red;
        }
        else But.GetComponent<Image>().color = Color.red;

    }

    CustomRenderTexture NewCustomRenderTexture()
    {
        CustomRenderTexture CustRenTex = new CustomRenderTexture(CRenderTexture.width, CRenderTexture.height);

        CustRenTex.material = CRenderTexture.material;
        CustRenTex.initializationMode = CRenderTexture.initializationMode;
        CustRenTex.initializationMaterial = CRenderTexture.initializationMaterial;
        CustRenTex.initializationSource = CRenderTexture.initializationSource;
        CustRenTex.initializationColor = CRenderTexture.initializationColor;
        CustRenTex.initializationTexture = CRenderTexture.initializationTexture;

        CustRenTex.updateMode = CRenderTexture.updateMode;
        CustRenTex.doubleBuffered = CRenderTexture.doubleBuffered;

        return CustRenTex;
    }
}
