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
    private bool playerIsSet = false;
    private const int MAX_LIFE = 3;
    private int player1Life;
    private int player2Life;
    private bool gameIsEnded = false;

    void Start()
    {
        player1Life = MAX_LIFE;
        player2Life = MAX_LIFE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsSet)
        {
            SetPlayer();
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

    private void SetLife(GameManager.PLAYER player ,int life)
    {
        /*if (player == GameManager.PLAYER.PLAYER1)
        {
            player1Name = playerName;
            Debug.Log(playerName);
        }
        else
        {
            player2Name = playerName;
            Debug.Log(playerName);
        }*/
    }

    private void SetPlayer()
    {
        playerName1Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER1);
        playerName2Text.text = GameManager.instance.getPlayerName(GameManager.PLAYER.PLAYER2);
    }
}
