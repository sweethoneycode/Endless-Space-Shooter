using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Awake()
    {
            SoundManager.Instance.PlayEnemySound(audioClip);
    }
}
