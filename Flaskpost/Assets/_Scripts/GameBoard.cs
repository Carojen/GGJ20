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

            if (Input.GetKey(m_Input.Left))
            {
                if (m_BoardTransform.up.x > -0.4f)
                    m_BoardTransform.Rotate(m_BoardTransform.forward, m_Input.TiltPower);
            }
            else if (Input.GetKey(m_Input.Right))
            {
                if (m_BoardTransform.up.x < 0.4f)
                    m_BoardTransform.Rotate(m_BoardTransform.forward, -m_Input.TiltPower);
            }
            else if (Mathf.Abs(m_BoardTransform.up.x) > 0.01f)
                m_BoardTransform.Rotate(m_BoardTransform.forward, m_BoardTransform.up.x > 0 ? m_Input.Elasticity : -m_Input.Elasticity);

            if (Input.GetKey(m_Input.Back))
            {
                if (m_BoardTransform.up.z > -0.4f)
                    m_BoardTransform.Rotate(m_BoardTransform.right, -m_Input.TiltPower);
            }
            else if (Input.GetKey(m_Input.Forward))
            {
                if (m_BoardTransform.up.z < 0.4f)
                    m_BoardTransform.Rotate(m_BoardTransform.right, m_Input.TiltPower);
            }
            else if (Mathf.Abs(m_BoardTransform.up.z) > 0.01f)
                m_BoardTransform.Rotate(m_BoardTransform.right, m_BoardTransform.up.z > 0 ? -m_Input.Elasticity : m_Input.Elasticity);
        }
    }
}