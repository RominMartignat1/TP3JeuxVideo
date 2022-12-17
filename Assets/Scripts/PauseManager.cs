using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static bool gameIsPaused;
    public static bool GameIsPaused { get { return gameIsPaused; } }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gameIsPaused) Time.timeScale = 0f;
        else Time.timeScale = 1;
    }
}
