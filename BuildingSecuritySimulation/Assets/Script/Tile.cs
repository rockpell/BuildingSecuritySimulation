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

	// Use this for initialization
	void Start () {
		
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

    }
}
