using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateTile(10, 10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateTile(int width, int height)
    {
        GameObject _nomarlTile = Resources.Load<GameObject>("Prefabs/nomarl_tile");

        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(_nomarlTile, new Vector3(2*i, 2*j, 0), Quaternion.identity);
            }
        }
        
    }
}
