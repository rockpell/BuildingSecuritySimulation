using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager instance;
    string filePath;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    public void SaveLog()
    {

    }
}
