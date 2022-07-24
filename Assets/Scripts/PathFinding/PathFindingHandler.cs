using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for find the more close path for player
/// </summary>
public class PathFindingHandler
{
    private Vector3Int[] dir = { Vector3Int.right, Vector3Int.up };
    private MazeEntity mazeEntity;
    private int sizeMaze; 

    // Data for path finddin


    public PathFindingHandler(MazeEntity mazeEntity, int sizeMaze)
    {
        this.mazeEntity = mazeEntity;
        this.sizeMaze = sizeMaze;   
    }

    #region Public Methods

    public async Task CreateAStarPath()
    {
        bool isArrive = false;
        Vector3Int explorer = Vector3Int.zero;
        Cell currentCell;
        int lastMovement = -1; // 0 -> right, 1 -> up, 2 -> down
        Stack<int> movements = new Stack<int>();
        Stack<Vector3Int> steps = new Stack<Vector3Int>();
        currentCell = mazeEntity.Maze[(0, 0)];
        Cell nextCellRight = mazeEntity.Maze[(0, 0)];
        Cell nextCellUp = mazeEntity.Maze[(0, 0)];

        while (!isArrive)
        {
            nextCellRight = mazeEntity.Maze[(explorer.x + 1, explorer.y)];  
            nextCellUp = mazeEntity.Maze[(explorer.x, explorer.y + 1)];

            if (currentCell.canGoRight && nextCellRight.walkeable)
            {
                explorer += Vector3Int.right;
                currentCell = mazeEntity.Maze[(explorer.x, explorer.y)];
                lastMovement = 0;
                movements.Push(lastMovement);
                steps.Push(Vector3Int.right);
            }
            else if (currentCell.canGoUp && nextCellUp.walkeable)
            {
                explorer += Vector3Int.up;
                currentCell = mazeEntity.Maze[(explorer.x, explorer.y)];
                lastMovement = 1;
                movements.Push(lastMovement);
                steps.Push(Vector3Int.up);
            }
            else
            {
                if(currentCell.canGoDown && nextCellUp.walkeable)
                {
                    explorer += Vector3Int.down;
                    currentCell = mazeEntity.Maze[(explorer.x, explorer.y)];
                    lastMovement = 2;
                    movements.Push(lastMovement);
                    steps.Push(Vector3Int.down);
                }

                else if (currentCell.typePoint == TypePoint.None)
                {
                    currentCell.walkeable = false;
                    steps.Pop();
                    explorer -= dir[movements.Pop()];
                    currentCell = mazeEntity.Maze[(explorer.x, explorer.y)];
                }
            }

            //Debug.Log("Finding"); 

            if (currentCell.typePoint == TypePoint.End)
            {
                isArrive = true;
                break;
            }
            await Task.Yield();
        }

        DrawPath(GetSteps(steps));
    }

    #endregion

    #region Private Methods

    private void DrawPath(List<Vector3Int> steps)
    {
        Vector3Int path = Vector3Int.zero;
        foreach (var step in steps)
        {
            path += step;
            mazeEntity.AddColor(path, 4);
        }
    }

    private List<Vector3Int> GetSteps(Stack<Vector3Int> steps)
    {
        List<Vector3Int> inverseSteps = new List<Vector3Int>(steps);
        inverseSteps.Reverse();
        return inverseSteps;
    } 

    #endregion

}
