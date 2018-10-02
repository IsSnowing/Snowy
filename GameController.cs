using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int scores { set; get; }
    public int highest_scores { set; get; }
    public int coins { set; get; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);
        DontDestroyOnLoad(instance);
        //File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        //UnityEditor.AssetDatabase.Refresh();
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            scores = 0;
            highest_scores = 0;
            coins = 0;
            Save();
        }
        else
        {
            Load();
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.scores = instance.scores;
        data.highest_scores = instance.highest_scores;
        data.coins = instance.coins;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            instance.scores = data.scores;
            instance.highest_scores = data.highest_scores;
            instance.coins = data.coins;
        }
    }

    [Serializable]
    class PlayerData
    {
        public int scores;
        public int highest_scores;
        public int coins;

    }

}
