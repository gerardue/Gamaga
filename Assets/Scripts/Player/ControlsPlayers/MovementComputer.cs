using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComputer : MovementBase
{
    public MovementComputer(Action<int, int> move) : base(move)
    {

    }

    public override void Movement()
    {
        x = 0;
        y = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            x = 1;
            move?.Invoke(x, y);
            return; 
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            x = -1;
            move?.Invoke(x, y);
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            y = 1;
            move?.Invoke(x, y);
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            y = -1;
            move?.Invoke(x, y);
            return;
        }
    }
}
