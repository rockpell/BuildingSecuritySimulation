using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    public float speed = 0.2f;

    // Use this for initialization
    void Start () {
        GetAuthority();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Move();
        Interaction();
    }

    public bool GetAuthority()
    {
        return authority;
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up*speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed);
            }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed);
        }
    }
    public void Interaction()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("문열고닫고중");
        }
    }
    public void AuthoritySelect(bool authority)
    {

    }
}
