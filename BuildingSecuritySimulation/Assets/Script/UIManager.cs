using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Image pause;
    [SerializeField] private GameObject characterSelectWindow;
    [SerializeField] private GameObject CreateTileWindow;
    [SerializeField] private Text warningText;
    private Simulation simulation;
    private Character character;
    private bool isPaused = false;
    private GameObject width;
    private GameObject height;
    // Use this for initialization
    void Start () {
        simulation = GameObject.Find("Simulation").GetComponent<Simulation>();
        width = CreateTileWindow.transform.GetChild(0).gameObject;
        height = CreateTileWindow.transform.GetChild(1).gameObject;
        //캐릭터 가져와야함
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void CreateTile()
    {
        CreateTileWindow.SetActive(true);
    }

    public void Save()
    {
        FileManager.instance.Save();
    }

    public void Load()
    {
        FileManager.instance.Load();
    }

    public void Play()
    {
        characterSelectWindow.SetActive(true);
        simulation.Play();
    }

    public void Pause()
    {
        //나중에 수정 필요
        if (!isPaused)
        {
            isPaused = true;
            pause.color = Color.red;
        }
        else
        {
            isPaused = false;
            pause.color = Color.white;
        }
        simulation.Pause();
    }

    public void Stop()
    {
        simulation.Stop();
    }

    public void Exit()
    {
        //파일 저장할지 않할지 창 띄워야함
        Application.Quit();
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
        //이거 안쓸듯
    }

    public void PalletSelect()
    {

    }
    public void getNoAuthority()
    {
        character.AuthoritySelect(false);
    }
    public void getAuthority()
    {
        character.AuthoritySelect(true);
    }
    public void CharacterSelectCancle()
    {
        characterSelectWindow.SetActive(false);
    }

    public void MakeCreateTile()
    {
        if(width.transform.GetComponentInChildren<InputField>().text == "" &&
            height.transform.GetComponentInChildren<InputField>().text =="")
        {
            warningText.gameObject.SetActive(true);
            return;
        }
        int widthValue = System.Convert.ToInt32(width.transform.GetComponentInChildren<InputField>().text);
        int heightValue = System.Convert.ToInt32(height.transform.GetComponentInChildren<InputField>().text);
        if (widthValue >= 10 && widthValue <= 100 && heightValue >= 10 && heightValue <= 100)
        {
            CreateTileWindow.SetActive(false);
            BuildManager.instance.CreateTile(widthValue, heightValue);
        }
        else
        {
            warningText.gameObject.SetActive(true);
        }
    }
    public void CreateTileCancle()
    {
        CreateTileWindow.SetActive(false);
    }
}
