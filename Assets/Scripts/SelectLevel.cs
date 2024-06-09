using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel SharedInstance;
    public Button[] allLevels;
    public Button endlessLevel;
    string mode, nextLvl;

    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        for (int i = 0; i < allLevels.Length; i++)
        {
            if (!PlayerPrefs.HasKey(allLevels[i].name))
            {
                if (allLevels[i].interactable)
                    PlayerPrefs.SetInt(allLevels[i].name, 1);
                else
                    PlayerPrefs.SetInt(allLevels[i].name, 0);
            }
            else
                allLevels[i].interactable = (PlayerPrefs.GetInt(allLevels[i].name) == 1);
        }

        for (int i = 0; i < allLevels.Length; i++)
        {
            int iCopy = i;
            allLevels[i].onClick.AddListener(delegate
            {
                AudioPlayer.Instance.PlaySfx("SFX");
                SetModeAndDifficulty(iCopy);
            });
        }
        endlessLevel.onClick.AddListener(delegate
        {
            AudioPlayer.Instance.PlaySfx("SFX");
            mode = "mixed-endless";
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScreen");
        });
    }
    void SetModeAndDifficulty(int iCopy)
    {
        PlayerPrefs.SetInt("diffCode", iCopy % 3 + 1);

        switch (iCopy)
        {
            case 0:
            case 1:
            case 2:
                mode = "+";
                break;
            case 3:
            case 4:
            case 5:
                mode = "-";
                break;
            case 6:
            case 7:
            case 8:
                mode = "x";
                break;
            case 9:
            case 10:
            case 11:
                mode = "/";
                break;
            default:
                break;
        }

        nextLvl = "Level" + (iCopy + 2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScreen");
    }
    public string GetGameMode()
    {
        return mode;
    }
    public string GetNextLevel()
    {
        return nextLvl;
    }
}
