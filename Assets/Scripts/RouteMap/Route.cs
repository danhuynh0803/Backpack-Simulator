using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Route : MonoBehaviour
{
    public bool isPathGood;

    // Start is called before the first frame update
    void Start()
    {
        isPathGood = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Check()
    {
        isPathGood = true;
    }
}
