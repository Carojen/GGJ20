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

        [SerializeField]
        private GameObject m_WinScreen = null;

        public enum GameState
        {
            Starting,
            Running,
            Menu,
            WinScreen,
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
                //DontDestroyOnLoad(gameObject);
            }
            else if (m_Instance != this)
                Destroy(this.gameObject);
        }

        private Target[] m_Targets = null;

        private void Start()
        {
            CurrentState = GameState.Running;
            if (m_Board == null)
                m_Board = FindObjectOfType<GameBoard>();
            m_Targets = FindObjectsOfType<Target>();
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
                else if(CheckWinCondition())
                {
                    DisplayWinScreen();
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

        private void DisplayWinScreen()
        {
            CurrentState = GameState.WinScreen;
            Pause();
            m_WinScreen.SetActive(true);
            StartCoroutine(WinRoutine());
        }

        private IEnumerator WinRoutine()
        {
            yield return new WaitForSeconds(5f);
            m_WinScreen.SetActive(false);
            CurrentState = GameState.Starting;
            Restart();            
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

        private bool CheckWinCondition()
        {
            foreach (var target in m_Targets)
            {
                if (!target.Repaired)
                {
                    return false;
                }
            }
            return true;
        }
    }
}