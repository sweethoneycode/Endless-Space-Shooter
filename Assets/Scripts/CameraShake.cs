using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.25f;
    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
      initialPosition = transform.position;
    }

    private void OnEnable()
    {
        EventBroker.PlayerHit += startShake;
        EventBroker.PlayerDeath += stopShake;
    }

    public void stopShake()
    {
        shakeDuration = 0;
        transform.position = initialPosition;
    }

    private void OnDisable()
    {
        EventBroker.PlayerHit -= startShake;
        EventBroker.PlayerDeath -= stopShake;
    }

    public void startShake()
    {
        StartCoroutine(ShakeCamera());
    }

IEnumerator ShakeCamera()
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
        
        yield return new WaitForSeconds(shakeDuration);
    }
}
