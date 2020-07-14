﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    void Start()
    {

        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
