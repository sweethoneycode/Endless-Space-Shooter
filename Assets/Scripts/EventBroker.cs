using System;

public class EventBroker
{
    public static event Action ProjectileOutOfBounds;
    public static event Action RestoreShields;
    public static event Action PlayerHit;
    public static event Action PlayerDeath;
    public static event Action PlayerLives;
    public static event Action UpdatePlayerScore;
    public static event Action EndGame;
    public static event Action RestartGame;
    public static event Action StartGame;
    public static event Action PauseGame;
    public static event Action ExtraLife;
    public static event Action PlayAd;

    public static void CallPlayAd()
    {
        if (PlayAd != null)
            PlayAd();
    }

    public static void CallExtraLife()
    {
        if (ExtraLife != null)
            ExtraLife();
    }
    public static void CallRestoreShields()
    {
        if (RestoreShields != null)
            RestoreShields();
    }

    public static void CallPlayerHit()
    {
        if (PlayerHit != null)
            PlayerHit();
    }
    public static void CallPauseGame()
    {
        if (PauseGame != null)
            PauseGame();
    }

    public static void CallStartGame()
    {
        if (StartGame != null)
            StartGame();
    }

    public static void CallProjectileOutOfBounds()
    {
        if (ProjectileOutOfBounds != null)
            ProjectileOutOfBounds();
    }



    public static void CallPlayerDeath()
    {
        if (PlayerDeath != null)
            PlayerDeath();
    }

    public static void CallPlayerLives()
    {
        if (PlayerLives != null)
            PlayerLives();
    }

    public static void CallCallUpdateScore()
    {
        if (UpdatePlayerScore != null)
            UpdatePlayerScore();
    }

    public static void CallEndGame()
    {
        if (EndGame != null)
            EndGame();
    }

    public static void CallRestartGame()
    {
        if (RestartGame != null)
            RestartGame();
    }
}
