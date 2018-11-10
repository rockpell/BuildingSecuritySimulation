﻿using System.Collections;
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

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Use this for initialization
    void Start () {
        //기본 안해주면 삭제됨
        selectedType = type.Blank;
    }
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DeselectTile();
                mouseButtonDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouseButtonUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                BoxSizeAbsolute(mouseButtonDownPosition - mouseButtonUpPosition);

                RaycastHit2D[] hit = Physics2D.BoxCastAll((mouseButtonDownPosition + mouseButtonUpPosition)/2,
                    BoxSizeAbsolute(mouseButtonDownPosition - mouseButtonUpPosition),
                    0, Vector2.zero);

                tileArray = new Tile[hit.Length];

                for (int i = 0; i < hit.Length; i++)
                {
                    tileArray[i] = hit[i].collider.GetComponent<Tile>();
                }
                SelectTile();
            }
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

    }

    public void LoadCreateTile(string loadData)
    {

    }

    public void SelectTileType(type tileType)
    {
        selectedType = tileType;
    }

    //버튼으로 호출하기위해서 오버로딩 함수 만듬
    public void SelectTileType(int tileNum)
    {
        switch (tileNum)
        {
            case 0:
                selectedType = type.Blank;
                break;
            case 1:
                selectedType = type.Wall;
                break;
            case 2:
                selectedType = type.Door;
                break;
            case 3:
                selectedType = type.Window;
                break;
        }
    }

    public void SelectTile()
    {
        if(tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                if(tileArray[i] != null)
                {
                    tileArray[i].Select();
                    //11.10 타입 설정 추가
                    tileArray[i].SetType(selectedType);
                }

            }
        }
    }

    private void DeselectTile()
    {
        if (tileArray != null)
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                if (tileArray[i] != null)
                {
                    tileArray[i].Select();
                }

            }
            tileArray = null;
        }
        
    }

    private Vector3 BoxSizeAbsolute(Vector3 value)
    {
        float _resultX, _resultY;
        _resultX = value.x;
        _resultY = value.y;

        if (_resultX < 0) _resultX *= -1;
        if (_resultY < 0) _resultY *= -1;

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
}
