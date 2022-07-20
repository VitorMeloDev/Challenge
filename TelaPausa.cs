using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TelaPausa : MonoBehaviour
{
    public Animator pauseMenuAnim;

    void Start()
    {
        pauseMenuAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Store()
    {
        SceneManager.LoadScene(2);
    }


    public void AbrirMenu()
    {
        if(GAMEMANAGER.instance.jogoExecutando)
        {
            pauseMenuAnim.Play("PauseAnim");
            GAMEMANAGER.instance.jogoExecutando = false;
        }
        
    }

    public void FecharMenu()
    {
        if (GAMEMANAGER.instance.jogoExecutando == false)
        {
            pauseMenuAnim.Play("PauseAnimClose");
            GAMEMANAGER.instance.jogoExecutando = true;
        }
    }
}
