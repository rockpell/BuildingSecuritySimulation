using System.Collections;
using System.Collections.Generic;
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
            //_tileDataList.Add(new TileData(_tiles.GetChild(i).position,
            //    _tiles.GetChild(i).GetComponent<Tile>().GetType(),
            //    _tiles.GetChild(i).GetComponent<Tile>().GetState(),
            //    _tiles.GetChild(i).GetComponent<Tile>().GetIsSecurity()
            //    ));
            _tileDataList.Add(new TileData(
                _tiles.GetChild(i).GetComponent<Tile>().GetType(),
                _tiles.GetChild(i).GetComponent<Tile>().GetState(),
                _tiles.GetChild(i).GetComponent<Tile>().GetIsSecurity()
                ));
        }

        jsonWrapper.tiledatas = _tileDataList.ToArray();
        Debug.Log(jsonWrapper.tiledatas.Length);


        //_resultJson = JsonUtility.ToJson(jsonWrapper, true);
        _resultJson = JsonUtility.ToJson(new Test(new TileData(type.Blank, false, false)), true);
        Debug.Log(_resultJson);
    }
    public void Load()
    { 

    }
    public void SaveLog()
    {

    }
}