using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWantedAngle : MonoBehaviour
{
    GameObject[] AngleObjectNames;
    string WantedAngleObjectName = "WantedAngle";
    int index;
    // Start is called before the first frame update
    void Start()
    {
        AngleObjectNames = GameObject.FindGameObjectsWithTag("Angle");
        index = Random.Range(0, AngleObjectNames.Length);
        Debug.LogError(index);
        AngleObjectNames[index].name = WantedAngleObjectName;
    }

}
