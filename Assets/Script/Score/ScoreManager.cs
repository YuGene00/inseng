using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    //singleton
    public static ScoreManager instance = null;

    //inspector
    public Text scoreText;

    //variable
    int score = 0;
    int Score {
        get {
            return score;
        }
        set {
            if (value > score) {
                SoundManager.instance.PlayEffectSound(SoundManager.EffectType.COIN);
            }
            score = value;
            scoreText.text = score.ToString();
        }
    }
    public int GetScore {
        get {
            return score;
        }
    }

    public void Awake() {
        instance = this;
    }

    public void AddScore(int value) {
        Score += value;
    }

    public void UpdateHighScore() {
        SetHighScore(score);
    }

    public void SetHighScore(int score) {
        if (!PlayerPrefs.HasKey("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        } else if (score > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public int GetHighScore() {
        return PlayerPrefs.GetInt("HighScore");
    }
}
