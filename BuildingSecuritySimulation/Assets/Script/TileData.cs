using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData {
    public Vector3 position;
    public int tileType;
    public bool isSecurity;

    public TileData(Vector3 position, type tileType, bool isSecurity)
    {
        this.position = position;
        this.tileType = (int)tileType;
        this.isSecurity = isSecurity;
    }
}

[Serializable]
public class Test
{
    public List<TileData> tileList;

    public Test(List<TileData> value)
    {
        this.tileList = value;
    }
}

[Serializable]
public class JsonWrapper
{
    public TileData[] tiledatas;
}