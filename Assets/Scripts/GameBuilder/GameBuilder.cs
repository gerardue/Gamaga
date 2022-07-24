using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    [SerializeField]
    private CameraHandler cam;
    [SerializeField]
    private MazeHandler mazeHandler;
    [SerializeField]
    private MazeGraphic mazeGraphic;
    [SerializeField]
    private MovementControllerView movementController; 


    #region Unity Messages

    private void Awake()
    {
        Build().WrapErrors(); 
    }

    #endregion

    #region Private Regions

    private async Task Build()
    {
        mazeGraphic.Initialize();
        mazeHandler.Initialize();
        cam.Initialize(mazeHandler.SizeMaze); 
        movementController.Initialize(mazeHandler.Maze, mazeHandler.SizeMaze);

        await Task.Yield();  
    }

    #endregion
}
