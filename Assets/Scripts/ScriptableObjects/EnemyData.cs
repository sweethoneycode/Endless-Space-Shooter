using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies")]
public class EnemyData : ScriptableObject
{
    public string enenyName;
    public GameObject enemyModel;
    public int health = 5;
    public int maxHealth = 5;
    public float speed = 1;
    public int damage = 1;
    public int points = 5;
}
