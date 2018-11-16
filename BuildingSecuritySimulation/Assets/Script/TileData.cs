using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TileData {
    //public Vector3 position;
    public int tileType;
    public bool state;
    public bool isSecurity;

    //public TileData(Vector3 position, type tileType, bool state, bool isSecurity)
    //{
    //    this.position = position;
    //    this.tileType = tileType;
    //    this.state = state;
    //    this.isSecurity = isSecurity;
    //}

    public TileData(type tileType, bool state, bool isSecurity)
    {
        this.tileType = (int)tileType;
        this.state = state;
        this.isSecurity = isSecurity;
    }
}

[SerializeField]
public class Test
{
    public TileData tileData;

    public Test(TileData value)
    {
        this.tileData = value;
    }
}

[SerializeField]
public class JsonWrapper
{
    public TileData[] tiledatas;
}