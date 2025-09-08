using UnityEngine;

public class ScoreSaveControl 
{
    public int HighScore;
    public const string HIGH_SCORE = "HighScore";
    public int LastScore=0;
    public ScoreSaveControl()
    {
        LoadHighScore();
    }

    public void TrySaveHighScore(int score)
    {
        if (score > HighScore)
        {
            LastScore = score;
            HighScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE, HighScore);
        }
    }

    public void LoadHighScore()
    {
        HighScore =PlayerPrefs.HasKey(HIGH_SCORE)?PlayerPrefs.GetInt(HIGH_SCORE):0;
    }
}
