using UnityEngine;

public class SetupVolumeOnStartup : MonoBehaviour
{
    void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);
    }
}
