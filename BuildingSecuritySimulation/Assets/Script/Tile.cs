using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private type tileType;
    private bool tileState;
    private bool isSecurity;
    private bool isSelect;

    private Vector3 tilePosition;
    private GameObject additionObject;
    private new SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
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
        //11.10 타입 바꾸는거 추가 나중에 파일이름에 따라 바꿔야함
        if(name == type.Blank) renderer.sprite = Resources.Load<Sprite>("Sprites/nomarl_tile");
        else renderer.sprite = Resources.Load<Sprite>("Sprites/test" + name.ToString());
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

    public void Select()
    {
        if (isSelect)
        {
            renderer.color = Color.white;
            isSelect = false;
        }
        else
        {
            renderer.color = Color.red;
            isSelect = true;
        }
    }

    private void OnMouseOver()
    {
        if(!isSelect)
            renderer.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (!isSelect)
            renderer.color = Color.white;
    }
    
}
