using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    #region Unity Messages



    #endregion

    #region Public Methods

    public void Initialize(float posCamera)
    {
        Vector3 pos = new Vector3(posCamera / 2, posCamera / 2, -posCamera);
        cam.position = pos; 
    }

    #endregion
}
