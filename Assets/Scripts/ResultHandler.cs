using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultHandler : MonoBehaviour
{
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI player1ScoreLabel;
    public TextMeshProUGUI player2ScoreLabel;
    public TextMeshProUGUI winnerSign;
    public Player1Data player1Data;
    public Player2Data player2Data;
    public GameObject panel;
    public Button nextRound;
    public Button playAgainButton;

    private int scoreCounter;
    private float scoreCounterHelper;

    void Start()
    {
        if (GameData.gameEnabled)
            panel.SetActive(false);
        scoreCounterHelper = 0;
        scoreCounter = 0;
        player1Name.text = player1Data.playerName;
        player2Name.text = player2Data.playerName;
        PlayerScoreLabelLoad();
    }

    void Update()
    {
        if (GameData.gameEnabled)
        {
            scoreCounterHelper += Time.deltaTime;
            scoreCounter = (int)(scoreCounterHelper * GameData.speed);
            scoreLabel.text = scoreCounter.ToString();
        }
    }

    public void OnNextRoundClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.gameEnabled = true;
        GameData.round++;
    }

    public void OnPlayAgainClick()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player1Data.score = 0;
        player2Data.score = 0;
        GameData.gameEnabled = true;
        GameData.round = 1;
    }

    public void OnBackToMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    void PlayerScoreLabelLoad()
    {
        player1ScoreLabel.text = player1Data.score.ToString();
        player2ScoreLabel.text = player2Data.score.ToString();
    }

    public void ResultPanelHandler()
    {
        if (GameData.isPlayer1Wins)
            player1Data.score += scoreCounter;
        else
            player2Data.score += scoreCounter;
        PlayerScoreLabelLoad();

        if (GameData.round < 3 && GameData.round > 0)
        {
            nextRound.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(false);
            if (GameData.isPlayer1Wins)
                winnerSign.text = player1Name.text;
            else winnerSign.text = player2Name.text;
            winnerSign.text = winnerSign.text + " won this round!";
        }
        else
        {
            nextRound.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(true);
            if (player1Data.score > player2Data.score)
                winnerSign.text = player1Name.text + " won the game!";
            else if (player1Data.score < player2Data.score)
                winnerSign.text = player2Name.text + " won the game!";
            else winnerSign.text = "It's a draw!";
        }

        panel.SetActive(true);
    }

    
}
