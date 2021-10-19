using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ysocorp.ui
{
    public class ColorPersonnalisation : MonoBehaviour
    {
        [SerializeField] private GameColor _gameColor = default;
        [SerializeField] private GameObject _panelColor = default;
        [SerializeField] private Image _playerImage = default;
        [SerializeField] private Image _ennemyImage = default;
        [SerializeField] private Image _groundImage = default;
        [SerializeField] private Image _endLineImage = default;
        [SerializeField] private ColorWheel _colorWheel = default;
        [SerializeField] private GameObject _colorWheelPanel = default;

        private Image _selection;

        private void Awake()
        {
            _gameColor.ApplyColorToMaterial();
        }
        private void Start()
        {
            _colorWheel.ColorWheelButton.onClick.AddListener(delegate { SelectColor(); });
        }

        public void Build()
        {
            _panelColor.SetActive(true);
            LoadColor();
        }

        private void LoadColor()
        {
            _playerImage.color = _gameColor.PlayerColor;
            _endLineImage.color = _gameColor.EndLineColor;
            _ennemyImage.color = _gameColor.EnnemyColor;
            _groundImage.color = _gameColor.GroundColor;
        }

        /// <summary>
        /// Call on UI Button in Area
        /// </summary>
        /// <param name="image"></param>
        public void SelectImageToUpdate(Image image)
        {
            _selection = image;
            _colorWheelPanel.SetActive(true);
            _colorWheel.SelectedColor = image.color;
        }

        private void SelectColor()
        {
            _selection.color = _colorWheel.SelectedColor;
            _colorWheelPanel.SetActive(false);
        }

        public void ClosePanel()
        {
            _panelColor.SetActive(false);
            SaveColor();
        }

        private void SaveColor()
        {
            _gameColor.PlayerColor = _playerImage.color;
            _gameColor.EndLineColor = _endLineImage.color;
            _gameColor.EnnemyColor = _ennemyImage.color;
            _gameColor.GroundColor = _groundImage.color;
        }
    }
}
