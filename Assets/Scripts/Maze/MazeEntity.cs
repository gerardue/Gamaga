using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random; 

/// <summary>
/// This class is responsible for create and save data of maze
/// </summary>
public class MazeEntity
{
    private Dictionary<(int, int), Cell> maze;

    private int maxX = 0;
    private int maxY = 0;

    public Dictionary<(int, int), Cell> Maze { get => maze; }

    /// <summary>
    /// x, y, id
    /// </summary>
    public Action<int, int, int> OnIterator;

    public MazeEntity()
    {
        maze = new Dictionary<(int, int), Cell>();
        OnIterator = (x, y, v) => { };
    }


    #region Public Methods

    #region Methods for cells

    public Cell GetValueAt(Vector3 v)
    {
        return maze[((int)v.x, (int)v.y)];
    }

    public int SetValueAt(Vector3 v, int value)
    {
        maze[((int)v.x, (int)v.y)].id = value;
        return maze[((int)v.x, (int)v.y)].id;
    }

    public int CombineValueAt(Vector3 v, int value)
    {
        return maze[((int)v.x, (int)v.y)].id |= value;
    }

    public void Add(Vector3 pos, Vector3 lastPos, int turn, int invTurn)
    {
        CombineValueAt(pos, 1 << invTurn);
        CombineValueAt(lastPos, 1 << turn);
    }

    public void AddColor(Vector3 pos, int colorID)
    {
        int value = GetValueAt(pos).id & 0x0F;
        SetValueAt(pos, (colorID << 4) | value);
    }

    public int GetEmptyCount()
    {
        int count = 0;

        foreach (KeyValuePair<(int, int), Cell> m in maze)
        {
            if ((m.Value.id & 0x0F) == 0) { count++; }
        }

        return count;
    }

    public bool IsEmptyCell(Vector3 pos)
    {
        if (maze.ContainsKey(((int)pos.x, (int)pos.y)))
        {
            return maze[((int)pos.x, (int)pos.y)].isEnable;
        }
        return true;
    }

    public void Clear()
    {
        maze.Clear();
    }

    #endregion

    #region Iterations

    public void Iterate()
    {
        string sentece = string.Empty;
        foreach (KeyValuePair<(int, int), Cell> m in maze)
        {
            //OnIterator(m.Key.Item1, m.Key.Item2, m.Value.id);
            sentece = string.Empty;

            m.Value.CheckGoRight();
            m.Value.CheckGoUp();
            m.Value.CheckGoLeft();
            m.Value.CheckGoDown(); 

            if (m.Value.canGoRight)
                sentece += " right";
            if (m.Value.canGoUp)
                sentece += " up";
            if (m.Value.canGoDown)
                sentece += " down";
            if (m.Value.canGoLeft)
                sentece += " left"; 

            Debug.Log(m.Key + " - " + m.Value.id + sentece);
        }
    }

    public void IterateRect()
    {
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                OnIterator(x, y, maze[(x, y)].id);
            }
        }
    }

    #endregion

    public async Task CreateCells(int maxX, int maxY, int value = 0)
    {
        maze.Clear();

        this.maxX = maxX;
        this.maxY = maxY;

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                maze[(x, y)] = new Cell();
                maze[(x, y)].id = 0;
                maze[(x, y)].isEnable = true;

                if (x == 0 && y == 0)
                    maze[(x, y)].typePoint = TypePoint.Start;
                else if (x == maxX - 1 && y == maxY - 1)
                    maze[(x, y)].typePoint = TypePoint.End;
            }
        }

        //mark walls and shift 4 bits
        //top 1, right 2, bottom 4, left 8
        for (int y = -1; y < maxY + 1; y++)
        {
            maze[(-1, y)] = new Cell();
            maze[(-1, y)].id = 15; //left
            maze[(maxX, y)] = new Cell();
            maze[(maxX, y)].id = 15; //right
        }

        for (int x = -1; x < maxX + 1; x++)
        {
            maze[(x, maxY)] = new Cell();
            maze[(x, maxY)].id = 15; //top

            maze[(x, -1)] = new Cell();
            maze[(x, -1)].id = 15; //bottom
        }

        await Task.Yield();
    }

    public async Task CreateCellsForbbiden(int maxCells, int emptyCells)
    {
        List<int> xList = new List<int>();
        List<int> yList = new List<int>();
        List<int> tempList = new List<int>();


        for (int i = 0; i < maxCells; i++)
        {
            xList.Add(i);
            yList.Add(i);
        }

        for (int i = 0; i < emptyCells; i++)
        {
            tempList.Clear();
            tempList.AddRange(yList);

            int randX = xList[Random.Range(0, xList.Count)];
            xList.Remove(randX);

            if (randX == 0 || randX == maxCells - 1)
            {
                tempList.Remove(randX);
            }

            int randY = tempList[Random.Range(0, tempList.Count)];
            yList.Remove(randY);

            maze[(randX, randY)].isEnable = false;
            AddColor(new Vector3(randX, randY, 0), 5);

            await Task.Yield();
        }
    }

    public int DefineRandomEmptyCells(int maxCells)
    {
        int emptyCells = 0;

        if (maxCells > 2)
            emptyCells = 1;
        else if (maxCells > 3)
            emptyCells = (maxCells / 2) - 1;

        return emptyCells;
    }

    #endregion

    #region Private Methods



    #endregion
}
