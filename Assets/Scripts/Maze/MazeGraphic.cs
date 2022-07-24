using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for create visual part of maze
/// </summary>
public class MazeGraphic : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Vector2> uv;
    private List<Color> colors;

    private Color[] colorArray = { Color.white, Color.red, Color.green, Color.cyan, Color.yellow, new Color(0, 0, 0, 0) };

    private int triangleCounter = 0;

    private const float TILE_SIZE = 0.0625f;
    private Vector2 offset = Vector2.zero;

    #region Unity Messages

    private void Awake()
    {
        
    }

    #endregion

    #region Public Methods

    public void Initialize()
    {
        InitMesh();
        ClearMesh();
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();

        meshCollider.sharedMesh = mesh;
    }

    public void ClearMesh()
    {
        mesh.Clear();
        vertices.Clear();
        uv.Clear();
        triangles.Clear();
        colors.Clear();

        triangleCounter = 0;
    }

    public void AddTile(float x, float y, int id = 0, int idColor = 0)
    {
        Vector3 pos = Vector3.zero;
        pos.x = x;
        pos.y = y;

        vertices.Add(Vector3.zero + pos);
        vertices.Add(Vector3.up + pos);
        vertices.Add(Vector3.right + pos);
        vertices.Add(Vector3.right + Vector3.up + pos);

        colors.Add(colorArray[idColor]);
        colors.Add(colorArray[idColor]);
        colors.Add(colorArray[idColor]);
        colors.Add(colorArray[idColor]);

        triangles.AddRange(new int[]  {
            0 + triangleCounter,
            1 + triangleCounter,
            2 + triangleCounter });

        triangles.AddRange(new int[]  {
            2 + triangleCounter,
            1 + triangleCounter,
            3 + triangleCounter });

        triangleCounter += 4;

        offset = Vector2.zero;

        offset.x += id * TILE_SIZE;
        uv.Add(Vector2.zero + offset);
        uv.Add(Vector2.up + offset);
        uv.Add(Vector2.right * TILE_SIZE + offset);
        uv.Add(Vector2.right * TILE_SIZE + Vector2.up + offset);

    }

    #endregion

    #region Private Methods

    private void InitMesh()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>(); 
        meshRenderer.material = material;
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        vertices = new List<Vector3>();
        triangles = new List<int>();
        uv = new List<Vector2>();
        colors = new List<Color>();
    }

    #endregion
}