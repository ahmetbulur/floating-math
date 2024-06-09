using UnityEngine;
using UnityEngine.UI;

public class UpdateSceneLanguage : MonoBehaviour
{
    public Text[] textsToUpdate;
    public int sceneIndex;
    void Start()
    {
        UpdateLanguage(textsToUpdate, sceneIndex);
    }
    public void UpdateLanguage(Text[] textsToUpdate, int SceneIndex)
    {
        if (sceneIndex == 1)
        {
            if (PlayerPrefs.GetFloat("langCode") == 0)
            {
                textsToUpdate[0].text = "SELECT LEVEL";
                textsToUpdate[1].text = "Addition";
                textsToUpdate[2].text = "Subtraction";
                textsToUpdate[3].text = "Multiplication";
                textsToUpdate[4].text = "Division";
                textsToUpdate[5].text = "Mixed";
                textsToUpdate[6].text = "Endless";
            }
            else
            {
                textsToUpdate[0].text = "SEV�YE SE�";
                textsToUpdate[1].text = "Toplama";
                textsToUpdate[2].text = "��karma";
                textsToUpdate[3].text = "�arpma";
                textsToUpdate[4].text = "B�lme";
                textsToUpdate[5].text = "Kar���k";
                textsToUpdate[6].text = "Sonsuz";
            }
        }
        else if (sceneIndex == 2)
        {
            if (GameControl.SharedInstance.isGameEndless)
            {
                if (PlayerPrefs.GetFloat("langCode") == 0)
                    textsToUpdate[0].text = "SCORE MAXIMUM POINTS";
                else
                    textsToUpdate[0].text = "EN Y�KSEK PUANI TOPLAYIN";
            }
            else
            {
                if (PlayerPrefs.GetFloat("langCode") == 0)
                    textsToUpdate[0].text = "SCORE 500 POINTS";
                else
                    textsToUpdate[0].text = "500 PUAN TOPLAYIN";
            }
        }
        else if (sceneIndex == 3)
        {
            if (GameControl.SharedInstance.isGameEndless)
            {
                if (PlayerPrefs.GetFloat("langCode") == 0)
                {
                    textsToUpdate[0].text = "Your Score\n" + GameControl.SharedInstance.point;
                    textsToUpdate[1].text = "Highscore\n" + PlayerPrefs.GetInt("highscore");
                }
                else
                {
                    textsToUpdate[0].text = "Puan�n\n" + GameControl.SharedInstance.point;
                    textsToUpdate[1].text = "En Y�ksek Puan\n" + PlayerPrefs.GetInt("highscore");
                }
            }
            else
            {
                if (GameControl.SharedInstance.isLevelCompleted)
                {
                    float trueCount, falseCount, successRate;
                    trueCount = GameControl.SharedInstance.GetCountStats()[0];
                    falseCount = GameControl.SharedInstance.GetCountStats()[1];
                    successRate = GameControl.SharedInstance.GetCountStats()[2];

                    textsToUpdate[0].color = new Color32(119, 255, 0, 255);
                    if (PlayerPrefs.GetFloat("langCode") == 0)
                    {
                        textsToUpdate[0].text = "LEVEL COMPLETED";
                        textsToUpdate[1].text = "True: " + trueCount + "\nFalse: " + falseCount + "\nSuccess (%): " + successRate.ToString("0.");
                    }
                    else
                    {
                        textsToUpdate[0].text = "SEV�YE TAMAMLANDI";
                        textsToUpdate[1].text = "Do�ru: " + trueCount + "\nYanl��: " + falseCount + "\nBa�ar� (%): " + successRate.ToString("0.");
                    }

                }
                else
                {
                    textsToUpdate[0].color = new Color32(255, 0, 119, 255);
                    if (PlayerPrefs.GetFloat("langCode") == 0)
                    {
                        textsToUpdate[0].text = "LEVEL FAILED";
                        textsToUpdate[1].text = "TRY AGAIN";
                    }
                    else
                    {
                        textsToUpdate[0].text = "SEV�YE GE��LEMED�";
                        textsToUpdate[1].text = "TEKRAR DENEY�N";
                    }
                }
            }
        }
        else if (SceneIndex == 5)
        {
            if (PlayerPrefs.GetFloat("langCode") == 0)
                textsToUpdate[0].text = "Click true answer \nkeep sliding operations\n away from bottom\n\n"
                                  + "Practice and Enjoy!\n\n"
                                  + "Metbu Games  �  2021";
            else
                textsToUpdate[0].text = "Kayan i�lemleri \nalttan uzak tutmak i�in\ndo�ru yan�ta t�klay�n\n\n"
                                  + "Pratik Yap�n ve E�lenin!\n\n"
                                  + "Metbu Games  �  2021";
        }
    }
}
