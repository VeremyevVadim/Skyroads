
using System;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ScoreConverter
{
    public static string ToScoreString(this int score)
    {
        StringBuilder scoreString = new StringBuilder(score.ToString());
        if (score >= 1000)
        {
            scoreString.Insert(scoreString.Length - 3, '\'');
        }
        scoreString.Append(" score");

        return scoreString.ToString();
    }
    
    public static string ToScoreString(this float score)
    {
        return ToScoreString((int)score);
    }
    
    public static string ToScoreString(this double score)
    {
        return ToScoreString((int)score);
    }
}
