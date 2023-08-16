using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livestext;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip GamerestartSFX;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livestext.text = playerLives.ToString();
        scoretext.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoretext.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        livestext.text = playerLives.ToString();
        StartCoroutine(ReloadSceneAfterDelay());
    }

    IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator ResetGameSession()
    {
        AudioSource.PlayClipAtPoint(GamerestartSFX, transform.position);
        yield return new WaitForSeconds(GamerestartSFX.length);
        FindObjectOfType<ScenePersist>().ResetScenePersisit();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
