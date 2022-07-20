using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPrefabs : MonoBehaviour
{
    public GameObject uimanager;
    void Awake()
    {
        
    }

    private void Start()
    {
        uimanager = GameObject.FindWithTag("uimanager");
        if (OndeEstou.instance.fase == 0 && OndeEstou.instance.fase == 1)
        {
            Destroy(uimanager);
        }

        if (OndeEstou.instance.fase == 0 && OndeEstou.instance.fase == 1)
        {
            Destroy(GameObject.Find("GAMEMANAGER(Clone)"));
        }

        if (OndeEstou.instance.fase == 2)
        {
            Destroy(GameObject.Find("GAMEMANAGER(Clone)"));
        }
    }
}
