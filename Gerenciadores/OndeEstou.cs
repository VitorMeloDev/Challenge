using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int fase = -1;
    public GameObject UI_MANAGERGO, GameManagerGO;

    public static OndeEstou instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(gameObject);
        }

        if (OndeEstou.instance.fase == 2)
        {
            Instantiate(UI_MANAGERGO);
        }


        SceneManager.sceneLoaded += VerificaFase;
    }
    private void Update()
    {
       
    }
    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;

        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1)
        {

            if (GameObject.Find("UIMANAGER(Clone)") == null)
            {
                Instantiate(UI_MANAGERGO);
            }

            if (GameObject.Find("GAMEMANAGER(Clone)") == null)
            {
                Instantiate(GameManagerGO);
            }

        }

    }
}
