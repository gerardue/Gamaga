using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

/// <summary>
/// This class is responsable for use data of maze and connect systems
/// </summary>
public class MazeHandler : MonoBehaviour
{
    [SerializeField]
    private int sizeMaze = 4;
    [SerializeField]
    private int emptyCells = 0;

    [SerializeField]
    private Button createMazeButton;
    [SerializeField]
    private Button findPathButton;

    private ExplorerEntity explorer;   //Entity
    private MazeEntity maze;     //Entity
    private MazeGraphic tileMap;  //View

    private PathFindingHandler pathFinding;

    public int SizeMaze { get => sizeMaze; }
    public MazeEntity Maze { get => maze; }

    #region Unity Messages 

    //private void Awake()
    //{

    //}

    //private void Update()
    //{

    //}


    //private void Start()
    //{

    //}

    #endregion

    #region Public Methods

    public void Initialize()
    {
        explorer = new ExplorerEntity();
        maze = new MazeEntity();
        pathFinding = new PathFindingHandler(Maze, sizeMaze);
        tileMap = FindObjectOfType<MazeGraphic>();

        sizeMaze = Random.Range(3, 6);
        emptyCells = RandomEmptyCeils(); 

        explorer.SetClamp(SizeMaze, SizeMaze);

        // Adding events
        explorer.OnForward = (pos, lastPos, turn, invTurn) =>
        { Maze.Add(pos, lastPos, turn, invTurn); };

        Maze.OnIterator = (x, y, value) =>
        { tileMap.AddTile(x, y, value & 0x0F, (value >> 4) & 0x0F); };

        ///////

        Create().WrapErrors();

        createMazeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        findPathButton.onClick.AddListener(() =>
        {
            FindPath().WrapErrors();
        });
    }

    #endregion

    #region Private Methods

    private async Task Create()
    {
        await Restart();
        await CreateMaze();
    }

    private async Task CreateMaze()
    {
        Vector3 tempPos = new Vector3();
        System.Random rand = new System.Random();

        await Task.Run(async () =>
        {
            while (Maze.GetEmptyCount() > emptyCells)
            {
                explorer.AddTurn(rand.Next(1, 4));

                tempPos = explorer.Pos;
                tempPos += explorer.Dir;

                if (tempPos.x == SizeMaze || tempPos.y == SizeMaze)
                    continue;

                if (!Maze.IsEmptyCell(tempPos))
                {
                    continue;
                }

                explorer.Forward();
                await Task.Yield();
            }
        });

        UpdateTilemap();
        Maze.Iterate();
    }

    private async Task Restart()
    {
        Maze.Clear();
        explorer.ResetPos();
        await Maze.CreateCells(SizeMaze, SizeMaze);
        await Maze.CreateCellsForbbiden(SizeMaze, emptyCells);
        Maze.AddColor(Vector3.zero, 3);
    }

    private void UpdateTilemap()
    {
        Maze.AddColor(new Vector3(SizeMaze - 1, SizeMaze - 1, 0), 2);
        tileMap.ClearMesh();
        Maze.IterateRect();
        tileMap.UpdateMesh();
    }

    private async Task FindPath()
    {
        await pathFinding.CreateAStarPath();
        UpdateTilemap();
    }

    private int RandomEmptyCeils()
    {
        if(sizeMaze == 3)
            return 0;

        return sizeMaze - 3;
    }

    #endregion
}
