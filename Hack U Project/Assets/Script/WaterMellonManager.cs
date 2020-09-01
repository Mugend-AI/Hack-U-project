using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMellonManager : MonoBehaviour
{
    [SerializeField] GameObject hiteffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stick") //StickSタグの付いたゲームオブジェクトと衝突したか判別
        {
            Instantiate(hiteffect, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            //Destroy(this.gameObject); //衝突したゲームオブジェクトを削除
            this.GetComponent<SS>().ScreenShot();
        }
    }
}
