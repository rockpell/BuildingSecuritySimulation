using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Image pause;
    [SerializeField] private GameObject characterSelectWindow;
    [SerializeField] private GameObject CreateTileWindow;
    [SerializeField] private GameObject Pallet;
    [SerializeField] private Text warningText;
    
    private Simulation simulation;
    private Character character;

    private GameObject width;
    private GameObject height;

    private bool isPaused = false;
    private bool isShowPallet = false;
    private bool isClickPalletArrow = true;
    private bool isMouseMoveClick = false;

    private Vector3 tempClickPosition;

    // Use this for initialization
    void Start () {
        //시뮬레이션 찾기
        simulation = GameObject.Find("Simulation").GetComponent<Simulation>();
        //타일 생성시 가로세로 가져오기
        width = CreateTileWindow.transform.GetChild(0).gameObject;
        height = CreateTileWindow.transform.GetChild(1).gameObject;
        //캐릭터 가져와야함
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += 0.2f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= 0.2f;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            tempClickPosition = Input.mousePosition;
            isMouseMoveClick = true;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonDown(2)) isMouseMoveClick = false;

        if (isMouseMoveClick)
        {
            Camera.main.transform.Translate((tempClickPosition - Input.mousePosition) * 0.01f * Camera.main.orthographicSize * 0.5f);
            tempClickPosition = Input.mousePosition;
        }
        
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
        if(isClickPalletArrow) StartCoroutine(MovePallet());
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
        //입력 값이 공백이면 텍스트를 띄운다
        if(width.transform.GetComponentInChildren<InputField>().text == "" &&
            height.transform.GetComponentInChildren<InputField>().text =="")
        {
            warningText.gameObject.SetActive(true);
            return;
        }
        //입력값을 int로 가져오기
        int widthValue = System.Convert.ToInt32(width.transform.GetComponentInChildren<InputField>().text);
        int heightValue = System.Convert.ToInt32(height.transform.GetComponentInChildren<InputField>().text);
        //제대로된 입력값이 들어오면 타일 생성
        if (widthValue >= 10 && widthValue <= 100 && heightValue >= 10 && heightValue <= 100)
        {
            CreateTileWindow.SetActive(false);
            warningText.gameObject.SetActive(false);
            BuildManager.instance.CreateTile(widthValue, heightValue);
        }
        else
        {
            StartCoroutine(ShowWarningText());
        }
    }
    public void CreateTileCancle()
    {
        CreateTileWindow.SetActive(false);
    }
    private IEnumerator ShowWarningText()
    {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        warningText.gameObject.SetActive(false);
        yield return null;
    }
    private IEnumerator MovePallet()
    {
        int distance = 500;
        RectTransform rect = Pallet.GetComponent<RectTransform>();
        isClickPalletArrow = false;
        while (distance > 0)
        {
            yield return new WaitForSeconds(0.001f);
            if (!isShowPallet)
            {
                distance -= 5;
                rect.localPosition -= new Vector3(5, 0, 0);
            }
            else
            {
                distance -= 5;
                rect.localPosition += new Vector3(5, 0, 0);
            }
            
        }
        yield return null;
        isClickPalletArrow = true;
        if (isShowPallet) isShowPallet = false;
        else isShowPallet = true;
    }
}
