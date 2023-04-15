using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameLate : MonoBehaviour
{
    private float timer = -1f;
    private bool timerStarted = false;

    void Update()
    {
        if (timerStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                LoadGame();
            }
        }
    }

    public void Wait2SecStart()
    {
        timerStarted = true;
        timer = 1.6f;
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
