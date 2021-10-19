using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ysocorp.ui
{
    public class GameManagerUI : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager = default;

        [SerializeField] private GameObject _panelLose = default;
        [SerializeField] private GameObject _panelWin = default;
        [SerializeField] private Image _transitionLevel = default;

        [Header("StartMenu")]
        [SerializeField] private GameObject _panelStart = default;
        [SerializeField] private Text _textLevel = default;
        [SerializeField] private Text _textCharacterSaved = default;
        [Header("MaxLevel")]
        [SerializeField] private GameObject _reachMaxLevel = default;
        [SerializeField] private GameObject _exitButton = default;

        private void Awake()
        {
            DisplayStartPanel();
        }

        /// <summary>
        /// Call on UI Button Play
        /// </summary>
        /// 
        public void StartLevel()
        {
            _gameManager.StartLevel();
            _panelStart.SetActive(false);
            _reachMaxLevel.SetActive(false);
        }

        public void DisplayReachMaxLevel()
        {
            _panelWin.SetActive(false);
            _reachMaxLevel.SetActive(true);
            _exitButton.SetActive(!_panelStart.activeSelf);
        }

        public void DisplayStartPanel()
        {
            _panelStart.SetActive(true);
            _textLevel.text = "Level " + (PlayerPrefs.GetInt("CurrentLevel") + 1) + "/" + _gameManager.MaxLevel;
            _textCharacterSaved.text = "Character Saved " + PlayerPrefs.GetInt("CharacterSaved");
        }

        #region Player Lose
        public void PlayerLose()
        {
            _panelLose.SetActive(true);
        }

        /// <summary>
        /// Call ON UI Button Play Again from Panel Lose
        /// </summary>
        public void PlayAgain()
        {
            _panelLose.SetActive(false);
            _gameManager.RestartLevel();
        }

        #endregion

        #region Player Win
        public void PlayerWin()
        {
            _panelWin.SetActive(true);
        }
        /// <summary>
        /// Call onUI button from Panel Win
        /// </summary>
        public void NextLevel()
        {
            _panelWin.SetActive(false);
            if (_gameManager.ReachMaxLevel())
            {
                DisplayReachMaxLevel();
            }
            else
            {
                _transitionLevel.gameObject.SetActive(true);
                StartCoroutine(StartFadeTransition(true));
                _gameManager.LoadNextLevel();
            }
        }

        private IEnumerator StartFadeTransition(bool fadeAway)
        {
            float duration = 1.0f;
            for (float t = 0.0f; t < duration; t += Time.deltaTime / duration)
            {
                _transitionLevel.color = Color.Lerp(Color.black, Color.clear, t);

                yield return null;
            }

            _transitionLevel.gameObject.SetActive(false);
            _gameManager.StartLevel();
        }
        #endregion

        /// <summary>
        /// CAll on UI button in panelREcahMaxLevel
        /// </summary>
        public void RestartProgression()
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.SetInt("CharacterSaved", 0);
            _gameManager.LoadCurrentLevel();
            _reachMaxLevel.SetActive(false);
            DisplayStartPanel();
        }

        public void ExitLevel()
        {
            _gameManager.LoadCurrentLevel();
            DisplayStartPanel();
            DisplayReachMaxLevel();
        }
    }
}
 
