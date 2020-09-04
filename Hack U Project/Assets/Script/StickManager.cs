using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using GoogleARCore.CrossPlatform;
using UnityEngine.UI;

public class StickManager : MonoBehaviourPunCallbacks
{
    GameObject camera;
    private bool start;
    XPAnchor anchor;
    Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            var posi = camera.transform.position;
            posi.z += 0.1f;
            transform.position = posi;
        }
        else
        {

        }
    }

    public void SetCamera(GameObject cam,XPAnchor anc)
    {

        camera = cam;     
    }
}
