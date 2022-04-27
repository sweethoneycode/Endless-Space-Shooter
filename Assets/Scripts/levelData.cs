using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelData
{
    private static int highScore;

    public static int HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
        }
    }
}
