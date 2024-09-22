using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text_inGame, text_death, text_highScore;

    int score, highScore;

    private void OnEnable()
    {
        EnemyStats.OnEnemyDeath += AddScore;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    private void OnDisable()
    {
        EnemyStats.OnEnemyDeath -= AddScore;
    }

    private void Update()
    {
        text_inGame.text = score.ToString();
        text_death.text = score.ToString();
        text_highScore.text = highScore.ToString();

        if (highScore < score) highScore = score;

        if (Player.instance.GetPlayerStats().IsDead == true) PlayerPrefs.SetInt("highScore", highScore);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    void AddScore(int amt)
    {
        score++;
    }
}
