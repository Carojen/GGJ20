using UnityEngine;

namespace Flaskpost
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "ScriptableObjects/InputSettings", order = 1)]
    public class InputSettings : ScriptableObject
    {
        [SerializeField]
        private KeyCode m_Left = KeyCode.A;
        [SerializeField]
        private KeyCode m_Right = KeyCode.D;
        [SerializeField]
        private KeyCode m_Forward = KeyCode.W;
        [SerializeField]
        private KeyCode m_Back = KeyCode.S;

        public KeyCode Left { get { return m_Left; } }
        public KeyCode Right { get { return m_Right; } }
        public KeyCode Forward { get { return m_Forward; } }
        public KeyCode Back { get { return m_Back; } }

        [Tooltip("The power applied to tilt the board when input is given.")]
        [SerializeField]
        [Range(0f, 5f)]
        private float m_TiltPower = 1f;
        public float TiltPower { get { return m_TiltPower; } }

        [Tooltip("The amount the board tries to return to its default rotation if no input given.")]
        [Range(0f, 5f)]
        [SerializeField]
        private float m_Elasticity = 0f;
        public float Elasticity { get { return m_Elasticity; } }

    }
}

