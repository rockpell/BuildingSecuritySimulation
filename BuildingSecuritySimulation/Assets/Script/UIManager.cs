using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private Button pausedButton;
    [SerializeField] private Button stopButton;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateTile()
    {

    }

    public void Save()
    {
        FileManager.instance.Save();
    }

    public void Load()
    {
        FileManager.instance.Load();
    }

    public void Pause()
    {
    }

    public void Stop()
    {

    }

    public void Exit()
    {

    }

    public void ShowLog()
    {
        FileManager.instance.SaveLog();
    }

    public void ShowPallet()
    {

    }

    public void CharacterSelect()
    {

    }

    public void PalletSelect()
    {

    }
}
