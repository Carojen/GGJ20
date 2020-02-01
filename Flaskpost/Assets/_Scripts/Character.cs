﻿using System.Collections;
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
        
        void OnCollisionEnter(Collision collision)
        {
            var direction = m_Rigidbody.velocity.normalized;
            var magnitude = m_Rigidbody.velocity.magnitude;

            var force = (Vector3.Reflect(direction, collision.contacts[0].normal)
                * magnitude/2 * m_CharacterSettings.BouncePower) - Physics.gravity;
            //Debug.LogFormat("Add force {0}.", force);
            m_Rigidbody.AddForce(force, ForceMode.Impulse);
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