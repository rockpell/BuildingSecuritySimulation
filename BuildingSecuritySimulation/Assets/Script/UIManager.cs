using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    [SerializeField] private Image pause;
    [SerializeField] private GameObject characterSelectWindow;
    [SerializeField] private GameObject CreateTileWindow;
    [SerializeField] private GameObject Pallet;
    [SerializeField] private Text warningText;
    [SerializeField] private Text ErrorMessage;
    [SerializeField] private ScrollRect LogWindow;
    [SerializeField] private GameObject InteractionObject;
    [SerializeField] private GameObject fileBrowserPanel;
    [SerializeField] private Text time;
    [SerializeField] private GameObject exitSavePanel;
    private Simulation simulation;
    private Character character;

    private GameObject width; // 사용자가 입력한 width
    private GameObject height; // 사용자가 입력한 height

    private bool isPaused = false;
    private bool isShowPallet = false;
    private bool isClickPalletArrow = true;
    private bool isMouseMoveClick = false;
    private bool isObjectSelectMode = false;
    private bool isLogShow = false;
    private Vector3 tempClickPosition;

    private string timeString;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }
    // Use this for initialization
    void Start () {
        //시뮬레이션 찾기
        simulation = GameObject.Find("Simulation").GetComponent<Simulation>();
        character = simulation.GetPlayer().GetComponent<Character>();
        //타일 생성시 가로세로 가져오기, 사용자가 입력한 값을 가져옴
        width = CreateTileWindow.transform.GetChild(0).gameObject;
        height = CreateTileWindow.transform.GetChild(1).gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // 화면 크기 조정
        {
            Camera.main.orthographicSize += 0.2f + (Camera.main.orthographicSize/50);
            if (Camera.main.orthographicSize > 100) Camera.main.orthographicSize = 100;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= 0.2f + (Camera.main.orthographicSize / 50);
            if (Camera.main.orthographicSize < 5) Camera.main.orthographicSize = 5;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) // 마우스 오른쪽 버튼 또는 가운데 버튼을 이용하여 화면 이동
        {
            tempClickPosition = Input.mousePosition;
            isMouseMoveClick = true;
        }
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)) isMouseMoveClick = false;

        if (isMouseMoveClick)
        {
            Camera.main.transform.Translate((tempClickPosition - Input.mousePosition) * 0.01f * Camera.main.orthographicSize * 0.5f);
            tempClickPosition = Input.mousePosition;

            if (Camera.main.transform.position.x < 5)
            {
                Camera.main.transform.position = new Vector3(5, Camera.main.transform.position.y, Camera.main.transform.position.z);
            } else if(Camera.main.transform.position.x > 80)
            {
                Camera.main.transform.position = new Vector3(80, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }

            if (Camera.main.transform.position.y > 0)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
            }
            else if (Camera.main.transform.position.y < -150)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -150, Camera.main.transform.position.z);
            }
        }
        if (simulation.GetIsPlaying())
        {
            time.gameObject.SetActive(true);
            if ((int)simulation.GetTime() / 60 < 10)
            {
                if ((int)simulation.GetTime() % 60 < 10)
                {
                    time.text = "0" + (int)simulation.GetTime() / 60 + " : 0" + (int)simulation.GetTime() % 60;
                }
                else
                {
                    time.text = "0" + (int)simulation.GetTime() / 60 + " : " + (int)simulation.GetTime() % 60;
                }
            }
            else
            {
                if ((int)simulation.GetTime() % 60 < 10)
                {
                    time.text = (int)simulation.GetTime() / 60 + " : 0" + (int)simulation.GetTime() % 60;
                }
                else
                {
                    time.text = (int)simulation.GetTime() / 60 + " : " + (int)simulation.GetTime() % 60;
                }
            }
            timeString = time.text;
        }
    }

    public void CreateTile()
    {
        if (simulation.GetIsPlaying()) return;
        CreateTileWindow.SetActive(true);
    }

    public void Save()
    {
        if (simulation.GetIsPlaying()) return;
        FileManager.instance.IsFileBrowsing = true;
        FileManager.instance.IsSaveFile = true;

        fileBrowserPanel.SetActive(true);
        CreateTileWindow.SetActive(false);
        //FileManager.instance.Save();
    }

    public void Load()
    {
        if (simulation.GetIsPlaying()) return;
        FileManager.instance.IsFileBrowsing = true;
        FileManager.instance.IsSaveFile = false;

        fileBrowserPanel.SetActive(true);
        CreateTileWindow.SetActive(false);
        //FileManager.instance.Load();
    }

    public void ObjectSelect()
    {
        if (simulation.GetIsPlaying()) return;
        BuildManager.instance.SetObjectSelectMode(true);
        isObjectSelectMode = true;
    }

    public void Play()
    {
        if (BuildManager.instance.GetIsSetTileAndSecurity() && !simulation.GetIsPlaying())
        {
            characterSelectWindow.SetActive(true);
            Pallet.SetActive(false);
            simulation.Play();
        }
        else
        {
            StartCoroutine(ShowErrorMessage("이미 시뮬레이션이 동작중입니다."));
        }
    }

    public void Pause()
    {
        if (!simulation.GetIsPlaying()) return;
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
        LogWindow.gameObject.SetActive(false);
        Pallet.SetActive(true);
    }

    public void Exit()
    {
        if (simulation.GetIsPlaying()) return;
        //파일 저장할지 않할지 창 띄워야함
        characterSelectWindow.SetActive(false);
        CreateTileWindow.SetActive(false);
        exitSavePanel.SetActive(true);
        //Application.Quit();
    }

    public void ShowLog()
    {
        FileManager.instance.SaveLog();
    }

    public void ShowPallet()
    {
        if(isClickPalletArrow) StartCoroutine(MovePallet());
    }
    // 11.14 캐릭터 생성때문에 이 함수 사용 파라미터 추가
    public void CharacterSelect(bool isAuthority)
    {
        simulation.CreateCharacter(isAuthority);
        characterSelectWindow.SetActive(false);
        LogWindow.gameObject.SetActive(true);
    }

    public void PalletSelect(int index)
    {
        if (isObjectSelectMode) ObjectDiselect(); // 팔레트의 타일을 선택하면 개체선택모드 해제
        BuildManager.instance.SelectTileType(index);
        BuildManager.instance.SetObjectSelectMode(false);
    }
    // 11.14 캐릭터 생성때문에 문제가 있음
    public void getNoAuthority()
    {
        character.AuthoritySelect(false);
        
    }
    // 11.14 캐릭터 생성때문에 문제가 있음
    public void getAuthority()
    {
        character.AuthoritySelect(true);
        //simulation.CreateCharacter();
        characterSelectWindow.SetActive(false);
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

    public void CloseFileBrowserPanel()
    {
        fileBrowserPanel.SetActive(false);
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
                distance -= 15;
                rect.localPosition -= new Vector3(15, 0, 0);
            }
            else
            {
                distance -= 15;
                rect.localPosition += new Vector3(15, 0, 0);
            }
            
        }
        yield return null;
        isClickPalletArrow = true;
        if (isShowPallet) isShowPallet = false;
        else isShowPallet = true;
    }

    private void ObjectDiselect()
    {
        BuildManager.instance.SetObjectSelectMode(false);
        isObjectSelectMode = false;
    }
    public IEnumerator ShowErrorMessage(string errorMessage)
    {
        ErrorMessage.text = errorMessage;
        ErrorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ErrorMessage.gameObject.SetActive(false);
    }
    public void ChangeInteractionText(bool isShow)
    {
        InteractionObject.SetActive(isShow);
    }
    public void ChangeLogMessage(int securityIndex)
    {
        if (!isLogShow)
        {
            Text logText = LogWindow.content.GetComponent<Text>();
            logText.text += securityIndex + "번 시스템위치에서 문이 열려있습니다. (" + timeString + ")\n";
            LogWindow.verticalScrollbar.value = 0;
            isLogShow = true;
            StartCoroutine(ChangeIsLogShow());
        }
    }
    IEnumerator ChangeIsLogShow()
    {
        yield return new WaitForSeconds(0.1f);
        isLogShow = false;
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
