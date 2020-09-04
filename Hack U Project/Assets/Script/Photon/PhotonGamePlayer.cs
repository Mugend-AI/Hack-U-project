using Photon.Pun;
using UnityEngine;

public class PhotonGamePlayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine)
        {
            var dx = 0.1f * Input.GetAxis("Horizontal");
            var dy = 0.1f * Input.GetAxis("Vertical");
            if (Input.GetAxis("Horizontal") >= 1 || Input.GetAxis("Horizontal") <= -1) Debug.Log("a");
            transform.Translate(dx, dy, 0f);
        }
    }
}
