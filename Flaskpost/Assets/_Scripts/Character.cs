using UnityEngine;

namespace Flaskpost
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterSettings m_CharacterSettings = null;

        [SerializeField]
        private Rigidbody m_Rigidbody = null;


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                m_Rigidbody.AddForce(Vector3.Reflect(-transform.up, transform.up * m_CharacterSettings.BouncePower * 0.5f),
                               ForceMode.Impulse);
        }

        void OnCollisionEnter(Collision collision)
        {            
            var direction = m_Rigidbody.velocity.normalized;
            var magnitude = m_Rigidbody.velocity.magnitude;
            Debug.Log("Collision " + (magnitude * m_CharacterSettings.BouncePower).ToString());
            m_Rigidbody.AddForce((Vector3.Reflect(direction, collision.contacts[0].normal) 
                * magnitude * m_CharacterSettings.BouncePower) - Physics.gravity, ForceMode.Impulse);
        }
    }
}