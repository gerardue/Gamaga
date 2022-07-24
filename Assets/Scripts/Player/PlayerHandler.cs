using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for move the player
/// </summary>
public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private MazeEntity mazeEntity;

    private Vector3Int nextPosition = Vector3Int.zero;

    private int sizeMaze; 

    public void Initialize(MazeEntity mazeEntity, int sizeMaze)
    {
        this.mazeEntity = mazeEntity;
        this.sizeMaze = sizeMaze;   
    }

    public void MovePlayer(int x, int y)
    {

        if (!IsCorrectMove(x, y))
        {
            nextPosition -= new Vector3Int(x, y, 0);
            return;
        }

        player.position += new Vector3(x, y, player.position.z);
    }

    private bool IsCorrectMove(int x, int y)
    {
        // 0 right, 1 left, 2 up, 3 down
        int dir = -1; 
        dir = GetDirection(x, y);
        return EvaluateDirection(dir);

    }

    private int GetDirection(int x, int y)
    {
        if (x == 1)
            return 0;
        else if (x == -1)
            return 1;
        else if (y == 1)
            return 2;
        else if (y == -1)
            return 3;

        return -1; 
    }

    private bool EvaluateDirection(int dir)
    {
        if (dir == 0)
            return mazeEntity.Maze[((int)player.position.x + 1, (int)player.position.y)].canGoLeft;
        else if (dir == 1)
            return mazeEntity.Maze[((int)player.position.x - 1, (int)player.position.y)].canGoRight;
        else if (dir == 2)
            return mazeEntity.Maze[((int)player.position.x, (int)player.position.y + 1)].canGoDown;
        else if (dir == 3)
            return mazeEntity.Maze[((int)player.position.x, (int)player.position.y - 1)].canGoUp;

        return false;
    }
}
