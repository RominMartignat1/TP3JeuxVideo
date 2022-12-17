using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Text playerName1Text;
    [SerializeField] private Text playerName2Text;
    GameObject[] player1Lifes;
    GameObject[] player2Lifes;
    Color black = Color.black;
    private const int MAX_INITIAL_LIFE = 3;
    private const int MAX_POSSIBLE_LIFE = 5;
    private int player1Life;
    private int player2Life;
    private bool gameIsEnded = false;
    private const int LIFE_TO_ADD = 1;

    void Start()
    {
        player1Life = MAX_INITIAL_LIFE;
        player2Life = MAX_INITIAL_LIFE;
        player1Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer1"); //liste des coeurs de gauche
        player2Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer2"); // liste des coeurs de droite
        playerName1Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER1);
        playerName2Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER2);
      //  SubstractLife(PlayerController.PlayerTeam.Blue);
       // SubstractLife(PlayerController.PlayerTeam.Blue);
       // SubstractLife(PlayerController.PlayerTeam.Blue);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsEnded)
        {

            CheckIfGameEnded();
        }
        else
        {
            FinishGame();
        }

    }

    private void FinishGame()
    {
        if (player1Life <= 0)
        {
            GameManager.instance.EndGame(playerName1Text.text);
        }
        else if (player2Life <= 0)
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

        if (player1Life <= 0 || player2Life <= 0)
        {
            ManagerWinner(player);
                
        }
    }

    private void ManagerWinner(PlayerController.PlayerTeam player)
    {
        if (player == PlayerController.PlayerTeam.Blue)
        {
            GameManager.instance.EndGame(playerName2Text.text);
        }
        else
        {
            GameManager.instance.EndGame(playerName1Text.text);
            
        }
    }

    public void AddLife(PlayerController.PlayerTeam player)
    {
        
        if (player == PlayerController.PlayerTeam.Blue)
        {
            if (player1Life++ > MAX_POSSIBLE_LIFE) return;
            player1Lifes[player1Life - 1].GetComponent<Image>().color = Color.white;
            player1Life += LIFE_TO_ADD;
           
        }
        else
        {
            if (player2Life++ > MAX_POSSIBLE_LIFE) return;
            player2Lifes[player2Life - 1].GetComponent<Image>().color = Color.white;
            player2Life += LIFE_TO_ADD;
            
        }
    }
}
