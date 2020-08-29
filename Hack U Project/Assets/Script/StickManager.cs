using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StickManager : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
    Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonR;
    private GameObject stickPosition;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        stickPosition = GameObject.FindWithTag("StickPosition");
        m_joyconR = JoyconManager.Instance.j.Find(c => !c.isLeft);
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

        m_pressedButtonR = null;

        if (m_joyconR == null)
        {
            text.text = "接続なし";
            return;
        }
        foreach (var button in m_buttons)
        {
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            m_joyconR.SetRumble(160, 320, 0.6f, 200);
        }
        var vec = m_joyconR.GetVector().eulerAngles;
        var rb = gameObject.transform.localEulerAngles;
        //rb.x += -gyro[1] * move;
        //rb.y += -gyro[0] * move; 
        rb.z = -vec.y + 180;
        gameObject.transform.localEulerAngles = rb;
        if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            gameObject.transform.localEulerAngles = Vector3.zero;
        }

    }
}
