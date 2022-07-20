using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PontosFree : MonoBehaviour
{
    public static PontosFree instance;

    public int pontos;
    public int best;
    public Text[] pontosTXT;
    public Text[] recordeTXT;

    void Start()
    {
        pontos = GAMEMANAGER.instance.pontos;
        best = PlayerPrefs.GetInt("Pontos");

        pontosTXT[0].text = pontos.ToString();
        pontosTXT[1].text = pontos.ToString();
        pontosTXT[2].text = pontos.ToString();

        recordeTXT[0].text = best.ToString();
        recordeTXT[1].text = best.ToString();
    }

    private void Update()
    {
        pontos = GAMEMANAGER.instance.pontos;
        best = PlayerPrefs.GetInt("Pontos");

        pontosTXT[0].text = pontos.ToString();
        pontosTXT[1].text = pontos.ToString();
        pontosTXT[2].text = pontos.ToString();

        recordeTXT[0].text = best.ToString();
        recordeTXT[1].text = best.ToString();

        if (GAMEMANAGER.instance.numJogadas <= 0)
            GuardarPontos();
    }

    public void GuardarPontos()
    {
       if (!PlayerPrefs.HasKey("Pontos"))
        {
            PlayerPrefs.SetInt("Pontos", pontos);
        }
        else
        {
           if(pontos > PlayerPrefs.GetInt("Pontos"))
            {
                UI_Manager.instance.newRecord.enabled = true;
                PlayerPrefs.SetInt("Pontos", pontos);
            }
        }
        
    }
}
