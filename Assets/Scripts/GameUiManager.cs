using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Text playerName1Text;
    [SerializeField] private Text playerName2Text;
    GameObject[] player1Lifes;
    GameObject[] player2Lifes;
    Color black = Color.black;
    private bool playerIsSet = false;
    private const int MAX_INITIAL_LIFE = 3;
    private const int MAX_POSSIBLE_LIFE = 5;
    private int player1Life;
    private int player2Life;
    private bool gameIsEnded = false;

    void Start()
    {
        player1Life = MAX_INITIAL_LIFE;
        player2Life = MAX_INITIAL_LIFE;
        player1Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer1");
        player2Lifes = GameObject.FindGameObjectsWithTag("HearthPlayer2");
        Debug.Log(player1Lifes.Length);

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsSet)
        {
            SetPlayer();
            SetHearths();
        }
        if (!gameIsEnded)
        {

            CheckIfGameEnded();
        }
        else
        {
            FinishGame();
        }

    }

    private void SetHearths()
    {
    }

    private void FinishGame()
    {
        throw new NotImplementedException();
    }

    private void CheckIfGameEnded()
    {
        if (player1Life <= 0 || player2Life <= 0)
        {
            gameIsEnded = true;
        }
    }

    private void SubstractLife(PlayerController.PlayerTeam player, int life)
    {
        if (player == PlayerController.PlayerTeam.Blue)
        {
            player1Life -= life;
            player1Lifes[player1Life].GetComponent<Image>().color = black;
        }
        else
        {
            player2Life -= life;
            player2Lifes[player2Life].GetComponent<Image>().color = black;
        }

        if (player1Life <= 0 || player2Life <= 0)
        {
            //cela call le gamemanager avec qui qui a perdu 
        }
    }

    private void AddLife(PlayerController.PlayerTeam player, int life)
    {
        if (player == PlayerController.PlayerTeam.Blue)
        {
            player1Life += life;
            player1Lifes[player1Life].GetComponent<Image>().color = Color.white;
        }
        else
        {
            player2Life += life;
            player2Lifes[player2Life].GetComponent<Image>().color = Color.white;
        }
    }
    private void SetPlayer()
    {
        playerName1Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER1);
        playerName2Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER2);
    }
}
