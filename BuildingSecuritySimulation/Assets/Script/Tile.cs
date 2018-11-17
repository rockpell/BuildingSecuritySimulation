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
    private SpriteRenderer childeSprite;                //문,창문 색깔바꾸기위한 변수
    private UIManager uIManager;
    private bool isObjectSelectMode;                    //개체선택모드인지
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
        
        if (name == type.Blank)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nomarl_tile");
            tileType = name;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            if (name == type.Door)
            {
                AddImageObject(Resources.Load<Sprite>("Sprites/door"), 1, true, "Door");
                tileType = name;
            }
            else if (name == type.Window)
            {
                AddImageObject(Resources.Load<Sprite>("Sprites/window"), 1, true, "Window");
                tileType = name;
            }
            else if (name == type.Wall)
            {
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/wall");
                tileType = name;
            }
            else
            {
                if (tileType == type.Door || tileType == type.Window)
                {
                    AddImageObject(Resources.Load<Sprite>("Sprites/siren"), 2, false, "Sequrity");
                }
                else
                {
                    uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                    uIManager.StartCoroutine(uIManager.ShowErrorMessage("벽에는 보안시스템을 설치 할 수 없습니다."));
                }
            }
        }
        
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

    public void Select(bool select, bool isObjectSelect)
    {
        isObjectSelectMode = isObjectSelect;
        if (select)
        {
            if (isObjectSelect)
            {
                //문,창문이 있을때
                if (transform.childCount > 0)
                {
                    childeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
                    childeSprite.color = Color.gray;
                }
                spriteRenderer.color = Color.gray;
            }
            else
            {
                spriteRenderer.color = Color.red;
                isSelect = true;
            }
        }
        else
        {
            if (isObjectSelect)
            {
                if (transform.childCount > 0)
                {
                    childeSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
                    childeSprite.color = Color.white;
                }
            }
            spriteRenderer.color = Color.white;
            isSelect = false;
        }
    }

    private void OnMouseOver()
    {
        if (!isSelect)
        {
            if (!isObjectSelectMode) spriteRenderer.color = Color.green;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelect)
        {
            if(!isObjectSelectMode) spriteRenderer.color = Color.white;
        }
    }
    
    private void AddImageObject(Sprite sprite, int sortingOrder, bool makeCollider, string tag)
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
            _gameObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            _gameObject.tag = tag;
            //if (makeCollider) _gameObject.AddComponent<BoxCollider2D>();
        }
    }
    public void SetIsObjectSelectModeFalse()
    {
        isObjectSelectMode = false;
    }
}
