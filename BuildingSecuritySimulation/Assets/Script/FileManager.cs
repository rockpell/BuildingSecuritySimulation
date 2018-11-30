using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager instance;
    private JsonWrapper jsonWrapper;

    public GUISkin[] skins;
    public Texture2D file, folder, back, drive;
    private FileBrowser fileBrower; // 외부 라이브러리

    private string filePath;
    private bool isFileBrowsing;
    private bool isSaveFile;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        jsonWrapper = new JsonWrapper();

        ResetFileBrowser();
    }

    void OnGUI()
    {
        if (isFileBrowsing)
        {
            if (fileBrower.draw())
            { 
                filePath = (fileBrower.outputFile == null) ? null : fileBrower.outputFile.FullName;

                if (filePath != null)
                {
                    if (!isSaveFile)
                    {
                        Load();
                    }
                    CloseFileBrowser();
                }
                if (fileBrower.isSelect)
                {
                    if (isSaveFile)
                    {
                        filePath = fileBrower.GetNowDirectory();
                        filePath = filePath.Replace("\\", "/");
                        Debug.Log(filePath);
                        Save();
                    }
                }
                
                CloseFileBrowser();
            }
        }
    }

    public void Save()
    {
        List<TileData> _tileDataList = new List<TileData>();
        string _resultJson;
        Transform _tiles = GameObject.Find("Tiles").transform;

        for (int i = 0; i < _tiles.childCount; i++)
        {
            _tileDataList.Add(new TileData(_tiles.GetChild(i).position,
                _tiles.GetChild(i).GetComponent<Tile>().GetType(),
                _tiles.GetChild(i).GetComponent<Tile>().GetIsSecurity()
                ));
        }

        jsonWrapper.tiledatas = _tileDataList.ToArray();

        _resultJson = JsonUtility.ToJson(jsonWrapper, true);

        System.DateTime _myTime = System.DateTime.Now;
        string _result = _myTime.Year.ToString() + _myTime.Month.ToString() + _myTime.Day.ToString() + _myTime.Hour.ToString() + _myTime.Minute.ToString() + _myTime.Second.ToString();
        File.WriteAllText(filePath + "/" + _result  + ".json", _resultJson);
        if (UIManager.instance.IsExit)
        {
            Debug.Log("Application.Quit();");
            Application.Quit();
        }
    }

    public void Load()
    {
        string _loadData = File.ReadAllText(filePath);
        Debug.Log(_loadData);
        if (_loadData != null)
        {
            jsonWrapper = JsonUtility.FromJson<JsonWrapper>(_loadData);
            if(jsonWrapper.tiledatas != null)
            {
                BuildManager.instance.LoadCreateTile(jsonWrapper.tiledatas); // 로드한 데이터를 오브젝트로 제작
            }
            else // 지원하지 않는 json 양식일 경우
            {
                UIManager.instance.ShowErrorMessageStart("잘못된 파일입니다.");
            }
            
        }
        else
        {
            UIManager.instance.ShowErrorMessageStart("내용이 없는 파일입니다.");
        }
    }

    public void SaveLog(string data)
    {
        System.DateTime _myTime = System.DateTime.Now;
        string _result = _myTime.Year.ToString() + _myTime.Month.ToString() + _myTime.Day.ToString() + _myTime.Hour.ToString() + _myTime.Minute.ToString() + _myTime.Second.ToString();
        if(Directory.Exists(Application.dataPath + "/logs/log"))
        {
        }
        else
        {
            Directory.CreateDirectory(Application.dataPath + "/logs");
        }
        File.WriteAllLines(Application.dataPath + "/logs/log" + _result + ".txt", data.Split('\n'));
    }

    public bool IsFileBrowsing {
        get { return isFileBrowsing; }
        set { isFileBrowsing = value; }
    }

    public bool IsSaveFile {
        get { return isSaveFile; }
        set { isSaveFile = value; }
    }

    private void CloseFileBrowser()
    {
        isFileBrowsing = false;
        UIManager.instance.CloseFileBrowserPanel();
        ResetFileBrowser();
    }

    private void ResetFileBrowser()
    {
        filePath = null;
        isFileBrowsing = false;

        fileBrower = new FileBrowser();
        fileBrower.guiSkin = skins[0];
        fileBrower.fileTexture = file;
        fileBrower.directoryTexture = folder;
        fileBrower.backTexture = back;
        fileBrower.driveTexture = drive;
        fileBrower.showSearch = false;
        fileBrower.searchRecursively = true;
    }

}