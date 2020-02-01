using System.Collections;
using UnityEngine;

namespace Flaskpost
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterSettings m_CharacterSettings = null;

        [SerializeField]
        private Rigidbody m_Rigidbody = null;

        [SerializeField]
        private GameObject m_Visuals = null;
        
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
            
            m_Rigidbody.AddForce((Vector3.Reflect(direction, collision.contacts[0].normal) 
                * magnitude * m_CharacterSettings.BouncePower) - Physics.gravity, ForceMode.Impulse);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Respawn") && gameObject.activeSelf)
            {                
                var targetPos = FindObjectOfType<GameBoard>().GetComponentInChildren<Collider>().ClosestPointOnBounds(transform.position);
                targetPos -= m_Rigidbody.velocity;
                targetPos.y = 3f;

                StartCoroutine(Respawn(targetPos));                
            }                
        }

        private IEnumerator Respawn(Vector3 position)
        {
            Debug.LogFormat("Out of bounds, respawn at {0}.", position);

            m_Rigidbody.isKinematic = true;
            m_Visuals?.SetActive(false);
            transform.position = position;
            m_Rigidbody.velocity = Vector3.zero;
            yield return new WaitForSeconds(1f);

            m_Visuals?.SetActive(true);
            m_Rigidbody.isKinematic = false;
        }
    }
}