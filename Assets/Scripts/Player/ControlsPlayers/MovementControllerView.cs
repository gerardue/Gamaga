using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerView : MonoBehaviour
{
    [SerializeField]
    private PlayerHandler playerHandler; 

    private MovementBase movementBase;
    private bool ignoreUpdate; 

    //private void Awake()
    //{

    //}

    public void Initialize(MazeEntity mazeEntity, int sizeMaze)
    {
        playerHandler.Initialize(mazeEntity, sizeMaze);
#if UNITY_IOS || UNITY_ANDROID
        movementBase = new MovementMobile(playerHandler.MovePlayer);
        ignoreUpdate = true;
#elif UNITY_STANDALONE
        movementBase = new MovementComputer(playerHandler.MovePlayer);
        ignoreUpdate = false;
#elif UNITY_EDITOR
        movementBase = new MovementComputer(playerHandler.MovePlayer);
        ignoreUpdate = false;
#endif
    }

    private void Update()
    {
        if (ignoreUpdate)
            return;
        movementBase.Movement(); 
    }
}
