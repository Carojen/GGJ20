﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private InputSettings m_Input = null;

    [SerializeField]
    private Transform m_BoardTransform = null;

    private void Update()
    {
        if (m_BoardTransform == null)
            return;

        if (Input.GetKey(m_Input.Left))
            m_BoardTransform.Rotate(m_BoardTransform.forward, -m_Input.TiltPower);
        else if (Input.GetKey(m_Input.Right))
            m_BoardTransform.Rotate(m_BoardTransform.forward, m_Input.TiltPower);
        //else if(rotation not zero)
        //    m_BoardTransform.Rotate(m_BoardTransform.forward, m_Input.Elasticity * m_BoardTransform.rot);


        if (Input.GetKey(m_Input.Back))
            m_BoardTransform.Rotate(m_BoardTransform.right, -m_Input.TiltPower);
        else if (Input.GetKey(m_Input.Forward))
            m_BoardTransform.Rotate(m_BoardTransform.right, m_Input.TiltPower);

    }

}
