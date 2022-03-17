using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider SoundLevel;
    float currentSoundLevel;

    // Start is called before the first frame update
    void Start()
    {
        SoundLevel.value = SaveManager.instance.activeSave.soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        currentSoundLevel = SoundLevel.value;
    }

    public void QuitSettings()
    {
        SaveSettings();
        SceneManager.LoadSceneAsync("Start");
    }

    private void SaveSettings()
    {
            currentSoundLevel = SoundLevel.value;
            

            SaveManager.instance.activeSave.soundVolume = currentSoundLevel;
            SaveManager.instance.Save();

    }
}
