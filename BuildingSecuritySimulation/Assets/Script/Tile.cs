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
    private bool isObjectSelectMode;                    //개체선택모드인지
    private int securityNum = 0;
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
            GetComponent<SpriteRenderer>().sprite = BuildManager.instance.GetNormalTileSprite();
            tileType = name;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            if (name == type.Door)
            {
                AddImageObject(BuildManager.instance.GetDoorTileSprite(), 1);
                tileType = name;
            }
            else if (name == type.Window)
            {
                AddImageObject(BuildManager.instance.GetWindowTileSprite(), 1);
                tileType = name;
            }
            else if (name == type.Wall)
            {
                GetComponent<SpriteRenderer>().sprite = BuildManager.instance.GetWallTileSprite();
                tileType = name;
            }
            else
            {
                if (tileType == type.Door || tileType == type.Window)
                {
                    AddSecurity(BuildManager.instance.GetSecuritySprite());
                }
                else
                {
                    UIManager.instance.StartCoroutine(UIManager.instance.ShowErrorMessage("벽에는 보안시스템을 설치 할 수 없습니다."));
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
        this.isSecurity = isSecurity;
        if (isSecurity)
        {
            AddSecurity(BuildManager.instance.GetSecuritySprite());
        }
    }
    //개필요없음
    public void SetPosition(Vector3 pos)
    {

    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void Interact()
    {
        if (GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            if (securityNum != 0)
                UIManager.instance.ChangeLogMessage(securityNum);
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if(securityNum != 0)
                UIManager.instance.ChangeLogMessage(securityNum);
        }
    }

    public void Select(bool select, bool isObjectSelect)
    {
        isObjectSelectMode = isObjectSelect;
        if (select)
        {
            if (isObjectSelectMode)
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
            if (isObjectSelectMode)
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
    
    private void AddImageObject(Sprite sprite, int sortingOrder)
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
        }
    }
    private void AddSecurity(Sprite sprite)
    {
        if (this.transform.childCount < 2)
        {
            GameObject _gameObject;
            _gameObject = new GameObject();
            _gameObject.transform.SetParent(this.transform, false);
            _gameObject.AddComponent<SpriteRenderer>();
            _gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            _gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            _gameObject.transform.Translate(0, 0.5f, 0);
            GameObject meshObject = new GameObject();
            MeshRenderer meshtmp = meshObject.AddComponent<MeshRenderer>();
            TextMesh textMeshtmp = meshObject.AddComponent<TextMesh>();
            meshObject.transform.parent = _gameObject.transform;
            meshObject.transform.localPosition = new Vector3(0, -0.5f, 0);
            textMeshtmp.characterSize = 0.4f;
            textMeshtmp.anchor = TextAnchor.UpperCenter;
            textMeshtmp.alignment = TextAlignment.Center;
            textMeshtmp.color = Color.black;
            securityNum = BuildManager.instance.GetSecurityIndex();
            textMeshtmp.text = securityNum.ToString();
            meshtmp.sortingOrder = 3;
            BuildManager.instance.IncreaseSecurityIndex();
            BuildManager.instance.SettingSecurity();
        }
    }

    public void SetIsObjectSelectModeFalse()
    {
        isObjectSelectMode = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && tileType == type.Window || tileType == type.Door)
        {
            UIManager.instance.ChangeInteractionText(true);
            Character playerTmp = collision.GetComponentInParent<Character>();
            playerTmp.AddTileList(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && tileType == type.Window || tileType == type.Door)
        {
            UIManager.instance.ChangeInteractionText(false);
            Character playerTmp = collision.GetComponentInParent<Character>();
            playerTmp.DeleteList(this);
        }
    }
}
