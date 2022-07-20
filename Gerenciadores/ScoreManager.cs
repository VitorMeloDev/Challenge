using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SalvaDados(int moeda)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/dadoscoinDataB.data");

        SaveDados coin = new SaveDados();
        coin.moedas = moeda;

        bf.Serialize(fs, coin);
        fs.Close();
    }

    public int LoadDados()
    {
        int moeda = 0;


        if (File.Exists(Application.persistentDataPath + "/dadoscoinDataB.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/dadoscoinDataB.data", FileMode.Open);

            SaveDados coin = (SaveDados)bf.Deserialize(fs);
            fs.Close();

            moeda = (int)coin.moedas;

        }

        return moeda;

    }

    public void PerdeMoedas(int moeda)
    {
        int tempMoeda, novoVal;

        tempMoeda = LoadDados();

        novoVal = tempMoeda - moeda;

        SalvaDados(novoVal);
    }

    [Serializable]
    class SaveDados
    {
        public int moedas;
    }




}
