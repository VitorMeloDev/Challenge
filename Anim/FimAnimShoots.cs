using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimAnimShoots : MonoBehaviour
{
    public void FimRimShoot()
    {
        GAMEMANAGER.instance.rimShot = false;
    }

    public void FimSwishShot()
    {
        GAMEMANAGER.instance.swishShot = false;
    }

    public void FimSkyHook()
    {
        GAMEMANAGER.instance.skyHook = false;
    }
}
