using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaNFases : MonoBehaviour
{
   public void Carregamento(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void Voltar()
    {
        SceneManager.LoadScene(0);
    }

    public void Avançar()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase + 1);
    }
}
