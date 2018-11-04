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
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public type GetType()
    {
        return tileType;
    }

    public void SetType(type name)
    {

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
