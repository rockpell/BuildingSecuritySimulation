using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    public float speed = 0.2f;
    private SpriteRenderer sprite;
    // Use this for initialization
    void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        if (authority)
        {
            sprite.sprite = Resources.Load<Sprite>("Sprites/Authority");
        }
        else
        {
            sprite.sprite = Resources.Load<Sprite>("Sprites/NoAuthority");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Interaction();
    }

    void FixedUpdate()
    {
        Move();
        Vector3 camPos = transform.position;
        camPos.z = -5;
        Camera.main.transform.position = camPos;
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
    public void AuthoritySelect(bool isAuthority)
    {
        authority = isAuthority;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Window" || collision.transform.tag == "Window")
        {

        }
    }
}
