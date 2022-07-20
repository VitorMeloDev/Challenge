using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GAMEMANAGER : MonoBehaviour
{

    [System.Serializable]
    public class Desafios
    {
        public int ondeEstou;
        public string desafioRim, desafioSwish, desafioSky;
        public int desafioInt1RimShot = 0, desafioInt2Swish = 0, desafioInt3SkyHook = 0;
        public int numeroJogadas;
    }

    public List<Desafios> desafiosList;

    void ListaAdd()
    {
        foreach (Desafios desaf in desafiosList)
        {
            if (desaf.ondeEstou == OndeEstou.instance.fase)
            {
                UI_Manager.instance.desafio1.text = desaf.desafioRim;
                UI_Manager.instance.desafio2.text = desaf.desafioSwish;
                UI_Manager.instance.desafio3.text = desaf.desafioSky;

                desafioNum1RimShoot = desaf.desafioInt1RimShot;
                desafioNum2SwishShoot = desaf.desafioInt2Swish;
                desafioNum3SkyHook = desaf.desafioInt3SkyHook;

                UI_Manager.instance.desafio1AP.text = desaf.desafioRim;
                UI_Manager.instance.desafio2AP.text = desaf.desafioSwish;
                UI_Manager.instance.desafio3AP.text = desaf.desafioSky;

                numJogadas = desaf.numeroJogadas;

                break;
            }
        }
    }

    public int desafioNum1RimShoot, desafioNum2SwishShoot, desafioNum3SkyHook;

    public static GAMEMANAGER instance;

    public bool bolaEmCena;
    public int numJogadas;
    public GameObject[] bolasPrefab;
    private Transform posDireita, posEsquerda, posBaixo, posCima;
    public bool jogoExecutando = true, win = false, lose = false;


    //tutorial

    public GameObject mao;
    public Animator maoAnim;
    public int primeiraVez = 0;

    //identifica pontos
    public int pontos = 1;

    //RimShot
    public bool rimShot = false, swishShot = false, skyHook = false;

    public int moedasIntSave;

    public GameObject fundo, tela, telaWL;
    public Animator contAnim;



    void Awake()
    {
        if (PlayerPrefs.HasKey("PrimeiraVez") == null)
        {
            PlayerPrefs.SetInt("PrimeiraVez", 0);
        }
        else
        {
            primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
        {
            if (OndeEstou.instance.fase == 3)
                numJogadas = 10;
            
            /*ListaAdd();
            jogoExecutando = false;
            posDireita = GameObject.FindWithTag("DIREITA_POS").GetComponent<Transform>();
            posEsquerda = GameObject.FindWithTag("ESQUERDA_POS").GetComponent<Transform>();
            posCima = GameObject.FindWithTag("CIMA_POS").GetComponent<Transform>();
            posBaixo = GameObject.FindWithTag("BAIXO_POS").GetComponent<Transform>();


            telaWL = GameObject.FindWithTag("telaWL");
            tela = GameObject.FindWithTag("telaDesafio");
            fundo = GameObject.FindWithTag("fundoC");
            contAnim = GameObject.FindWithTag("contador").GetComponent<Animator>();

            UI_Manager.instance.btnOk.onClick.AddListener(LiberaContagem);
            UI_Manager.instance.btnVoltar.onClick.AddListener(Voltar);
            UI_Manager.instance.btnNovamente.onClick.AddListener(Novamente);
            if(OndeEstou.instance.fase !=3)
            UI_Manager.instance.btnAvancar.onClick.AddListener(Avancar);


            */
            primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
            if (primeiraVez == 0 || primeiraVez == 1)
            {
                PegaSprites();
                if (primeiraVez == 1)
                {
                    DestroiMao(mao.gameObject);
                }
            }

           

            
        }

        SceneManager.sceneLoaded += Carrega;
    }


    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            moedasIntSave = ScoreManager.instance.LoadDados();
            UI_Manager.instance.moedas.text = moedasIntSave.ToString();
        }

        if(OndeEstou.instance.fase ==3)
            numJogadas = 10;

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
        {
            ListaAdd();
            jogoExecutando = false;
            posDireita = GameObject.FindWithTag("DIREITA_POS").GetComponent<Transform>();
            posEsquerda = GameObject.FindWithTag("ESQUERDA_POS").GetComponent<Transform>();
            posCima = GameObject.FindWithTag("CIMA_POS").GetComponent<Transform>();
            posBaixo = GameObject.FindWithTag("BAIXO_POS").GetComponent<Transform>();
            numJogadas = 10;


            telaWL = GameObject.FindWithTag("telaWL");
            tela = GameObject.FindWithTag("telaDesafio");
            fundo = GameObject.FindWithTag("fundoC");
            contAnim = GameObject.FindWithTag("contador").GetComponent<Animator>();

            UI_Manager.instance.btnOk.onClick.AddListener(LiberaContagem);
            UI_Manager.instance.btnVoltar.onClick.AddListener(Voltar);
            UI_Manager.instance.btnNovamente.onClick.AddListener(Novamente);
            if (OndeEstou.instance.fase != 3)
            UI_Manager.instance.btnAvancar.onClick.AddListener(Avancar);



            primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
            if (primeiraVez == 0 || primeiraVez == 1)
            {
                PegaSprites();
                if (primeiraVez == 1)
                {
                    DestroiMao(mao.gameObject);
                }
            }
        }
    }

    void LiberaContagem()
    {
        tela.SetActive(false);
        fundo.SetActive(false);
        telaWL.SetActive(false);
        contAnim.Play("Contador");
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        ScoreManager.instance.SalvaDados(3000);

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
        {
            ListaAdd();
            pontos = 0;
            bolaEmCena = true;

            jogoExecutando = false;
            posDireita = GameObject.FindWithTag("DIREITA_POS").GetComponent<Transform>();
            posEsquerda = GameObject.FindWithTag("ESQUERDA_POS").GetComponent<Transform>();
            posCima = GameObject.FindWithTag("CIMA_POS").GetComponent<Transform>();
            posBaixo = GameObject.FindWithTag("BAIXO_POS").GetComponent<Transform>();


            telaWL = GameObject.FindWithTag("telaWL");
            tela = GameObject.FindWithTag("telaDesafio");
            fundo = GameObject.FindWithTag("fundoC");
            contAnim = GameObject.FindWithTag("contador").GetComponent<Animator>();

            UI_Manager.instance.btnOk.onClick.AddListener(LiberaContagem);
            UI_Manager.instance.btnVoltar.onClick.AddListener(Voltar);
            UI_Manager.instance.btnNovamente.onClick.AddListener(Novamente);
            if (OndeEstou.instance.fase != 3)
            UI_Manager.instance.btnAvancar.onClick.AddListener(Avancar);


            win = false;
            lose = false;
        }

        if (OndeEstou.instance.fase == 3)
            numJogadas = 10;

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            moedasIntSave = ScoreManager.instance.LoadDados();
            UI_Manager.instance.moedas.text = moedasIntSave.ToString();
        }
        /*
        if(OndeEstou.instance.fase ==3)
        {
            ListaAdd();
            pontos = 0;
            bolaEmCena = true;

            UI_Manager.instance.btnOk.onClick.AddListener(LiberaContagem);
            UI_Manager.instance.btnVoltar.onClick.AddListener(Voltar);
            UI_Manager.instance.btnNovamente.onClick.AddListener(Novamente);
            UI_Manager.instance.btnAvancar.onClick.AddListener(Avancar);


            win = false;
            lose = false;
        }*/


    }

    // Update is called once per frame
    void Update()
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            if (numJogadas <= 0)
            {
                if(desafioNum1RimShoot > 0 || desafioNum2SwishShoot > 0 || desafioNum3SkyHook > 0)
                {
                    Lose();
                }
            }
            else if (numJogadas > 0 && desafioNum1RimShoot <= 0 && desafioNum2SwishShoot <= 0 && desafioNum3SkyHook <= 0)
            {
                Win();
            }
        }
          
        if(OndeEstou.instance.fase == 3)
        {
            if(numJogadas <= 0)
            FIM();
        }
            

    }

    public void Avancar()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase +1);
    }

    public void Novamente()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase);
    }

    public void Voltar()
    {
        SceneManager.LoadScene(0);
    }

    public void NascBolas()
    {
        Instantiate(bolasPrefab[UI_Manager.instance.aux], new Vector2(Random.Range(posEsquerda.position.x, posDireita.position.x), Random.Range(posCima.position.y, posBaixo.position.y)), Quaternion.identity);
        bolaEmCena = true;
    }

    public void FimTutorial()
    {
        Destroy(mao);
        PlayerPrefs.SetInt("PrimeiraVez", 1);
    }

    void DestroiMao(GameObject obj)
    {
        Destroy(obj);
    }

    void PegaSprites()
    {
        mao = GameObject.FindWithTag("mao");
        maoAnim = mao.GetComponent<Animator>();
    }


    public void DesafioDeFase(int fase)
    {
        if (OndeEstou.instance.fase == fase)
        {
            if (desafioNum1RimShoot == 0)
            {
                UI_Manager.instance.desafio1T.isOn = true;
            }

            if (desafioNum2SwishShoot == 0)
            {
                UI_Manager.instance.desafio2T.isOn = true;
            }

            if (desafioNum3SkyHook == 0)
            {
                UI_Manager.instance.desafio3T.isOn = true;
            }
        }
    }

    void Win()
    {
        if(jogoExecutando)
        {
            jogoExecutando = false;
            win = true;
            ScoreManager.instance.SalvaDados(moedasIntSave);
            telaWL.SetActive(true);
            UI_Manager.instance.txtWinLose.text = "YOU WIN";
            int temp = OndeEstou.instance.fase -3;
            temp++;
            PlayerPrefs.SetInt("Level" + temp, 1);
            if(OndeEstou.instance.fase == 13)
            {
                UI_Manager.instance.btnAvancar.enabled = false;
            }
        }
    }

    void Lose()
    {
        if (jogoExecutando)
        {
            jogoExecutando = false;
            lose = true; 
            telaWL.SetActive(true);
            UI_Manager.instance.btnAvancar.enabled = false;
            UI_Manager.instance.txtWinLose.text = "YOU LOSE";
            
        }
    }

    void FIM()
    {
        if (jogoExecutando)
        {
            jogoExecutando = false;
            lose = true;
            telaWL.SetActive(true);
            //UI_Manager.instance.btnAvancar.enabled = false;
            UI_Manager.instance.txtWinLose.text = "GAME OVER";
        }
    }

    public void PrimeiraJogada()
    {
        if(jogoExecutando && primeiraVez == 0)
        {
            if(mao != null)
            {
                maoAnim.Play("Mão_Tutorial");
            }
        }
    }
}
