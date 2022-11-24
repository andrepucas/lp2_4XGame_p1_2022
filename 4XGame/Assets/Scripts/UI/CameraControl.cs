using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Vector3 resetCamera;
    private Vector3 origin;
    private Vector3 difference;
    private bool drag = false;

    private float camSize;
    [SerializeField] private float zoomSensitivity; 
    void Start()
    {
        resetCamera = Camera.main.transform.position;
        Debug.Log("Yes");
        camSize = Camera.main.orthographicSize;
        Debug.Log(camSize);
    }

    void Update()
    {
        CamDrag();
        CamZoom();
    }


    private void CamDrag()
    {
        if (Input.GetMouseButton(2))
        {

            Debug.Log("Click");
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag == true)
        {
            Camera.main.transform.position = origin - difference;
        }
    }

    private void CamZoom()
    {

        camSize += (Input.mouseScrollDelta.y * zoomSensitivity);
        
        Camera.main.orthographicSize = camSize;
    }
}
