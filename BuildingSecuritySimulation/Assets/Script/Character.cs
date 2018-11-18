using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    public float speed = 0.2f;
    private SpriteRenderer sprite;
    private List<Tile> tileList;
    // Use this for initialization
    void Start () {
        tileList = new List<Tile>();
         Camera.main.orthographicSize = 7;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        gameObject.tag = "Player";
        if (authority)
        {
            sprite.sprite = Resources.Load<Sprite>("Sprites/Authority");
            sprite.sortingOrder = 3;
        }
        else
        {
            sprite.sprite = Resources.Load<Sprite>("Sprites/NoAuthority");
            sprite.sortingOrder = 3;
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
        camPos.z = -1;
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
            if(tileList.Count >0)
            {
                for (int i = 0; i < tileList.Count; i++)
                {
                    tileList[i].Interact();
                }
            }
        }
    }
    public void AuthoritySelect(bool isAuthority)
    {

        authority = isAuthority;
    }
    public void AddTileList(Tile tile)
    {
        Debug.Log("추가");
        if (!tileList.Contains(tile))
        {
            Debug.Log("추가됨");
            tileList.Add(tile);
        }
        Debug.Log(tileList.Count);
    }
    public void DeleteList(Tile tile)
    {
        Debug.Log("삭제");
        if (tileList.Contains(tile))
        {
            Debug.Log("삭제됨");
            tileList.Remove(tile);
        }
        Debug.Log(tileList.Count);
    }
}
