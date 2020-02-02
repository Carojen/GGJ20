using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flaskpost
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_HowToScreen = null;

        [SerializeField]
        private Button m_PlayButton = null;

        [SerializeField]
        private Button m_RestartButton = null;

        [SerializeField]
        private Button m_HowToButton = null;

        [SerializeField]
        private Button m_ExitButton = null;

        private void Start()
        {
            m_PlayButton.onClick.RemoveAllListeners();
            m_PlayButton.onClick.AddListener(() => GameManager.Instance.Unpause());

            m_RestartButton.onClick.RemoveAllListeners();
            m_RestartButton.onClick.AddListener(() => GameManager.Instance.Restart());

            m_HowToButton.onClick.RemoveAllListeners();
            m_HowToButton.onClick.AddListener(() => OpenHowTo());

            m_ExitButton.onClick.RemoveAllListeners();
            m_ExitButton.onClick.AddListener(() => GameManager.Instance.Quit());
        }

        private void OpenHowTo()
        {
            m_HowToScreen?.SetActive(true);
        }

        public void CloseHowTo()
        {
            m_HowToScreen?.SetActive(false);
        }
    }
}