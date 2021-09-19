using UnityEngine;
using UnityEngine.UI;

public class raycastPaint : MonoBehaviour
{
    public Shader WallShader;
    public GameObject But;
    public Camera CamTex;
    public GameObject tex;

    public static bool start = false;

    RaycastHit raycast;
    int ButtonTouch = 0;
    Vector3 Pos;
    Vector2 PredPos;
    Collider predCol = null;

    public void DrawOn()
    {
        start = true;
        Pos = But.transform.position;
        PredPos = Input.GetTouch(Input.touchCount - 1).position;
    }

    private void Update()
    {
        if (predCol != null)
        {
            predCol.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CamTex.targetTexture);
            predCol = null;
        }

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

            if (Physics.Raycast(ray, out raycast, 10000, 1000))
            {
                RenderTexture RenTexWall = raycast.collider.GetComponent<Wall>().RenTex;
                RenderTexture RenTex = (RenTexWall == null) ? new RenderTexture(1000, 666, 24) : RenTexWall;
                raycast.collider.GetComponent<Wall>().RenTex = RenTex;

                CamTex.targetTexture = RenTex;

                Material Mat = tex.GetComponent<MeshRenderer>().material;

                Mat.SetTexture("_Brfore", RenTex);
                Mat.SetFloat("X", raycast.textureCoord.x);
                Mat.SetFloat("Y", raycast.textureCoord.y);
                predCol = raycast.collider;
            }
        }
    }
}
