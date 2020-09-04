using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class InfoManager : MonoBehaviour
{
    PhotonView photonView;
    private string cloudID ="no";
    private bool exAnchor;
    private bool connected;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CloudID
    {
        get { return cloudID; }
    }


    public void SetCloudID(string id)
    {

        photonView.RPC("SetID", RpcTarget.All, id);
    }

    [PunRPC]
    void SetID(string id)
    {
        exAnchor = true;
        cloudID = id;
    }

    public bool ExAnchor
    {
        get { return exAnchor; }
    }

    public void ConnectFinish()
    {
        photonView.RPC("ConFin", RpcTarget.All);
    }

    [PunRPC]
    void ConFin()
    {
        connected = true;
    }

    public bool Connected
    {
        get { return connected; }
    }
}
