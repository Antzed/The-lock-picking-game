using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 MousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 MousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

            //convert the position we get from these planes from screen space  into world space
            //It established a relationship between screen and the world space
            Vector3 MousePosFAR = Camera.main.ScreenToWorldPoint(MousePosFar);
            Vector3 MousePosNEAR = Camera.main.ScreenToWorldPoint(MousePosNear);
            Debug.DrawRay(MousePosNEAR, MousePosFAR - MousePosNEAR, Color.green);

        }
    }
}
