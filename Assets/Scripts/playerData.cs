using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData
{
    private static bool editCharacter;
    private static bool activeCharacter;

    public static bool EditCharacter
    {
        get
        {
            return editCharacter;
        }
        set
        {
            editCharacter = value;
        }
    }

    public static bool ActiveCharacter
    {
        get
        {
            return activeCharacter;
        }
        set
        {
            activeCharacter = value;
        }
    }

}
