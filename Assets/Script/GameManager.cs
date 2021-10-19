using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ysocorp.ui;
using ysocorp.levels;
using ysocorp.obstacle;
using ysocorp.character;

    public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerUI _gameManagerUI = default;
    [SerializeField] private Camera _camera = default;
    [SerializeField] private SwipeDetection _swipeDirection = default;
    [SerializeField] private Transform _gameMover = default;
    [Header("Levels")]
    [SerializeField] private Levels _levels = default;
    [SerializeField] private LevelInstanceUnity _prefabLevel = default;
    [Header("Character")]
    [SerializeField] private Transform _characterContainer = default;
    [SerializeField] private Character _characterPrefab = default;

    private float targetPositionX;
    private Vector3 _direction;
    private bool _isMoving;
    private float _speed = 20.0f;
    private float _speedGame = 10f;
    private float _jumpForce = 10.0f;
    private List<Character> _players;
    private int _currentLevel;
    private bool _isGameFinished;

    private LevelInstanceUnity _levelInstance;

    private List<Character> _winCharacters;

    public float CameraClipNear
    {
        get => _camera.nearClipPlane;
    }

    public int MaxLevel
    {
        get => _levels.Size;
    }

    void Start()
    {
        _swipeDirection.OnSwipeDirection += MovePlayers;
        _swipeDirection.OnSwipeVertical += MoveVerticalPlayer;
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        Initialization();
        LoadLevel();
    }

    private void Initialization()
    {
        _players = new List<Character>();
        _isGameFinished = false;
        enabled = false;
        _swipeDirection.enabled = false;
        _characterContainer.transform.localPosition = new Vector3(0, -5.5f, 0);
        _gameMover.transform.localPosition = Vector3.zero;
    }
   
    private Vector3 GetRandomPosition()
    {
        float boundX = CharacterArea.AREA_X;
        return new Vector3(Random.Range(-2, 2),LevelInstanceUnity.POSITION_CHARACTER_Y, 0.0f);
    }

    void OnDestroy()
    {
        _swipeDirection.OnSwipeDirection -= MovePlayers;
        _swipeDirection.OnSwipeVertical -= MoveVerticalPlayer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isGameFinished)
        {
            _gameMover.Translate(Vector3.forward * _speedGame * Time.deltaTime);
            if (_isMoving)
            {
                _characterContainer.Translate(_direction * _speed * Time.deltaTime);
                if ((targetPositionX - _characterContainer.position.x) * _direction.x < 0.01f)
                {
                    _isMoving = false;
                    RotateCharacterTo(0);
                }
            }

            if (_players.Count == 0)
            {
                PlayerLose();
            }
        }
        else
        {
            foreach(Character character in _players)
            {
                _characterContainer.Translate(Vector3.forward * _speedGame * Time.deltaTime);
            }
        }
       
    }

    private void MovePlayers(int direction)
    {
        targetPositionX = _swipeDirection.WorldTouchPosition.x;
        _direction = new Vector3(direction,0, 0);
        _isMoving = true;
        RotateCharacterTo(45 * direction);
    }

    private void RotateCharacterTo(int angleDegree)
    {
        foreach(Character character in _players)
        {
            character.transform.localEulerAngles = Vector3.up * angleDegree;
        }
    }

    private void MoveVerticalPlayer(int direction)
    {
        if (direction==1)
        {
            foreach (Character character in _players)
            {
                character.Jump(_jumpForce);
            }
        }
        else
        {
            foreach (Character character in _players)
            {
                character.RunSlide();
            }
        }
    }

    public void KillCharacter(Character deadCharacter)
    {
        deadCharacter.transform.SetParent(null);
        _players.Remove(deadCharacter);
    }

    public void AddCharacter(Character newCharacter)
    {
      _players.Add(newCharacter);
    }

    private void PlayerLose()
    {
        _gameManagerUI.PlayerLose();
        enabled = false;
    }

    public void PlayerWin(Character winCharacter)
    {
        if (_isGameFinished)
        {
            _winCharacters.Add(winCharacter);
            return;
        }
        _isGameFinished = true;
        _winCharacters = new List<Character>();
       _winCharacters.Add(winCharacter);
        _gameManagerUI.PlayerWin();
        int totalCharacterSaved = PlayerPrefs.GetInt("CharacterSaved") + _players.Count;
        PlayerPrefs.SetInt("CharacterSaved", totalCharacterSaved);
        if (!ReachMaxLevel())
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel + 1);
        }
    }

    public void StartLevel()
    {
        _swipeDirection.enabled = true;
        foreach ( Character character in _players)
        {
            character.StartLife();
            enabled = true;
        }
    }

    private void LoadLevel()
    {
        if (ReachMaxLevel())
        {
            _gameManagerUI.DisplayReachMaxLevel();
        }
        Level levelToLoad = _levels.GetLevel(_currentLevel);
        _levelInstance = Instantiate(_prefabLevel);
        _levelInstance.Build(levelToLoad, this);
        InstantiateCharacter();
    }

    public void RestartLevel()
    {
        _gameMover.transform.position = Vector3.zero;
        _levelInstance.RestartLevel();
        InstantiateCharacter();
        StartLevel();
    }

    private void InstantiateCharacter()
    {
        for (int i = 0; i < _levelInstance.Level.CharacterOnStart; i++)
        {
            Character character = Instantiate(_characterPrefab, _characterContainer);
            character.transform.localPosition = GetRandomPosition();
            character.Build(this);
            AddCharacter(character);
        }
    }

    public void LoadNextLevel()
    {
        Destroy(_levelInstance.gameObject);
        foreach (Character character in _winCharacters)
        {
            Destroy(character.gameObject);
        }
        Initialization();
        _currentLevel++;
        LoadLevel();
    }

    public void LoadCurrentLevel()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        Destroy(_levelInstance.gameObject);
        foreach (Character character in _winCharacters)
        {
            Destroy(character.gameObject);
        }
        Initialization();
        LoadLevel();
    }

    public bool ReachMaxLevel()
    {
        return _currentLevel >= MaxLevel-1;
    }
}
