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

        [SerializeField]
        private Animator m_Animator = null;

        private void Start()
        {
            if (m_Animator == null)
                m_Animator = m_Visuals.GetComponentInChildren<Animator>();
        }

        void OnCollisionEnter(Collision collision)
        {
            
            var direction = m_Rigidbody.velocity.normalized;
            var magnitude = m_Rigidbody.velocity.magnitude;
            m_Rigidbody.velocity = Vector3.zero;
            var force = Vector3.zero;
            if (collision.gameObject.CompareTag("Board"))
                force = (Vector3.Reflect(direction, collision.contacts[0].normal)
                    * magnitude / 2 * m_CharacterSettings.BouncePower) - Physics.gravity;
            else
                force = GameManager.Instance.Board.transform.up * magnitude / 2 * m_CharacterSettings.BouncePower -Physics.gravity;

            Bounce(force);           
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

        public void Freeze()
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.gameObject.SetActive(false);
        }

        public void Unfreeze()
        {
            m_Rigidbody.isKinematic = false;
            m_Rigidbody.gameObject.SetActive(true);
        }

        private void Bounce(Vector3 force)
        {
            StartCoroutine(BounceRoutine());
            if (force.magnitude > 10)
                force = force.normalized * 10;
            else if (force.magnitude < 2)
                force = force.normalized * 2;

            m_Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private IEnumerator BounceRoutine()
        {
            m_Animator.SetBool("Bounce", true);
            yield return new WaitForSeconds(1f);
            m_Animator.SetBool("Bounce", false);
        }
    }
}