using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld : MonoBehaviour {

    [Header("Enemy Encounters")]
    public GameObject mountainButton;
    public GameObject forestButton;
    public GameObject castleButton;
    public GameObject towerButton;
    [Header("Nonenemy Encounters")]
    public GameObject treasureButton;
    public GameObject treasure2Button;
    public GameObject merchantButton;
    public GameObject merchant2Button;


    // Use this for initialization
    void Start() {

        float rate2 = Random.Range(0.0f, 2.0f);
        float rate3 = Random.Range(0.0f, 2.0f);
        float rate5 = Random.Range(0.0f, 2.0f);

        if (rate2 > 1.0f)
        {
            mountainButton.SetActive(true);
        }
        if (rate2 < 1.0f)
        {
            forestButton.SetActive(true);
        }
        if (rate3 > 1.0f)
        {
            castleButton.SetActive(true);
        }
        if (rate3 < 1.0f)
        {
            towerButton.SetActive(true);
        }
        if (rate5 > 1.0f)
        {
            treasureButton.SetActive(true);
        }
        if (rate5 < 1.0f)
        {
            treasure2Button.SetActive(true);
        }
       /* else if (rate5 > 1.0f)
        {
            merchantButton.SetActive(true);
        }
        else if (rate5 > 0.0f)
        {
            merchant2Button.SetActive(true);
        }*/
    }
    
    // Update is called once per frame
	void Update () {
		
	}
}
