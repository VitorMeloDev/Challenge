using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public Text desafio1, desafio2, desafio3;

    public Text numJogadas;

    public Toggle desafio1T, desafio2T, desafio3T;

    public Text moedas;

    public Button btnOk;

    public Text desafio1AP, desafio2AP, desafio3AP;

    public Text txtWinLose;
    public Button btnAvancar;
    public Button btnNovamente;
    public Button btnVoltar;

    public Text newRecord;

    //loja 
    public List<int> bolas;
    public Image menuImg;
    public Sprite[] imagemSp;
    public int aux = 0;

    public Button[] compraBtn;

    private Button cima, baixo;

    public Text moedasTXT;
    private int valorMoedas;

    public GameObject telaPausa;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        bolas = new List<int>();
        bolas.Add(0);

        if (!PlayerPrefs.HasKey("Bola0"))
        {
            PlayerPrefs.SetInt("Bola0", bolas[0]);
        }

        for (int i = 1; i < PlayerPrefs.GetInt("list_count"); i++)
        {
            bolas.Add(PlayerPrefs.GetInt("Bola" + i));
        }

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            numJogadas = GameObject.FindWithTag("NumBolas").GetComponent<Text>();
            numJogadas.text = "10";


            desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
            desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
            desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();

            moedas = GameObject.FindWithTag("MoedasTxt").GetComponent<Text>();
            btnOk = GameObject.FindWithTag("btnOK").GetComponent<Button>();

            desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
            desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
            desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();

            desafio1AP = GameObject.FindWithTag("desafio1AP").GetComponent<Text>();
            desafio2AP = GameObject.FindWithTag("desafio2AP").GetComponent<Text>();
            desafio3AP = GameObject.FindWithTag("desafio3AP").GetComponent<Text>();

            txtWinLose = GameObject.FindWithTag("txt_wl").GetComponent<Text>();
            btnAvancar = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();
            btnNovamente = GameObject.FindWithTag("btnNovamente").GetComponent<Button>();
            btnVoltar = GameObject.FindWithTag("btnVoltar").GetComponent<Button>();

            telaPausa = GameObject.FindWithTag("telaPausa");

            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];

            cima = GameObject.FindWithTag("btnCima").GetComponent<Button>();
            baixo = GameObject.FindWithTag("btnBaixo").GetComponent<Button>();

            cima.onClick.AddListener(Cima);
            baixo.onClick.AddListener(Baixo);
            aux = 0;
        }

        if (OndeEstou.instance.fase == 3)
        {
            numJogadas = GameObject.FindWithTag("NumBolas").GetComponent<Text>();
            numJogadas.text = "10";

            btnOk = GameObject.FindWithTag("btnOK").GetComponent<Button>();

            newRecord = GameObject.FindWithTag("NewTxt").GetComponent<Text>();
            newRecord.enabled = false;

            txtWinLose = GameObject.FindWithTag("txt_wl").GetComponent<Text>();
            //btnAvancar = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();
            btnNovamente = GameObject.FindWithTag("btnNovamente").GetComponent<Button>();
            btnVoltar = GameObject.FindWithTag("btnVoltar").GetComponent<Button>();

            telaPausa = GameObject.FindWithTag("telaPausa");

            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];

            cima = GameObject.FindWithTag("btnCima").GetComponent<Button>();
            baixo = GameObject.FindWithTag("btnBaixo").GetComponent<Button>();

            cima.onClick.AddListener(Cima);
            baixo.onClick.AddListener(Baixo);
            aux = 0;

        }
        SceneManager.sceneLoaded += Carrega;


        /* if (OndeEstou.instance.fase == 2)
         {
             moedasTXT = GameObject.FindWithTag("coinUI").GetComponent<Text>();
             valorMoedas = ScoreManager.instance.LoadDados();
             moedasTXT.text = valorMoedas.ToString();

             BtnCompraBola();
         }*/

    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            numJogadas = GameObject.FindWithTag("NumBolas").GetComponent<Text>();
            numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();

            moedas = GameObject.FindWithTag("MoedasTxt").GetComponent<Text>();

            desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
            desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
            desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();

            moedas = GameObject.FindWithTag("MoedasTxt").GetComponent<Text>();
            btnOk = GameObject.FindWithTag("btnOK").GetComponent<Button>();

            desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
            desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
            desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();

            desafio1AP = GameObject.FindWithTag("desafio1AP").GetComponent<Text>();
            desafio2AP = GameObject.FindWithTag("desafio2AP").GetComponent<Text>();
            desafio3AP = GameObject.FindWithTag("desafio3AP").GetComponent<Text>();

            txtWinLose = GameObject.FindWithTag("txt_wl").GetComponent<Text>();
            btnAvancar = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();
            btnNovamente = GameObject.FindWithTag("btnNovamente").GetComponent<Button>();
            btnVoltar = GameObject.FindWithTag("btnVoltar").GetComponent<Button>();

            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];

            cima = GameObject.FindWithTag("btnCima").GetComponent<Button>();
            baixo = GameObject.FindWithTag("btnBaixo").GetComponent<Button>();

            cima.onClick.AddListener(Cima);
            baixo.onClick.AddListener(Baixo);
            aux = 0;

        }

        if (OndeEstou.instance.fase == 3)
        {
            numJogadas = GameObject.FindWithTag("NumBolas").GetComponent<Text>();
            numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();

            btnOk = GameObject.FindWithTag("btnOK").GetComponent<Button>();

            newRecord = GameObject.FindWithTag("NewTxt").GetComponent<Text>();
            newRecord.enabled = false;

            txtWinLose = GameObject.FindWithTag("txt_wl").GetComponent<Text>();
            //btnAvancar = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();
            btnNovamente = GameObject.FindWithTag("btnNovamente").GetComponent<Button>();
            btnVoltar = GameObject.FindWithTag("btnVoltar").GetComponent<Button>();

            telaPausa = GameObject.FindWithTag("telaPausa");

            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];

            cima = GameObject.FindWithTag("btnCima").GetComponent<Button>();
            baixo = GameObject.FindWithTag("btnBaixo").GetComponent<Button>();

            cima.onClick.AddListener(Cima);
            baixo.onClick.AddListener(Baixo);
            aux = 0;

        }


        if (OndeEstou.instance.fase == 2)
        {
            moedasTXT = GameObject.FindWithTag("coinUI").GetComponent<Text>();
            valorMoedas = ScoreManager.instance.LoadDados();
            moedasTXT.text = valorMoedas.ToString();

            BtnCompraBola();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //ScoreManager.instance.SalvaDados(30000);
        if (OndeEstou.instance.fase != 2)
        {
            numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();
            btnOk = GameObject.FindWithTag("btnOK").GetComponent<Button>();
        }

        if (OndeEstou.instance.fase == 2)
        {
            moedasTXT = GameObject.FindWithTag("coinUI").GetComponent<Text>();
            valorMoedas = ScoreManager.instance.LoadDados();
            moedasTXT.text = valorMoedas.ToString();

            BtnCompraBola();
        }

        
    }
        public void CompraBola(int id)
        {
            if (id == 1)
            {
                if (ScoreManager.instance.LoadDados() >= 500)
                {
                    ScoreManager.instance.PerdeMoedas(1000);
                    valorMoedas = ScoreManager.instance.LoadDados();
                    moedasTXT.text = valorMoedas.ToString();
                    ChamaCompra(1);
                }
                else
                {
                }
            }

            if (id == 2)
            {
                if (ScoreManager.instance.LoadDados() >= 1000)
                {
                    ScoreManager.instance.PerdeMoedas(3000);
                    valorMoedas = ScoreManager.instance.LoadDados();
                    moedasTXT.text = valorMoedas.ToString();
                    ChamaCompra(2);
                }
                else
                {
                }
            }


            if (id == 3)
            {
                if (ScoreManager.instance.LoadDados() >= 3000)
                {
                    ScoreManager.instance.PerdeMoedas(3000);
                    valorMoedas = ScoreManager.instance.LoadDados();
                    moedasTXT.text = valorMoedas.ToString();
                    ChamaCompra(3);
                }
                else
                {
                }
            }


            if (id == 4)
            {
                if (ScoreManager.instance.LoadDados() >= 3000)
                {
                    ScoreManager.instance.PerdeMoedas(3000);
                    valorMoedas = ScoreManager.instance.LoadDados();
                    moedasTXT.text = valorMoedas.ToString();
                    ChamaCompra(4);
                }
                else
                {
                }
            }

        }

        void ChamaCompra(int id)
        {

            bolas.Add(id);

            PlayerPrefs.SetInt("list_count", bolas.Count);
            PlayerPrefs.SetInt("Bola" + id, id);
            compraBtn[id - 1].interactable = false;

            /*if (id != 2)
            {
                compraBtn[id].interactable = true;
            }*/

            if (bolas.Contains(id))
            {
                compraBtn[id - 1].GetComponentInChildren<Text>().text = "COMPRADO";
                compraBtn[id - 1].GetComponentInChildren<Text>().color = new Color(0, 1, 0, 1);
            }
        }

        void AjusteBolaBtn(int x)
        {
            compraBtn[x].interactable = false;
            compraBtn[x].GetComponentInChildren<Text>().text = "COMPRADO";
            compraBtn[x].GetComponentInChildren<Text>().color = new Color(0, 1, 0, 1);
        }

        void Cima()
        {
            if (aux >= 1)
            {
                aux--;
                menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + aux)];

            }
        }

        void Baixo()
        {
            if (aux < bolas.Count - 1)
            {
                aux++;
                menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + aux)];
            }
        }

        void BtnCompraBola()
        {
            compraBtn = new Button[4];

            compraBtn[0] = GameObject.FindWithTag("btnCompra1").GetComponent<Button>();
            compraBtn[1] = GameObject.FindWithTag("btnCompra2").GetComponent<Button>();
            compraBtn[2] = GameObject.FindWithTag("btnCompra3").GetComponent<Button>();
            compraBtn[3] = GameObject.FindWithTag("btnCompra4").GetComponent<Button>();

            compraBtn[0].onClick.AddListener(() => CompraBola(1));
            compraBtn[1].onClick.AddListener(() => CompraBola(2));
            compraBtn[2].onClick.AddListener(() => CompraBola(3));
            compraBtn[3].onClick.AddListener(() => CompraBola(4));

            if (bolas.Contains(1))
            {
                AjusteBolaBtn(0);

                /*if(!bolas.Contains(2))
                {
                    compraBtn[1].interactable = true;
                }*/
            }

            if (bolas.Contains(2))
            {
                AjusteBolaBtn(1);
            }


            if (bolas.Contains(3))
            {
                AjusteBolaBtn(2);
            }


            if (bolas.Contains(4))
            {
                AjusteBolaBtn(3);
            }
        }
        // Update is called once per frame
        void Update()
        {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2 && OndeEstou.instance.fase != 3)
        {
            numJogadas.text = GAMEMANAGER.instance.numJogadas.ToString();
        }
        }
} 

