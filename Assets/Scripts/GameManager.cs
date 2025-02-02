using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum PLAYER { PLAYER1, PLAYER2 };
    private const int MENU_SCENE = 0;
    private const int GAME_SCENE = 1;
    private const int END_SCENE = 2;

    public bool isPaused = false;

    private int actualLevel = 0;
    private string winnerName = "Patoum patoum";

    bool scenesAreInTransition = false;

    private string player1Name = "Joueur 1";
    private string player2Name = "Joueur 2";


    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        actualLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void savePlayerName(PLAYER playernumber, string playerName)
    {
        if (playerName == "") return;
        if (playernumber == PLAYER.PLAYER1)
        {
            player1Name = playerName;
            Debug.Log(playerName);
        }
        else
        {
            player2Name = playerName;
            Debug.Log(playerName);
        }

}

    public string getPlayerName(PLAYER playernumber)
    {
        if (playernumber == PLAYER.PLAYER1)
        {
            return player1Name;
        }
        else
        {
            return player2Name;
        }

    }

    public string GetWinner()
    {
        return winnerName;
    }

    public void EndGame(string text)
    {
        winnerName = text;
        StartEnding(1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    private void StartEnding(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, END_SCENE));
    }


    public void StartGame(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GAME_SCENE));
    }

    public void StartMenu(float delay)
    {
        if (scenesAreInTransition) return;

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, MENU_SCENE));
    }


    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);

        if(level == MENU_SCENE)
            SceneManager.LoadScene("HomeScene");
        else if (level == GAME_SCENE)
            SceneManager.LoadScene("CommonScene");
        else if (level == END_SCENE)
            SceneManager.LoadScene("GameOverScene");


       scenesAreInTransition = false;
    }
}