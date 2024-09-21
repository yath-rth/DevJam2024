using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    int score;

    private void OnEnable() {
        EnemyStats.OnEnemyDeath += AddScore;
    }

    private void OnDisable() {
        EnemyStats.OnEnemyDeath -= AddScore;
    }

    private void Update() {
        text.text = score.ToString();
    }

    void AddScore(int amt)
    {
        score++;
    }
}
