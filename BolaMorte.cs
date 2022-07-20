using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaMorte : MonoBehaviour
{
    public static BolaMorte instance;

    [SerializeField] private float vidaBola = 1f;
    [SerializeField] private Color cor;
    [SerializeField] private Renderer bolaRender;
    [SerializeField] public bool tocouChao = false;
    [SerializeField] public bool ponto = false;


    // Start is called before the first frame update
    void Start()
    {
        bolaRender = GetComponent<Renderer>();
        cor = bolaRender.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if (tocouChao)
        {
            Matabola();
        }
    }

    void Matabola()
    {
        if (vidaBola > 0)
        {
            vidaBola -= Time.deltaTime;
            bolaRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaBola));

            GAMEMANAGER.instance.bolaEmCena = false;
        }

        if (vidaBola <= 0)
        {
            if(ponto == false)
            {
                GAMEMANAGER.instance.bolaEmCena = false;

                if (gameObject.CompareTag("bola"))
                {
                    GAMEMANAGER.instance.numJogadas--;
                    UI_Manager.instance.numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();
                    if (GAMEMANAGER.instance.numJogadas > 0)
                    {
                        GAMEMANAGER.instance.NascBolas();
                    }
                    Destroy(gameObject);
                }
            }
            
            if(ponto)
            {
                GAMEMANAGER.instance.bolaEmCena = false;

                if (gameObject.CompareTag("bola"))
                {
                    GAMEMANAGER.instance.numJogadas++;
                    UI_Manager.instance.numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();
                    if (GAMEMANAGER.instance.numJogadas > 0)
                    {
                        GAMEMANAGER.instance.NascBolas();
                    }
                    ponto = false;
                    Destroy(gameObject);
                }
            }
            
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Identificador"))
        {
            if(tocouChao == false)
            ponto = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            tocouChao = true;
        }

        
    }
}
