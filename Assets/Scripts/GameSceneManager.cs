using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent addSpeed;
    [SerializeField] private Text playerName1Text;
    [SerializeField] private Text playerName2Text;
    [SerializeField] private Text timerText;

    GameObject[] player1Lifes;
    GameObject[] player2Lifes;
    Color black = Color.black;
    private const int MAX_INITIAL_LIFE = 3;
    private const int MAX_POSSIBLE_LIFE = 5;
    private int player1Life;
    private int player2Life;
    private static bool gameIsEnded = false;
    private const int LIFE_TO_ADD = 1;
    private const float TIMER_FOR_SPEED = 30f;
    private float timer = TIMER_FOR_SPEED;

    public static bool GameIsEnded { get { return gameIsEnded; } }

    void Start()
    {
        player1Life = MAX_INITIAL_LIFE;
        player2Life = MAX_INITIAL_LIFE;
        player1Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer1"); //liste des coeurs de gauche
        player2Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer2"); // liste des coeurs de droite
        playerName1Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER1);
        playerName2Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER2);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsEnded)
        {
            CheckTimer();
            CheckIfGameEnded();
        }
        else
        {
            FinishGame();
        }

    }

    private void CheckTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            addSpeed.Invoke();
            timer = TIMER_FOR_SPEED;
        }

        ChangeTimerText();
    }

    private void ChangeTimerText()
    {
        timerText.text = ((int)timer).ToString();
    }

    private void FinishGame()
    {
        
        if (player1Life <= 0)
        {
            GameManager.instance.EndGame(playerName2Text.text);
        }
        else if(player2Life <= 0)
        {
            GameManager.instance.EndGame(playerName1Text.text);
        }
        
    }

    private void CheckIfGameEnded()
    {
        if (player1Life <= 0 || player2Life <= 0)
        {
            gameIsEnded = true;
        }
    }

    public void SubstractLife(PlayerController.PlayerTeam player)
    {
        Debug.Log("substract life a été call");
        if (player == PlayerController.PlayerTeam.Blue)
        {
            player1Life -= LIFE_TO_ADD;
            player1Lifes[player1Life].GetComponent<Image>().color = black;
        }
        else
        {
            player2Life -= LIFE_TO_ADD;
            player2Lifes[player2Life].GetComponent<Image>().color = black;
        }

    }


    public void AddLife(PlayerController.PlayerTeam player)
    {
        
        if (player == PlayerController.PlayerTeam.Blue)
        {
            if (player1Life++ >= MAX_POSSIBLE_LIFE) return;
            player1Lifes[player1Life - 1].GetComponent<Image>().color = Color.white;
            player1Life += LIFE_TO_ADD;
           
        }
        else
        {
            if (player2Life++ >= MAX_POSSIBLE_LIFE) return;
            player2Lifes[player2Life - 1].GetComponent<Image>().color = Color.white;
            player2Life += LIFE_TO_ADD;
            
        }
    }
}
