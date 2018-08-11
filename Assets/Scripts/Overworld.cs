using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld : MonoBehaviour {

    [Header("Enemy Encounters")]
    public GameObject frostButton;
    public GameObject seaButton;
    public GameObject sandButton;
    public GameObject mountainButton;
    public GameObject grassButton;
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

        float rate = Random.Range(0.0f, 2.0f);
        float rate2 = Random.Range(0.0f, 2.0f);
        float rate3 = Random.Range(0.0f, 2.0f);
        float rate4 = Random.Range(0.0f, 2.0f);
        float rate5 = Random.Range(0.0f, 4.0f);

        if (rate > 1.0f)
        {
            frostButton.SetActive(true);
        }
        if (rate < 1.0f)
        {
            seaButton.SetActive(true);
        }
        if (rate2 > 1.0f)
        {
            grassButton.SetActive(true);
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
        if (rate4 > 1.0f)
        {
            sandButton.SetActive(true);
        }
        if (rate4 < 1.0f)
        {
            mountainButton.SetActive(true);
        }
        if (rate5 > 3.0f)
        {
            treasureButton.SetActive(true);
        }
        else if (rate5 > 2.0f)
        {
            treasure2Button.SetActive(true);
        }
        else if (rate5 > 1.0f)
        {
            merchantButton.SetActive(true);
        }
        else if (rate5 > 0.0f)
        {
            merchant2Button.SetActive(true);
        }
    }
    
    // Update is called once per frame
	void Update () {
		
	}
}
