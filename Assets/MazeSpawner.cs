using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);
    public HintRenderer HintRenderer;
    public Material M;
    public GameObject Menu;
    public CustomRenderTexture CRenderTexture;

    public Maze maze;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                bool WallLeft = maze.cells[x, y].WallLeft;
                c.WallLeft.SetActive(WallLeft);
                c.cord = new Vector2(x, y);

                bool WallBottom = maze.cells[x, y].WallBottom;
                c.WallBottom.SetActive(WallBottom);
                c.cord = new Vector2(x, y);

                if (x == maze.finishPosition.x && y == maze.finishPosition.y)
                {
                    GameObject Finish = c.transform.GetChild(0).gameObject;
                    Finish.AddComponent<restart>();
                    Finish.GetComponent<MeshRenderer>().material = M;
                    Finish.GetComponent<restart>().M = Menu;
                }
            }
        }

        HintRenderer.DrawPath();
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