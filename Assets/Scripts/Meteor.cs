using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Vector3 meteorScale;

    private void Awake()
    {
        float randomScale = Random.Range(0.5f, 1f);
        meteorScale = new Vector2(randomScale, randomScale);
        transform.localScale = meteorScale;
    }
}
