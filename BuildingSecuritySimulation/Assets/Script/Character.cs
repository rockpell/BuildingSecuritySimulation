using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    private float speed;
    
	// Use this for initialization
	void Start () {
        GetAuthority();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (GetAuthority() == true)
        {
            Move();
            Interaction();
        }
    }

    public bool GetAuthority()
    {
        return authority;
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down);
            }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right);
        }
    }
    public void Interaction()
    {

    }
    public void AuthoritySelect(bool authority)
    {

    }
}
