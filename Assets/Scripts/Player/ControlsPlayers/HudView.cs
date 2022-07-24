using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Button rightButton;
    [SerializeField] 
    private Button leftButton;
    [SerializeField] 
    private Button upButton;
    [SerializeField] 
    private Button downButton;

    public void Init(Action onRight, Action onLeft, Action onUp, Action onDown)
    {
        canvas.SetActive(true);

        rightButton.onClick.AddListener(() => onRight?.Invoke());
        leftButton.onClick.AddListener(() => onLeft?.Invoke());
        upButton.onClick.AddListener(() => onUp?.Invoke());
        downButton.onClick.AddListener(() => onDown?.Invoke());
    }
}
