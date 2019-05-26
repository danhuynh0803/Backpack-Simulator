using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViablePath : MonoBehaviour
{
    public bool isPathGood;
    Image thisImage;
    // Start is called before the first frame update
    void Start()
    {
        isPathGood = false;
        thisImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPathGood)
        {
            thisImage.color = Color.red;
        }
        if (isPathGood)
        {
            thisImage.color = Color.black;
        }
    }
}
