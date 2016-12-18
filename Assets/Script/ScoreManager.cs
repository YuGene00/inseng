using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    //singleton
    public static ScoreManager instance = null;

    //variable
    int score = 0;
    public int GetScore {
        get {
            return score;
        }
    }

    public void Awake() {
        instance = this;
    }

    public void AddScore(int value) {
        score += value;
    }

    public void SetHighScore(int score) {
        if (!PlayerPrefs.HasKey("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        } else if (score > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public int GetHighScore(int score) {
        return PlayerPrefs.GetInt("HighScore");
    }
}
