using System;

public class EventBroker
{
    public static event Action ProjectileOutOfBounds;
    public static event Action PlayerDeath;
    public static event Action PlayerLives;
    public static event Action UpdatePlayerScore;

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
}
