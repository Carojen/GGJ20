using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flaskpost
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Starting,
            Running,
            Menu,
            Quitting,
        }
        public GameState CurrentState { get; private set; } = GameState.Starting;

        private static GameManager m_Instance;
        public static GameManager Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = FindObjectOfType<GameManager>();
                return m_Instance;
            }
        }



        private void Awake()
        {
            if (m_Instance == null)
                m_Instance = this;
            else if (m_Instance != this)
                Destroy(this.gameObject);
        }

        private void Start()
        {
            CurrentState = GameState.Running;
        }

        public void Pause()
        {

        }

        public void Unpause()
        {

        }

        public void Quit()
        {
            if (CurrentState != GameState.Quitting)
                StartCoroutine(QuitRoutine());
        }

        private IEnumerator QuitRoutine()
        {
            CurrentState = GameState.Quitting;
            //while(endAnimation running)
            yield return null;

            Application.Quit();
        }
    }
}