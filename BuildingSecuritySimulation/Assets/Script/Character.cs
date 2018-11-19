using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool authority;
    public float speed = 5.0f;
    private SpriteRenderer sprite;
    private List<Tile> tileList;
    private AudioSource[] audio = new AudioSource[5];
    private AudioClip[] audioClip = new AudioClip[5];
    private Rigidbody2D rigidbody;
    Vector3 movement;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        for(int i=0; i<5; i++)
        {
            audio[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
            audio[i].Stop();
        }
        audioClip[0] = Resources.Load("audio/siren", typeof(AudioClip)) as AudioClip;
        audioClip[1] = Resources.Load("audio/opendoor", typeof(AudioClip)) as AudioClip;
        audioClip[2] = Resources.Load("audio/closedoor", typeof(AudioClip)) as AudioClip;
        audioClip[3] = Resources.Load("audio/openwindow", typeof(AudioClip)) as AudioClip;
        audioClip[4] = Resources.Load("audio/closewindow", typeof(AudioClip)) as AudioClip;
        for (int j = 0; j < 5; j++)
        {
            audio[j].clip = audioClip[j];
        }
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
        if (h == 0 || v == 0)
            return;
        Quaternion newRotation = Quaternion.LookRotation(movement);
//        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation,newRotation,speed*Time.deltaTime);
      
    }
    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            audio[1].Play();
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
