using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identificador : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject moedaAnim;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bola"))
        {
            if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
            {
                //GAMEMANAGER.instance.pontos++;
                ShootScript.fezponto = true;

                GAMEMANAGER.instance.moedasIntSave +=  50;

                UI_Manager.instance.moedas.text = (GAMEMANAGER.instance.moedasIntSave).ToString();
                TocaAudio.TocadordeAudio(audioS, clip);
                Instantiate(moedaAnim, transform.position, Quaternion.identity);
            }

            if (OndeEstou.instance.fase == 3)
            {
                //GAMEMANAGER.instance.pontos++;
                ShootScript.fezponto = true;
                //BolaMorte.instance.ponto = true;

                TocaAudio.TocadordeAudio(audioS, clip);
                //Instantiate(moedaAnim, transform.position, Quaternion.identity);
            }

        }

    }
}
