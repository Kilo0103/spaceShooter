using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using DataInfo;

public class DataManager : MonoBehaviour
{
    private string dataPath;

    public void Initialized()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }

    public void Save(GameData gameData)
    {
        BinaryFormatter df = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        //파일에 저장할 클래스에 데이터 할당
        GameData data = new GameData();
        data.killCount  = gameData.killCount;
        data.hp = gameData.hp;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;

        //BinaryFormatter를 이용하여 데이터 기록
        df.Serialize(file, data);
        file.Close();
    }

    public GameData Load()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            GameData data = new GameData();

            return data;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
