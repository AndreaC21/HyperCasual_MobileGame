using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameColor", menuName = "ScriptableObjects/GameColor")]
public class GameColor : ScriptableObject
{
    [SerializeField] private GameColorOptions _ennemy = default;
    [SerializeField] private GameColorOptions _ground = default;
    [SerializeField] private GameColorOptions _endLine = default;
    [SerializeField] private GameColorOptions _player = default;

    public Color PlayerColor
    {
        get => _player.Color;
        set
        {
            _player.SetColor(value);
        }
    }

    public Color EnnemyColor
    {
        get => _ennemy.Color;
        set
        {
            _ennemy.SetColor(value);
        }
    }

    public Color GroundColor
    {
        get => _ground.Color;
        set
        {
            _ground.SetColor(value);
        }
    }

    public Color EndLineColor
    {
        get => _endLine.Color;
        set
        {
            _endLine.SetColor(value);
        }
    }

    public void ApplyColorToMaterial()
    {
        _player.SetColor(_player.Color);
        _ennemy.SetColor(_ennemy.Color);
        _ground.SetColor(_ground.Color);
        _endLine.SetColor(_endLine.Color);
    }

    [Serializable]
    protected class GameColorOptions
    {
        [SerializeField] private Material _material;
        [SerializeField] private Color _color;

        public void SetColor(Color newColor)
        {
            _material.color = newColor;
            _color = newColor;
        }

        public Color Color
        {
            get => _color;
        }
    }
}
