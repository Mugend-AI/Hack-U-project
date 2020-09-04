using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.CrossPlatform;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private bool setAnchor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void SetAnchorID(string id)
    {
        if (!setAnchor)
        {
            GameObject.FindWithTag("InfoObj").GetComponent<InfoManager>().SetCloudID(id);
            setAnchor = true;
        }
    }

}
