using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorWheel : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager = default;
        [SerializeField] private Button _buttonColorWheel = default;
        [SerializeField] private Image _imageColorWheel = default;
        [SerializeField] private RectTransform _imageColorWheelRect = default;

        private int _imageRectWidth;
        private int _imageRectHeight;
        public Color _selectedColor;

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
            }
        }

        public Button ColorWheelButton
        {
            get => _buttonColorWheel;
            set => _buttonColorWheel = value;
        }

        void Start()
        {
            _imageRectWidth = (int)_imageColorWheelRect.rect.width;
            _imageRectHeight = (int)_imageColorWheelRect.rect.height;

            _selectedColor.a = 1.0f;
        }

        /// <summary>
        /// Call on UIButton ColorWheel
        /// </summary>
        public void ClickOnColorWheel()
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_imageColorWheelRect, _inputManager.ScreenPosition(), null, out localMousePosition) ;

            if (!IsOutsideCircle(localMousePosition))
            {
                localMousePosition += new Vector2(_imageRectWidth * 0.5f, _imageRectHeight * 0.5f);

                float x = Mathf.Clamp01(localMousePosition.x / _imageRectWidth);
                float y = Mathf.Clamp01(localMousePosition.y / _imageRectHeight);

                int xTexture = Mathf.RoundToInt(x * _imageColorWheel.sprite.texture.width);
                int yTexture = Mathf.RoundToInt(y * _imageColorWheel.sprite.texture.height);

                _selectedColor = _imageColorWheel.sprite.texture.GetPixel(xTexture, yTexture);
            }
        }

        private bool IsOutsideCircle(Vector3 point)
        {
            Vector2 center = Vector2.zero;
            float radius = _imageRectWidth * 0.5f;
            return Mathf.Pow((point.x - center.x), 2) + Mathf.Pow((point.y - center.y), 2) > radius * radius;
        }
    }

