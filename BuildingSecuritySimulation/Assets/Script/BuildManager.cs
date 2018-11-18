using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;
    private type selectedType;
    private string saveTile;
    private Tile[] tileArray;

    [SerializeField] private Transform tiles;

    private Vector3 mouseButtonDownPosition;
    private Vector3 mouseButtonUpPosition;

    private Sprite normalTileSprite;    // 일반 타일 이미지
    private Sprite doorTileSprite;  // 문 타일 이미지
    private Sprite windowTileSprite;    // 창문 타일 이미지
    private Sprite wallTileSprite;  // 벽 타일 이미지
    private Sprite securitySprite;  // 벽 타일 이미지

    private bool isObjectSelectMode;                //개체 선택 모드인지
    private bool isSetTile;                         // 타일이 설치 되었는지
    private bool isSetSecurity;                     // 보안 시스템이 설치 되었는지
    private int securityIndex = 1;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Use this for initialization
    void Start () {
        //기본 안해주면 삭제됨
        selectedType = type.Blank;

        normalTileSprite = Resources.Load<Sprite>("Sprites/nomarl_tile");
        wallTileSprite = Resources.Load<Sprite>("Sprites/wall");
        doorTileSprite = Resources.Load<Sprite>("Sprites/door");
        windowTileSprite = Resources.Load<Sprite>("Sprites/window");
        securitySprite = Resources.Load<Sprite>("Sprites/siren");
    }
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(0) && !isObjectSelectMode)
            {
                mouseButtonDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if(Input.GetMouseButtonDown(0) && isObjectSelectMode)
            {
                DeselectTile();
                mouseButtonDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                mouseButtonUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D[] hit = Physics2D.BoxCastAll((mouseButtonDownPosition + mouseButtonUpPosition)/2,
                    BoxSizeAbsolute(mouseButtonDownPosition - mouseButtonUpPosition),
                    0, Vector2.zero);

                tileArray = new Tile[hit.Length];
                //Debug.Log(hit.Length);
                //Debug.Log("mouseButtonDownPosition : " + mouseButtonDownPosition);
                //Debug.Log("mouseButtonUpPosition : " + mouseButtonUpPosition);
                for (int i = 0; i < hit.Length; i++)
                {
                    tileArray[i] = hit[i].collider.GetComponent<Tile>();
                }
                SelectTile();
            }
            else if (Input.GetMouseButtonUp(0) && !isObjectSelectMode)
            {
                DeselectTile();
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            DeselectTile();
        }
        else if (Input.GetKey(KeyCode.Delete))
        {
            SelectedTileChaneType(type.Blank);
            DeleteTiles();
            DeselectTile();
        }
    }

    public void CreateTile(int width, int height)
    {
        GameObject _nomarlTile = Resources.Load<GameObject>("Prefabs/nomarl_tile");

        if(tiles.childCount > 0)
        {
            for(int i = 0; i < tiles.childCount; i++)
            {
                Destroy(tiles.GetChild(i).gameObject);
            }
        }

        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(_nomarlTile, new Vector3(2*i, -2*j, 0), Quaternion.identity, tiles);
            }
        }
        
    }

    public void ChangeTileType()
    {

    }

    public void SettingSecurity()
    {
        isSetSecurity = true;
    }

    public void LoadCreateTile(TileData[] tileDatas)
    {
        GameObject _nomarlTile = Resources.Load<GameObject>("Prefabs/nomarl_tile");
        // 이미 생성된 타일들을 삭제
        if (tiles.childCount > 0) 
        {
            for(int i = 0; i < tiles.childCount; i++)
            {
                Destroy(tiles.GetChild(i).gameObject);
            }
        }

        // json 데이터 기반으로 타일 생성
        for (int i = 0; i < tileDatas.Length; i++)
        {
            GameObject _tempObject = Instantiate(_nomarlTile, tileDatas[i].position, Quaternion.identity, tiles);
            Tile _tempTile = _tempObject.GetComponent<Tile>();
            _tempTile.SetType((type)tileDatas[i].tileType);
            _tempTile.SetIsSecurity(tileDatas[i].isSecurity);
            _tempTile.SetState(tileDatas[i].state);
        }
    }

    public void SelectTileType(type tileType)
    {
        selectedType = tileType;
    }

    //버튼으로 호출하기위해서 오버로딩 함수 만듬
    public void SelectTileType(int tileNum)
    {
        selectedType = (type)tileNum;
    }

    public void SelectTile()
    {
        if(tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                if(tileArray[i] != null)
                {

                    //11.10 타입 설정 추가
                    if (!isObjectSelectMode)
                    {
                        tileArray[i].SetType(selectedType);
                    }
                    tileArray[i].Select(true, isObjectSelectMode);
                }
            }
            isSetTile = true;
        }
    }

    public void SetObjectSelectMode(bool value)
    {
        isObjectSelectMode = value;
        DeselectTile(true);
    }

    public Sprite GetNormalTileSprite()
    {
        return normalTileSprite;
    }

    public Sprite GetWallTileSprite()
    {
        return wallTileSprite;
    }

    public Sprite GetDoorTileSprite()
    {
        return doorTileSprite;
    }

    public Sprite GetWindowTileSprite()
    {
        return windowTileSprite;
    }

    public Sprite GetSecuritySprite()
    {
        return securitySprite;
    }
    private void DeleteTiles() // 선택한 타일을 지우는 함수
    {
        if (tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                if (tileArray[i] != null)
                {
                    if(tileArray[i].transform.childCount > 0)
                    {
                        for(int p = 0; p < tileArray[i].transform.childCount; p++)
                        {
                            tileArray[i].SetIsSecurity(false);
                            Destroy(tileArray[i].transform.GetChild(p).gameObject);
                        }
                            
                    }
                }

            }
        }
    }

    private void DeselectTile()
    {
        DeselectTile(isObjectSelectMode);
    }
    private void DeselectTile(bool isObjectSelectMode)
    {
        if (tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                if (tileArray[i] != null)
                {
                    tileArray[i].Select(false, isObjectSelectMode);
                    //이걸 해야지 타일의 개체선택모드가 풀림
                    tileArray[i].SetIsObjectSelectModeFalse();
                }
            }
            tileArray = null;
        }
    }


    private void SelectedTileChaneType(type tileType)
    {
        if(tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                tileArray[i].SetType(tileType);
            }
        }
    }

    private Vector3 BoxSizeAbsolute(Vector3 value)
    {
        float _resultX, _resultY;
        _resultX = value.x;
        _resultY = value.y;

        if (_resultX < 0) _resultX *= -1;
        else if (_resultX == 0) _resultX = 0.01f;
        if (_resultY < 0) _resultY *= -1;
        else if (_resultY == 0) _resultY = 0.01f;


        return new Vector2(_resultX, _resultY);
    }

    private float CalculateBoxDegree(Vector3 value)
    {
        float _result = 0;

        if (value.x >= 0 && value.y >= 0)
        {
            _result = 0;
        }
        else if (value.x <= 0 && value.y >= 0)
        {
            _result = 0;
        }
        else if (value.x >= 0 && value.y <= 0)
        {
            _result = 0;
        }
        else if (value.x <= 0 && value.y <= 0)
        {
            _result = 0;
        }

        return _result;
    }

    public bool GetIsSetTileAndSecurity()
    {
        if (isSetTile && isSetSecurity) return true;
        return false;
    }
    public int GetSecurityIndex()
    {
        return securityIndex;
    }
    public void IncreaseSecurityIndex()
    {
        securityIndex++;
    }
    public void ResetSecurityIndex()
    {
        securityIndex = 0;
    }
}
