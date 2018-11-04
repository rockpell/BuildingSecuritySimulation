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
    private void Start()
    {
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
