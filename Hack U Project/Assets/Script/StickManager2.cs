using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager2 : MonoBehaviour
{
    private readonly float moveDistance = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 firstPosi = Vector3.zero;
        firstPosi.x = Random.Range(-3.0f, 3.0f);
        firstPosi.z = Random.Range(-3.0f, 3.0f);
        transform.position = firstPosi;
        */       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 mv = Vector3.zero;
        if (Input.GetAxis("HorizontalJoycon") >= 1)
        {
            mv.x = moveDistance;
        }
        if (Input.GetAxis("HorizontalJoycon") <= -1)
        {
            mv.x = -moveDistance;
        }
        if(Input.GetAxis("VerticalJoycon") >= 1)
        {
            mv.z = -moveDistance;
        }
        if(Input.GetAxis("VerticalJoycon") <= -1)
        {
            mv.z = moveDistance;
        }
        transform.Translate(mv);
    }
}
