using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    GameObject gObj = null;
    Plane objPlane;
    Vector3 mouseObject;

    public ScreenShake screenShake;
    private void Start()
    {
        screenShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<ScreenShake>();

        
    }



    
    void Update()
    {
        //log the hit point coordinates in console
        

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay1 = GenerateRay();
            bool isHit = Physics.Raycast(mouseRay1.origin, mouseRay1.direction, out hit);
            if (isHit && hit.collider.gameObject.name == "Sphere")
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
            RaycastHit hit;
            Ray mouseRay1 = GenerateRay();
            bool isHit = Physics.Raycast(mouseRay1.origin, mouseRay1.direction, out hit);
            Debug.LogError("Hit point at x:" + hit.point.x + ", y:" + hit.point.y + ", z:" + hit.point.z);
            float distanceToWantedAngle = calculateDistanceToWantedAngle(hit);
            int screenShakeNumber = getScreenShakeNumber(distanceToWantedAngle);

            gObj = null;
            if (isHit && hit.collider.gameObject.name == "WantedAngle")
            {
                Debug.LogError("Unlock");
                GameObject winScreen = GameObject.FindGameObjectWithTag("WinScreen");
                winScreen.GetComponent<MeshRenderer>().enabled = true;
                StartCoroutine(waitOneSecond(winScreen));

                GameObject timerUI = GameObject.Find("CountdownText");
                Destroy(timerUI);

            }
            else
            {
                Debug.LogError("Wanted Angle not find");
                
                //determine the magnitue of screen shake and carry the shake out
                if (!screenShakeNumber.Equals(0))
                {
                    screenShake.CamShake(screenShakeNumber);
                    Debug.LogError("Not unlock");
                }
                else
                {
                    Debug.LogError("Odd distance appear");
                }
            }

           
        }
        
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

    private float calculateDistanceToWantedAngle(RaycastHit hit)
    {
        //calculation:
        //find the hitPoint x and y position
        float hitPointX = hit.point.x;
        float hitPointY = hit.point.y;
        //find the wanted angle x and y position
        GameObject WantedAngle = GameObject.Find("WantedAngle");
        float wantedAngleX = WantedAngle.transform.position.x;
        float wantedAngleY = WantedAngle.transform.position.y;
        //find the vecter x and  y value: ((x2 - x1)(y2-y1))
        float XDistanceToWantedAngle = hitPointX - wantedAngleX;
        float YDistanceToWantedAngle = hitPointY - wantedAngleY;
        //find the magnitue of the vector using sqrt((x2 -x1)^2 + (y2 - y1)^2)
        float distanceToWantedAngle = Mathf.Sqrt(Mathf.Pow(XDistanceToWantedAngle, 2) + Mathf.Pow(YDistanceToWantedAngle, 2));
        Debug.LogError("Distance to wantedAngle = " + distanceToWantedAngle);

        return distanceToWantedAngle;
        

    }

    private int getScreenShakeNumber(float distanceToWantedAngle)
    {
        //Determine the screenshake number number
        if (distanceToWantedAngle > 0.8)
        {
            return 3;
        }
        else if (distanceToWantedAngle < 0.8 && distanceToWantedAngle > 0.30)
        {
            return 2;
        }
        else if (distanceToWantedAngle < 0.3)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private void StartCoroutine(Func<IEnumerator> waitOneSecond)
    {
        throw new NotImplementedException();
    }

    IEnumerator waitOneSecond(GameObject winScreen)
    {
        yield return new WaitForSeconds(1f);
        winScreen.GetComponent<MeshRenderer>().enabled = false;
    }   
    

    
    
}
