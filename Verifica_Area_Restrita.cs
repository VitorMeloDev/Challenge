using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verifica_Area_Restrita : MonoBehaviour
{
    public static bool restrita = false;

    private void OnMouseOver()
    {
        if(restrita == false)
        {
            restrita = true;
        }
    }

    private void OnMouseExit()
    {
        if (restrita == true)
        {
            restrita = false;
        }
    }
}
