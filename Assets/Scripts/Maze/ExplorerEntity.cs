using System;
using UnityEngine;

/// <summary>
/// This class is responsible for explore and create a random maze
/// </summary>
public class ExplorerEntity
{
    private Vector3 pos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    private int turn = 0;
    private int invTurn = 0;

    private Vector3[] dirs = {
        Vector3.up,
        Vector3.right,
        Vector3.down,
        Vector3.left
    };

    private float clampX = 0;
    private float clampY = 0;

    public Vector3 Pos { get => pos; }
    public Vector3 LastPos { get => lastPos; }
    public Vector3 Dir { get => dirs[turn]; }

    public int Turn { get => turn; }
    public int InvTurn { get => invTurn; }

    /// <summary>
    /// Position, Last Position, Turn, Inverted Turn
    /// </summary>
    public Action<Vector3, Vector3, int, int> OnForward;
    public Action<int> OnTurn;

    public ExplorerEntity(float clampX = 3, float clampY = 3)
    {
        OnForward = (x, y, z, w) => { };
        OnTurn = (i) => { };

        invTurn = (turn + 2) & 0x03;

        this.clampX = clampX;
        this.clampY = clampY;
    }

    #region Public Methods

    public void Forward()
    {
        lastPos = pos;
        pos += dirs[turn];

        //Restrict array from explorer
        pos.x = Mathf.Clamp(pos.x, 0, clampX - 1);
        pos.y = Mathf.Clamp(pos.y, 0, clampY - 1);

        if (lastPos != pos)
            OnForward(pos, lastPos, turn, invTurn);
    }

    public void TurnTo(int newTurn)
    {
        turn = newTurn;
        turn &= 0x03;
        invTurn = (turn + 2) & 0x03;
        OnTurn(turn);
    }

    public void AddTurn(int newTurn)
    {
        turn += newTurn;
        turn &= 0x03;
        invTurn = (turn + 2) & 0x03;
        OnTurn(turn);
    }

    public void SetClamp(float clampX, float clampY)
    {
        this.clampX = clampX;
        this.clampY = clampY;
    }

    public void ResetPos() => pos = Vector3.zero;


    #endregion

    #region Private Methods



    #endregion
}
