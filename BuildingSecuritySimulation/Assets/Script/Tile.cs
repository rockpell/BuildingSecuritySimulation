using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private type tileType;
    private bool tileState;
    private bool isSecurity; // 보안시스템 설치 여부
    private bool isSelect;

    private Vector3 tilePosition;
    private GameObject additionObject;
    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public new type GetType()
    {
        return tileType;
    }

    public void SetType(type name)
    {
        tileType = name;
        //11.10 타입 바꾸는거 추가 나중에 파일이름에 따라 바꿔야함
        if (name == type.Blank) spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nomarl_tile");
        else if (name == type.Door)
        {
            AddImageObject(Resources.Load<Sprite>("Sprites/door"));
        }
        else if (name == type.Window)
        {
            AddImageObject(Resources.Load<Sprite>("Sprites/window"));
        }
        else spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/test" + name.ToString());

    }

    public bool IsDetect()
    {
        return false;
    }

    public bool GetState()
    {
        return tileState;
    }

    public void SetState(bool state)
    {

    }

    public bool GetIsSecurity()
    {
        return isSecurity;
    }

    public void SetIsSecurity(bool isSecurity)
    {

    }

    public void SetPosition(Vector3 pos)
    {

    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void Interact()
    {

    }

    public void Select(bool select)
    {
        if (select)
        {
            spriteRenderer.color = Color.red;
            isSelect = true;
        }
        else
        {
            spriteRenderer.color = Color.white;
            isSelect = false;
        }
    }

    private void OnMouseOver()
    {
        if(!isSelect)
            spriteRenderer.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (!isSelect)
            spriteRenderer.color = Color.white;
    }
    
    private void AddImageObject(Sprite sprite)
    {
        GameObject _gameObject;

        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            _gameObject = new GameObject();
            _gameObject.transform.SetParent(this.transform, false);
            _gameObject.AddComponent<SpriteRenderer>();
            _gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            _gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
