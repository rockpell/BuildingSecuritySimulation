using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager instance;
    private JsonWrapper jsonWrapper;

    public GUISkin[] skins;
    public Texture2D file, folder, back, drive;
    private FileBrowser fileBrower;

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

        filePath = null;
        isFileBrowsing = false;

        fileBrower = new FileBrowser();

        fileBrower.guiSkin = skins[0]; //set the starting skin
                                       //set the various textures
        fileBrower.fileTexture = file;
        fileBrower.directoryTexture = folder;
        fileBrower.backTexture = back;
        fileBrower.driveTexture = drive;
        //show the search bar
        fileBrower.showSearch = true;
        //search recursively (setting recursive search may cause a long delay)
        fileBrower.searchRecursively = true;

    }

    void OnGUI()
    {
        if (isFileBrowsing)
        {
            if (fileBrower.draw())
            { //true is returned when a file has been selected
              //the output file is a member if the FileInfo class, if cancel was selected the value is null
                filePath = (fileBrower.outputFile == null) ? null : fileBrower.outputFile.ToString();
                Debug.Log(filePath);
                if (filePath != null)
                {
                    CloseFileBrowser();
                    if (!isSaveFile)
                    {
                        Load();
                    }
                }

                if (fileBrower.isSelect)
                {
                    CloseFileBrowser();
                    if (isSaveFile)
                    {
                        filePath = fileBrower.GetNowDirectory();
                        filePath = filePath.Replace("\\", "/");
                        Debug.Log(filePath);
                        Save();
                    }
                }
                else
                {
                    CloseFileBrowser();
                }

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

        //File.WriteAllText(Application.dataPath + "/Player.json", _resultJson);
        System.DateTime _myTime = System.DateTime.Now;
        string _result = _myTime.Year.ToString() + _myTime.Month.ToString() + _myTime.Day.ToString() + _myTime.Hour.ToString() + _myTime.Minute.ToString() + _myTime.Second.ToString();
        File.WriteAllText(filePath + "/" + _result  + ".json", _resultJson);
    }

    public void Load()
    {
        //string _loadData = File.ReadAllText(Application.dataPath + "/Player.json");
        string _loadData = File.ReadAllText(filePath);
        Debug.Log(_loadData);
        if (_loadData != null)
        {
            jsonWrapper = JsonUtility.FromJson<JsonWrapper>(_loadData);

            BuildManager.instance.LoadCreateTile(jsonWrapper.tiledatas); // 로드한 데이터를 오브젝트로 제작
        }
        else
        {
            Debug.Log("There is no data.");
        }
    }

    public void SaveLog(string data)
    {
        System.DateTime _myTime = System.DateTime.Now;
        string _result = _myTime.Year.ToString() + _myTime.Month.ToString() + _myTime.Day.ToString() + _myTime.Hour.ToString() + _myTime.Minute.ToString() + _myTime.Second.ToString();
        File.WriteAllText(Application.dataPath + "/log" + _result  + ".txt", data);
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
    }

}