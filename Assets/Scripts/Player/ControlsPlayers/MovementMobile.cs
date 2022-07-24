using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementMobile : MovementBase
{
    private GameObject canvas; 
    private Button rightButton;
    private Button leftButton;
    private Button upButton;
    private Button downButton;

    HudView hudView;

    public MovementMobile(Action<int, int> move) : base(move)
    {
        hudView = GameObject.FindObjectOfType<HudView>();
        hudView.Init(Right, Left, Up, Down);
    }

    public override void Movement()
    {
        
    }

    private void Right()
    {
        x = 1;
        move?.Invoke(x, y);
        x = 0;
    }

    private void Left()
    {
        x = -1;
        move?.Invoke(x, y);
        x = 0;
    }

    private void Up()
    {
        y = 1;
        move?.Invoke(x, y);
        y = 0;
    }

    private void Down()
    {
        y = -1;
        move?.Invoke(x, y);
        y = 0;
    }
}
