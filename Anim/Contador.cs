using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
   public void EventoContador()
   {
        gameObject.GetComponent<Text>().enabled = false;
        GAMEMANAGER.instance.jogoExecutando = true;
        GAMEMANAGER.instance.PrimeiraJogada();
   }
}
