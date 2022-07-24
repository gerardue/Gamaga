using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBase 
{
    protected int x;
    protected int y;
    protected Action<int, int> move;

    protected MovementBase(Action<int, int> move)
    {
        this.move = move;
    }

    public abstract void Movement(); 
}
