using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    public float speed = 5.0f;
    private SpriteRenderer sprite;
    private List<Tile> tileList;
    private Rigidbody2D rigidbody;
    Vector3 movement;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h,v);
        Vector3 camPos = transform.position;
        camPos.z = -1;
        Camera.main.transform.position = camPos;
    }

    public bool GetAuthority()
    {
        return authority;
    }
    public void Move(float h, float v)
    {
        movement.Set(h, v, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);
        Quaternion turn = Quaternion.identity;
        if (h > 0)
        {
            turn.eulerAngles = new Vector3(0, 0, 90);
            transform.rotation = turn;
        }
        if (h < 0)
        {
            turn.eulerAngles = new Vector3(0, 0, 270);
            transform.rotation = turn;
        }
        if (v > 0)
        {
            turn.eulerAngles = new Vector3(0, 0, 180);
            transform.rotation = turn;
        }
        if (v < 0)
        {
            turn.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = turn;
        }
    }
    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
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
        if (!tileList.Contains(tile))
        {
            tileList.Add(tile);
        }
    }
    public void DeleteList(Tile tile)
    {
        if (tileList.Contains(tile))
        {
            tileList.Remove(tile);
        }
    }
}
