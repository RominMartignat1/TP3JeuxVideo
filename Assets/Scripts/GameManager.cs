using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPaused = false;

    private const int firstGamingLevel = 1;
    private const int lastGamingLevel = 3;
    private const int maxLives = 3;

    private int actualLevel = 0;

    private int score = 0;
    private int accumulatedScore = 0;
    private string playerName = "Player1";
    private int lives = maxLives;

    bool scenesAreInTransition = false;

    private bool textsNotLinked = true;

    Text playerNameText;
    Text playerScoreText;
    Text playerLivesText;

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

    void Update()
    {
        LinkTexts();
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    private void LinkTexts()
    {
        if (textsNotLinked)
        {
            textsNotLinked = false;
            if (actualLevel == 0) return;  //pas utilisé sur l'écran de titre

            playerNameText = GameObject.FindGameObjectWithTag("TextName").GetComponent<Text>();
            playerNameText.text = playerName;

            playerLivesText = GameObject.FindGameObjectWithTag("TextLives").GetComponent<Text>();
            playerLivesText.text = lives.ToString();


            playerScoreText = GameObject.FindGameObjectWithTag("TextScore").GetComponent<Text>();
            playerScoreText.text = score.ToString();
        }
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;  //Pour éviter plusieurs transitions lancées en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
    }


    public void StartNextlevel(float delay)
    {
        if (scenesAreInTransition) return;  //Pour éviter plusieurs transitions lancées en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GetNextLevel()));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;

        if (lives == 0)
            SceneManager.LoadScene("SceneGameOver");
        else if (level == 2)
            SceneManager.LoadScene("Scene2");
        else if (level == 3)
            SceneManager.LoadScene("Scene3");
        else
            SceneManager.LoadScene("Scene1");

        scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = 0;
        score = 0;
        accumulatedScore = 0;
        SceneManager.LoadScene("Scene0");
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    private int GetNextLevel()
    {
        if (++actualLevel == lastGamingLevel + 1)
            actualLevel = firstGamingLevel;

        accumulatedScore = 0;
        return actualLevel;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        accumulatedScore += scoreToAdd;
        playerScoreText.text = score.ToString();
    }

    public void PlayerDie()
    {
        lives--;
        score -= accumulatedScore;
        playerLivesText.text = lives.ToString();
        playerScoreText.text = score.ToString();
        accumulatedScore = 0;
    }

}