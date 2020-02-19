using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    GameObject gObj = null;
    Plane objPlane;
    Vector3 mouseObject;
    private void Start()
    {
        
    }
    
    Ray GenerateRay()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 mousePosFAR = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosNEAR = Camera.main.ScreenToWorldPoint(mousePosNear);
        Debug.DrawRay(mousePosNEAR, mousePosFAR - mousePosNEAR, Color.green);
        Ray mouseRay = new Ray(mousePosNEAR, mousePosFAR - mousePosNEAR);
        return mouseRay;
    }

    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay1 = GenerateRay();
            RaycastHit hit;
            
            if (Physics.Raycast(mouseRay1.origin, mouseRay1.direction, out hit) && hit.collider.gameObject.name == "Sphere")
            {
                gObj = hit.transform.gameObject;
                objPlane = new Plane(Camera.main.transform.forward * -1, gObj.transform.position);

                //calculated mouse offsets
                Ray mouseRay2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                float rayDistance;
                objPlane.Raycast(mouseRay2, out rayDistance);
                mouseObject = gObj.transform.position - mouseRay2.GetPoint(rayDistance);
            }
        }
        else if (Input.GetMouseButton(0) && gObj)
        {
            Ray mouseRay2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if(objPlane.Raycast(mouseRay2, out rayDistance))
            {
                gObj.transform.position = mouseRay2.GetPoint(rayDistance) + mouseObject;
            }
        }
        else if (Input.GetMouseButtonUp(0) && gObj)
        {
            gObj = null;


            Ray mouseRay1 = GenerateRay();
            RaycastHit hit;

            if(Physics.Raycast(mouseRay1.origin, mouseRay1.direction, out hit) && hit.collider.gameObject.name == "WantedAngle")
            {
                Debug.LogError("Unlock");
            }
            else
            {
                Debug.LogError("Not unlock");
            }
        }
        
    }
}
