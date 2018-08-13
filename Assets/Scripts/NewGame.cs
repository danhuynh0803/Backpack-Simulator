using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {

    public GameObject newGame;

    float timer;

    // Use this for initialization
    void Start () {
        timer = 24f;	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            newGame.SetActive(true);
        }
    }
}
