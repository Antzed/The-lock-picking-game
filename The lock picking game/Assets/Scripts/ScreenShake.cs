using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake(int shakeNumber)
    {
        switch (shakeNumber)
        {
            case 1:
                camAnim.SetTrigger("shake");
                break;
            case 2:
                camAnim.SetTrigger("shake2");
                break;
            case 3:
                camAnim.SetTrigger("shake3");
                break;
        }
        
    }

    public static explicit operator ScreenShake(GameObject v)
    {
        throw new NotImplementedException();
    }
}
