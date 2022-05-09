using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObjects/Weapons")]
public class WeaponData : ScriptableObject
{
    public int fireDelay;
    public GameObject weapon;
    public float coolDown;
}
