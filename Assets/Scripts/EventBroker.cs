using System;

public class EventBroker
{
    public static event Action ProjectileOutOfBounds;
    public static event Action PlayerDeath;

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
}
