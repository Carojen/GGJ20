using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flaskpost
{
    public class Target : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Target = null;

        [SerializeField]
        private GameObject m_Effects = null;

        public bool Repaired { get { return !m_Target.activeSelf; } }
               
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            m_Target?.SetActive(false);
            m_Effects?.SetActive(true);
        }
    }
}