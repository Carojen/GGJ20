using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flaskpost
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_MenuScreen = null;

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

        public bool IsPaused { get; private set; } = false;

        private GameBoard m_Board = null;
        public GameBoard Board
        {
            get
            {
                if (m_Board == null)
                    m_Board = FindObjectOfType<GameBoard>();
                return m_Board;
            }
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                DontDestroyOnLoad(gameObject);
            }                
            else if (m_Instance != this)
                Destroy(this.gameObject);
        }

        private void Start()
        {
            CurrentState = GameState.Running;
            if (m_Board == null)
                m_Board = FindObjectOfType<GameBoard>();
        }

        private void Update()
        {
            if (CurrentState == GameState.Running)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                }
                else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    OpenMenu();
                    Pause();
                }
            }
            else if (CurrentState == GameState.Menu)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    CurrentState = GameState.Running;
                    Unpause();
                }
            }
        }

        public void Pause()
        {
            IsPaused = true;
            FindObjectOfType<Character>().Freeze();
        }

        public void Unpause()
        {
            IsPaused = false;
            FindObjectOfType<Character>().Unfreeze();
        }

        public void OpenMenu()
        {
            CurrentState = GameState.Menu;
        }

        public void Restart()
        {
            CurrentState = GameState.Starting;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
#endif
        }
    }
}