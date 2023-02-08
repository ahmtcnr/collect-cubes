using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _preGameParent;
    [SerializeField] private GameObject _winGameParent;
    [SerializeField] private GameObject _loseGameParent;


    private void OnEnable()
    {
        Actions.OnGameStateChanged += HandleGameStateChanges;
    }

    private void OnDisable()
    {
        Actions.OnGameStateChanged -= HandleGameStateChanges;
    }

    private void HandleGameStateChanges(GameState state)
    {
        switch (state)
        {
            case GameState.GenerateLevel:
                _preGameParent.SetActive(true);
                _winGameParent.SetActive(false);
                _loseGameParent.SetActive(false);
                break;
            case GameState.StartGame:
                _preGameParent.SetActive(false);
                break;
            case GameState.WinGame:
                _winGameParent.SetActive(true);
                break;
            case GameState.LoseGame:
                _loseGameParent.SetActive(true);
                break;
        }
    }
}