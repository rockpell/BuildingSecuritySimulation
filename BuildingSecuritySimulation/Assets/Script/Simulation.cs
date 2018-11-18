using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

    public List<string> logData;
    private bool isPlaying;
    private bool isPaused;
    [SerializeField] private GameObject player;
    private GameObject playTmp;
    private Character nowPlayer;
    private float time = 0;
    private void Update()
    {
        if(isPlaying)
        {
            time += Time.deltaTime;
        }
    }
    //11.14 파라미터 추가
    public void CreateCharacter(bool isAuthority)
    {
        if (playTmp == null)
        {
            playTmp = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            playTmp.SendMessage("AuthoritySelect", isAuthority);
            nowPlayer = playTmp.GetComponent<Character>();
            time = 0;
        }
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
        Destroy(playTmp);
    }
    //필요 없을듯
    public void SaveLog()
    {

    }
    //필요 없을듯
    public void ShowLog(string tileData)
    {

    }
    public Character GetPlayer()
    {
        return nowPlayer;
    }
    public bool GetIsPlaying()
    {
        return isPlaying;
    }
    public bool GetIsPaused()
    {
        return isPaused;
    }
    public float GetTime()
    {
        return time;
    }
}
