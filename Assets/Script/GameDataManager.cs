using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameDataScrObj gameDataScrObj;

    private void Awake()
    {
        Instance = this;
        gameDataScrObj = Resources.Load("data") as GameDataScrObj;
        LoadGameData();
    }

    private void Start()
    {
    }

    public void SetKey(int a)
    {
        gameDataScrObj.keys += a;
    }

    public bool SetMusic()
    {
        return gameDataScrObj.musicOn = !gameDataScrObj.musicOn;
    }

    public bool SetVibrate()
    {
        return gameDataScrObj.vibrateOn = !gameDataScrObj.vibrateOn;
    }

    public void SetLevel()
    {
        gameDataScrObj.level++;
    }

    public void SetCoin(int a)
    {
        gameDataScrObj.totalCoin += a;
    }

    public void SetSkin1Mesh(Mesh a)
    {
        gameDataScrObj.skin1Mesh = a;
    }

    public void SetSkin1Material(Material a)
    {
        gameDataScrObj.skin1Material = a;
    }

    public void SetSkin2Mesh(Mesh a)
    {
        gameDataScrObj.skin2MeshFilterMesh = a;
    }

    public void SetSkin2Material(Material a)
    {
        gameDataScrObj.skin2MeshRendererMat = a;
    }

    public void SetSkin1VideoCount(int a)
    {
        gameDataScrObj.listOfSkin1BuyByVideo[a]++;
    }
    public void SaveGameData()
    {
        SetLevel();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "PlayerV2.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        var json = JsonUtility.ToJson(gameDataScrObj);
        formatter.Serialize(stream, json);
        stream.Close();
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "PlayerV2.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //GameDataScrObj gameDataScrObj = formatter.Deserialize(stream) as GameDataScrObj;
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), gameDataScrObj);
            stream.Close();
            //return gameDataScrObj;
        }
        else
        {
            Debug.Log("New Game");
        }
    }
}