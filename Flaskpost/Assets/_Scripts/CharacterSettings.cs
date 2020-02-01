using UnityEngine;

namespace Flaskpost
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "ScriptableObjects/CharacterSettings", order = 1)]
    public class CharacterSettings : ScriptableObject
    {
        //[SerializeField]
        [Range(0f, 10f)]
        private float m_BouncePower = 1f;
        public float BouncePower { get { return m_BouncePower; } }

        [SerializeField]
        [Range(0f, 5f)]
        private float m_Gravity = 1f;
        
        public void OnValidate()
        {            
            var gravity = Physics.gravity;
            gravity.y = -m_Gravity * 9f;
            Physics.gravity = gravity;
        }
    }
}