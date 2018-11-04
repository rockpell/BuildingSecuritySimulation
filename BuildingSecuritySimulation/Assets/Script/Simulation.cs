using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

    public List<string> logData;
    private bool isPlaying;
    private bool isPaused;
    
    public void CreateCharacter()
    {

    }
    public void Play()
    {
        isPlaying = true;    
    }
    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    public void Stop()
    {
        isPlaying = false;
    }
    public void SaveLog()
    {

    }
    public void ShowLog(string tileData)
    {

    }
}
