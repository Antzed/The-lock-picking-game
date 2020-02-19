using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 mousePosFAR = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosNEAR = Camera.main.ScreenToWorldPoint(mousePosNear);
        Debug.DrawRay(mousePosNEAR, mousePosFAR - mousePosNEAR, Color.green);
    }
}
