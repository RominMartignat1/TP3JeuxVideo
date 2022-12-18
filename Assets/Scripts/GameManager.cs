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
    private const int maxLives = 3;

    private int actualLevel = 0;
    private string winnerName = "joueur1";

    private int accumulatedScore = 0;
    private int lives = maxLives;

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    

    private string player1Name = "joueur1";
    private string player2Name = "joueur2";


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
        StartEnding(1f);
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
        if (scenesAreInTransition) return;  //Pour �viter plusieurs transitions lanc�es en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, END_SCENE));
    }


    public void StartGame(float delay)
    {
        if (scenesAreInTransition) return;  //Pour �viter plusieurs transitions lanc�es en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GAME_SCENE));
    }

    public void StartMenu(float delay)
    {
        if (scenesAreInTransition) return;  //Pour �viter plusieurs transitions lanc�es en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, MENU_SCENE));
    }
    

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;


           if(level == MENU_SCENE)
            SceneManager.LoadScene("HomeScene");

        else if (level == GAME_SCENE)
            SceneManager.LoadScene("CommonScene");
        else if (level == END_SCENE)
            SceneManager.LoadScene("GameOverScene");
       /* else
            SceneManager.LoadScene("Scene1");*/

       scenesAreInTransition = false;
    }


    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = 0;
      
        accumulatedScore = 0;
        SceneManager.LoadScene("Menu");
    }


    public void PlayerDie()
    {
        lives--;
        //playerLivesText.text = lives.ToString();
        //playerScoreText.text = score.ToString();
        accumulatedScore = 0;
    }

}