 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{
    private float forca = 2.0f;

    private Vector2 starPos;
    private bool tiro = false;
    private bool mirando = false;

    [SerializeField] private GameObject dotsGO;
    private List<GameObject> caminho;

    [SerializeField] private Rigidbody2D meuRb;
    [SerializeField] private CircleCollider2D col;

    [SerializeField] private float valorX, valorY;

    //Tipos de cesta
    private bool bateuAro; 
    private bool bateuTabela;
    private bool sky;

    //Marcou Ponto
    public static bool fezponto;

    //Txt Animações
    public Animator rimShot, swishShot, skyHook;

    // Start is called before the first frame update
    void Start()
    {
        rimShot = GameObject.Find("RimShotTxt").GetComponent<Animator>();
        swishShot = GameObject.Find("SwishShotTxt").GetComponent<Animator>();
        skyHook = GameObject.Find("SkyHookTxt").GetComponent<Animator>();



        sky = false;
        fezponto = false;
        dotsGO = GameObject.FindWithTag("dots");
        meuRb.isKinematic = true;
        col.enabled = false;
        starPos = transform.position;
        caminho = dotsGO.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        for(int x=0; x < caminho.Count;x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {

        if (GAMEMANAGER.instance.jogoExecutando)
        {
            if (!meuRb.isKinematic && fezponto)
            {
                if (bateuTabela == false)
                {
                    //RimShot
                    if (bateuAro)
                    {
                        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
                        {
                            GAMEMANAGER.instance.rimShot = true;
                            GAMEMANAGER.instance.desafioNum1RimShoot--;
                            GAMEMANAGER.instance.DesafioDeFase(OndeEstou.instance.fase);

                            rimShot.Play("RimShotTxt");
                            fezponto = false;
                        }

                        if (OndeEstou.instance.fase == 3)
                        {
                            rimShot.Play("RimShotTxt");
                            GAMEMANAGER.instance.pontos += 30;
                            fezponto = false;
                        }
                    }
                    //SwishShot
                    else
                    {
                        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
                        {
                            GAMEMANAGER.instance.swishShot = true;
                            GAMEMANAGER.instance.desafioNum2SwishShoot--;
                            GAMEMANAGER.instance.DesafioDeFase(OndeEstou.instance.fase);
                            swishShot.Play("SwishShotTxt");
                            fezponto = false;
                        }

                        if (OndeEstou.instance.fase == 3)
                        {
                            swishShot.Play("SwishShotTxt");
                            GAMEMANAGER.instance.pontos += 50;
                            fezponto = false;
                        }
                    }
                }
                else
                {
                    if(fezponto)
                    {
                        GAMEMANAGER.instance.pontos += 15;
                        fezponto = false;
                    }
                    
                }

                //SkyHook
                if (sky)
                {
                    if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
                    {
                        GAMEMANAGER.instance.desafioNum3SkyHook--;
                        GAMEMANAGER.instance.DesafioDeFase(OndeEstou.instance.fase);
                        GAMEMANAGER.instance.skyHook = true;
                        skyHook.Play("SkyHookTxt");
                        fezponto = false;
                    }

                    if(OndeEstou.instance.fase == 3)
                    {
                        skyHook.Play("SkyHookTxt");
                        GAMEMANAGER.instance.pontos += 100;
                        fezponto = false;
                    }
                    
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (GAMEMANAGER.instance.jogoExecutando)
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);

            if(hit.collider == null)
            Mirando();
        }
    }

    void MostraCaminho()
    {
        for(int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
        }
    }

    void EscondeCaminho()
    {
        for (int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    Vector2 PegaForca(Vector3 mouse)
    {
        return (new Vector2(starPos.x + valorX, starPos.y + valorY) - new Vector2(mouse.x, mouse.y)) * forca;
    }

    Vector2 CaminhoPonto(Vector2 posInicial, Vector2 velInicial, float tempo)
    {
        return posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;
    }

    void CalculoCaminho()
    {
        Vector2 vel = PegaForca(Input.mousePosition) * Time.fixedDeltaTime / meuRb.mass;


        for(int x =0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
            float t = x / 20f;
            Vector3 point = CaminhoPonto(transform.position, vel, t);
            point.z = 1.0f;
            caminho[x].transform.position = point;
        }
    }

    void Mirando()
    {
        if (tiro == true)
            return;

        if(Input.GetMouseButton(0))
        {
            if(GAMEMANAGER.instance.primeiraVez == 0)
            {
                GAMEMANAGER.instance.FimTutorial();
            }

            if(mirando == false)
            {
                mirando = true;
                starPos = Input.mousePosition;
                CalculoCaminho();
                MostraCaminho();
            }
            else
            {
                CalculoCaminho();
            }
        }
        else if (mirando && tiro == false)
        {
            meuRb.isKinematic = false;
            col.enabled = true;
            tiro = true;
            mirando = false;
            meuRb.AddForce(PegaForca(Input.mousePosition));
            EscondeCaminho();
        }
    }

    void OnBecameInvisible()
    {
        SegueBola.alvoInvisivel = true;    
    }
    void OnBecameVisible()
    {
        SegueBola.alvoInvisivel = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Aro"))
        {
            bateuAro = true;
        }

        if(collision.gameObject.CompareTag("Tabela"))
        {
            bateuTabela = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sky"))
            sky = true;
    }

}
