using UnityEngine;

namespace Flaskpost
{
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

            if (Input.GetKey(m_Input.Left) && m_BoardTransform.up.x < 0.4f)
            {
                m_BoardTransform.Rotate(m_BoardTransform.forward, -m_Input.TiltPower);
                Debug.LogFormat("Up: {0}.", m_BoardTransform.up);
            }                
            else if (Input.GetKey(m_Input.Right) && m_BoardTransform.up.x > -0.4f)
                m_BoardTransform.Rotate(m_BoardTransform.forward, m_Input.TiltPower);
            //else if(rotation not zero)
            //    m_BoardTransform.Rotate(m_BoardTransform.forward, m_Input.Elasticity * m_BoardTransform.rot);

            if (Input.GetKey(m_Input.Back) && m_BoardTransform.up.z > -0.4f)
                m_BoardTransform.Rotate(m_BoardTransform.right, -m_Input.TiltPower);
            else if (Input.GetKey(m_Input.Forward) && m_BoardTransform.up.z < 0.4f)
                m_BoardTransform.Rotate(m_BoardTransform.right, m_Input.TiltPower);

        }

    }
}