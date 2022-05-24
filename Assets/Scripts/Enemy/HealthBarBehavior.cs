using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 Offset;
    [SerializeField] ParticleSystem ParticleSystem;
    private bool restoreHealth = false;
    private bool updateHealth = false;
    public bool fixedPosition = false;

    public void SetHealth(float health, float maxHealth)
    {
        if (!fixedPosition)
        {
            slider.gameObject.SetActive(health < maxHealth);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
        

        slider.value = health;
        slider.maxValue = maxHealth;
    }

    public void RestoreHealth()
    {

        StartCoroutine(energyParticles());
    }

    private IEnumerator energyParticles()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        ParticleSystem.Play();
        yield return wait;
        ParticleSystem.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if(!fixedPosition)
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);

        if (restoreHealth)
        {
            slider.gameObject.SetActive(updateHealth);

            if (slider.value < 10)
            {
                slider.value += 5f * Time.deltaTime;
                //if (!ParticleSystem.isPlaying && updateHealth)
                //    ParticleSystem.Play();

            }
            else
            {
                //ParticleSystem.Stop();
                restoreHealth = false;
            }
        }
    }
}
