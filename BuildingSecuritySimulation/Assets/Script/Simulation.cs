using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

    public List<string> logData;
    private bool isPlaying;
    private bool isPaused;
    [SerializeField] private GameObject player;
    //11.14 파라미터 추가
    public void CreateCharacter(bool isAuthority)
    {
        GameObject playTmp = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        playTmp.SendMessage("AuthoritySelect", isAuthority);
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
    public GameObject GetPlayer()
    {
        return player;
    }
    public bool GetIsPlaying()
    {
        return isPlaying;
    }
    public bool GetIsPaused()
    {
        return isPaused;
    }
}
