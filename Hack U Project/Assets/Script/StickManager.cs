using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    private GameObject stickPosition;
    // Start is called before the first frame update
    void Start()
    {
        stickPosition = GameObject.FindWithTag("StickPosition");
    }

    // Update is called once per frame
    void Update()
    {
        if(stickPosition == null)
        {
            stickPosition = GameObject.FindWithTag("StickPosition");
        }
        else
        {
            transform.position = stickPosition.transform.position;
        }
    }
}
