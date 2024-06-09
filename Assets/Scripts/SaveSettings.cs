using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveSettings : MonoBehaviour
{
    public Button turnOnMsc;
    public Button turnOffMsc;
    public Slider mscSlider;
    public Text mscText;

    public Button turnOnSFX;
    public Button turnOffSFX;
    public Slider sfxSlider;
    public Text sfxText;

    public Button [] langButtons; // EnglishBtn, TurkishBtn
    
    public AudioMixer masterMixer;

    void Start()
    {
        // Update music settings
        if (PlayerPrefs.HasKey("mscLevel"))
            mscSlider.value = PlayerPrefs.GetFloat("mscLevel");
        else
            PlayerPrefs.SetFloat("mscLevel", mscSlider.value);

        UpdateMusicVolume(mscSlider.value);

        turnOffMsc.onClick.AddListener(delegate
        {
            PlayerPrefs.SetFloat("tempMsc", mscSlider.value);
            mscSlider.value = 0f;
            
            GameObject.Find("TurnOffMusic").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOffMusic").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOnMusic").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOnMusic").GetComponent<Image>().enabled = true;
        });

        turnOnMsc.onClick.AddListener(delegate
        {
            mscSlider.value = PlayerPrefs.GetFloat("tempMsc");
           
            GameObject.Find("TurnOnMusic").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOnMusic").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOffMusic").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOffMusic").GetComponent<Image>().enabled = true;
        });

        mscSlider.onValueChanged.AddListener(delegate
        {
            UpdateMusicVolume(mscSlider.value);
        });

        // Update sfx settings
        if (PlayerPrefs.HasKey("sfxLevel"))
            sfxSlider.value = PlayerPrefs.GetFloat("sfxLevel");
        else
            PlayerPrefs.SetFloat("sfxLevel", sfxSlider.value);

        UpdateSFXVolume(sfxSlider.value);

        turnOffSFX.onClick.AddListener(delegate
        {
            PlayerPrefs.SetFloat("tempSfx", sfxSlider.value);
            sfxSlider.value = 0f;

            GameObject.Find("TurnOffSFX").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOffSFX").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOnSFX").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOnSFX").GetComponent<Image>().enabled = true;
        });

        turnOnSFX.onClick.AddListener(delegate
        {
            sfxSlider.value = PlayerPrefs.GetFloat("tempSfx");

            GameObject.Find("TurnOnSFX").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOnSFX").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOffSFX").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOffSFX").GetComponent<Image>().enabled = true;
        });

        sfxSlider.onValueChanged.AddListener(delegate
        {
            UpdateSFXVolume(sfxSlider.value);
        });

        // Update language settings
        if (!PlayerPrefs.HasKey("langCode"))
            PlayerPrefs.SetFloat("langCode", 0f);

        UpdateLanguage(PlayerPrefs.GetFloat("langCode"));

        langButtons[0].onClick.AddListener(delegate
        {
            PlayerPrefs.SetFloat("langCode", 1f);
            UpdateLanguage(PlayerPrefs.GetFloat("langCode"));
        });

        langButtons[1].onClick.AddListener(delegate
        {
            PlayerPrefs.SetFloat("langCode", 0f);
            UpdateLanguage(PlayerPrefs.GetFloat("langCode"));
        });
    }

    public void UpdateMusicVolume(float value)
    {
        masterMixer.SetFloat("musicVol", Mathf.Log(value) * 20);
        if (value == 0.001f)
        {
            GameObject.Find("TurnOffMusic").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOffMusic").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOnMusic").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOnMusic").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("TurnOnMusic").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOnMusic").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOffMusic").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOffMusic").GetComponent<Image>().enabled = true;
        }

        mscText.text = (int)(value * 200) + "";
        PlayerPrefs.SetFloat("mscLevel", value);
    }

    public void UpdateSFXVolume(float value)
    {
        masterMixer.SetFloat("sfxVol", Mathf.Log(value) * 20);
        if (value == 0.001f)
        {
            GameObject.Find("TurnOffSFX").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOffSFX").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOnSFX").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOnSFX").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("TurnOnSFX").GetComponent<Button>().interactable = false;
            GameObject.Find("TurnOnSFX").GetComponent<Image>().enabled = false;
            GameObject.Find("TurnOffSFX").GetComponent<Button>().interactable = true;
            GameObject.Find("TurnOffSFX").GetComponent<Image>().enabled = true;
        }

        sfxText.text = (int)(value * 200) + "";
        PlayerPrefs.SetFloat("sfxLevel", value);
    }

    public void UpdateLanguage(float langCode)
    {
        if (langCode == 0f)
        {
            langButtons[1].GetComponent<Button>().interactable = false;
            langButtons[1].GetComponent<Image>().enabled = false;
            langButtons[0].GetComponent<Button>().interactable = true;
            langButtons[0].GetComponent<Image>().enabled = true;

            GameObject.Find("MusicTitle").GetComponent<Text>().text = "MUSIC";
            GameObject.Find("SFXTitle").GetComponent<Text>().text = "SFX";
            GameObject.Find("LangTitle").GetComponent<Text>().text = "LANGUAGE";
        }
        else
        {
            langButtons[0].GetComponent<Button>().interactable = false;
            langButtons[0].GetComponent<Image>().enabled = false;
            langButtons[1].GetComponent<Button>().interactable = true;
            langButtons[1].GetComponent<Image>().enabled = true;

            GameObject.Find("MusicTitle").GetComponent<Text>().text = "MÜZÝK";
            GameObject.Find("SFXTitle").GetComponent<Text>().text = "EFEKT";
            GameObject.Find("LangTitle").GetComponent<Text>().text = "DÝL";
        }
    }
}
