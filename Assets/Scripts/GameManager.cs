using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPaused = false;

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
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public void RestartLevel(float delay)
    {
        if (scenesAreInTransition) return;  //Pour �viter plusieurs transitions lanc�es en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, actualLevel));
    }


    public void StartNextlevel(float delay)
    {
        if (scenesAreInTransition) return;  //Pour �viter plusieurs transitions lanc�es en bloc

        scenesAreInTransition = true;

        StartCoroutine(RestartLevelDelay(delay, GetNextLevel()));
    }

    private IEnumerator RestartLevelDelay(float delay, int level)
    {
        yield return new WaitForSeconds(delay);
        textsNotLinked = true;


           if(level == 0)
            SceneManager.LoadScene("HomeScene");

        else if (level == 1)
            SceneManager.LoadScene("SceneRomin");
       /* else if (level == 3)
            SceneManager.LoadScene("Scene3");
        else
            SceneManager.LoadScene("Scene1");*/

       scenesAreInTransition = false;
    }

    public void ResetGame()
    {
        lives = maxLives;
        actualLevel = 0;
        score = 0;
        accumulatedScore = 0;
        SceneManager.LoadScene("Menu");
    }

    private int GetNextLevel()
    {
        if (actualLevel == 0)
        {
            actualLevel++;
        }
        else
        {
            actualLevel = 0;
        }
        return actualLevel;
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