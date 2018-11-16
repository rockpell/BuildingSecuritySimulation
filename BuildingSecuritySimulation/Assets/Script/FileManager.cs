using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager instance;
    public JsonWrapper jsonWrapper;

    string filePath;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        jsonWrapper = new JsonWrapper();
    }

    public void Save()
    {
        List<TileData> _tileDataList = new List<TileData>();
        string _resultJson;
        Transform _tiles = GameObject.Find("Tiles").transform;

        for(int i = 0; i < _tiles.childCount; i++)
        {
            _tileDataList.Add(new TileData(_tiles.GetChild(i).position,
                _tiles.GetChild(i).GetComponent<Tile>().GetType(),
                _tiles.GetChild(i).GetComponent<Tile>().GetState(),
                _tiles.GetChild(i).GetComponent<Tile>().GetIsSecurity()
                ));
        }

        jsonWrapper.tiledatas = _tileDataList.ToArray();

        _resultJson = JsonUtility.ToJson(jsonWrapper, true);

        File.WriteAllText(Application.dataPath + "/Player.json", _resultJson);

        Debug.Log(_resultJson);
    }
    public void Load()
    { 

    }
    public void SaveLog()
    {

    }
}