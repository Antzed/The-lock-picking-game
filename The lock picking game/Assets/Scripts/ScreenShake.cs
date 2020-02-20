using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        camAnim.SetTrigger("shake");
    }

    public static explicit operator ScreenShake(GameObject v)
    {
        throw new NotImplementedException();
    }
}
