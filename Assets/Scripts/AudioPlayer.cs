using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioMixer masterMixer;
    private static AudioPlayer instance = null;
    public static AudioPlayer Instance
    {
        get { return instance; }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("mscLevel"))
            PlayerPrefs.SetFloat("mscLevel", 0.5f);
        SetMusic(PlayerPrefs.GetFloat("mscLevel"));

        if (!PlayerPrefs.HasKey("sfxLevel"))
            PlayerPrefs.SetFloat("sfxLevel", 0.5f);
        SetSfx(PlayerPrefs.GetFloat("sfxLevel"));
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetMusic(float soundLevel)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log(soundLevel) * 20);
    }
    public void SetSfx(float soundLevel)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log(soundLevel) * 20);
    }
    public void PlaySfx(string sfxName)
    {
        GameObject.Find(sfxName).GetComponent<AudioSource>().Play();
    }
}
