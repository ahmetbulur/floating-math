using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl SharedInstance;

    public GameObject choices, speedIcons;
    Button[] chButtons;
    Image[] chImages, spImages;
    Text[] chTexts;
    public Text levelInfo, score, tempScore;
    Text[] texts;
    
    Queue<GameObject> activeOperations;
    GameObject operation;
    Vector2 spawnPos, choicePos, lastPos;

    string[] operators;
    string operat;
    public int point;
    int num1, num2;
    float slidingSpeed, result, trueCount, falseCount, successRate;
    public bool isLevelCompleted, isGameEndless;
    
    void Awake()
    {
        SharedInstance = this;
    }
    
    void Start()
    {
        operators = new string[] { "+", "-", "x", "/" };
        activeOperations = new Queue<GameObject>();

        isLevelCompleted = false;

        if (SelectLevel.SharedInstance.GetGameMode().Equals("mixed-endless"))
            isGameEndless = true;
        
        spawnPos = new Vector2(0, 1200);
        choicePos = GameObject.Find("Choices").GetComponent<RectTransform>().anchoredPosition;

        chImages = choices.GetComponentsInChildren<Image>();
        chTexts = choices.GetComponentsInChildren<Text>();
        chButtons = choices.GetComponentsInChildren<Button>();
        spImages = speedIcons.GetComponentsInChildren<Image>();

        slidingSpeed = 220;

        point = 0;
        score.text = point.ToString();

        StartCoroutine(PopUpLevelInfo(1.5f));

        for (int i = 0; i < chButtons.Length; i++)
        {
            int index = i;
            chButtons[i].onClick.AddListener(() => { CheckAnswer(index); });
        }
    }
    int tmpCount = 0;
    void Update()
    {
        if (tmpCount == 0 && slidingSpeed > 270 && slidingSpeed <= 320)
        {
            spImages[0].enabled = false;
            spImages[1].enabled = true;
            tmpCount++;
        }
        else if (tmpCount == 1 && slidingSpeed > 320)
        {
            spImages[1].enabled = false;
            spImages[2].enabled = true;
            tmpCount++;
        }
        if (isGameEndless)
        {
            if (activeOperations.Count != 0 && activeOperations.Peek().GetComponent<RectTransform>().anchoredPosition.y <= choicePos.y)
            {
                if (point > PlayerPrefs.GetInt("highscore"))
                    PlayerPrefs.SetInt("highscore", point);
                SceneManager.LoadScene("GameOver");
            }
            if (activeOperations.Count == 0 || spawnPos.y - lastPos.y >= 400f)
            {
                if (slidingSpeed <= 365)
                    slidingSpeed += 5;
                operation = ObjectPool.SharedInstance.GetPooledObject();
                if (operation != null)
                {
                    operation.GetComponent<RectTransform>().anchoredPosition = spawnPos;
                    operation.SetActive(true);
                }
                activeOperations.Enqueue(operation);
                CreateOperation(operation);
                if (activeOperations.Count == 1)
                    CreateChoices();
            }
            lastPos = operation.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            if (point < 500)
            {
                if (activeOperations.Count != 0 && activeOperations.Peek().GetComponent<RectTransform>().anchoredPosition.y <= choicePos.y)
                {
                    SceneManager.LoadScene("GameOver");
                }
                if (activeOperations.Count == 0 || spawnPos.y - lastPos.y >= 400f)
                {
                    if (slidingSpeed <= 365)
                        slidingSpeed += 5;
                    operation = ObjectPool.SharedInstance.GetPooledObject();
                    if (operation != null)
                    {
                        operation.GetComponent<RectTransform>().anchoredPosition = spawnPos;
                        operation.SetActive(true);
                    }
                    activeOperations.Enqueue(operation);
                    CreateOperation(operation);
                    if (activeOperations.Count == 1)
                        CreateChoices();
                }
                lastPos = operation.GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                isLevelCompleted = true;
                while (activeOperations.Count > 0)
                    DestroyOperation();
                PlayerPrefs.SetInt(SelectLevel.SharedInstance.GetNextLevel(), 1);
                for (int i = 0; i < chTexts.Length; i++)
                {
                    chTexts[i].text = "";
                }
                successRate = trueCount / (trueCount + falseCount) * 100;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    public void DestroyOperation()
    {
        activeOperations.Peek().SetActive(false);
        activeOperations.Dequeue();
    }
    void CreateOperation(GameObject operation)
    {
        if (SelectLevel.SharedInstance.GetGameMode().Equals("mixed-endless"))
        {
            int difficultyLevel = Random.Range(1, 11);
            if (difficultyLevel <= 5) //50% chance
                PlayerPrefs.SetInt("diffCode", 1);
            else if (difficultyLevel <= 9) //40% chance
                PlayerPrefs.SetInt("diffCode", 2);
            else //10% chance
                PlayerPrefs.SetInt("diffCode", 3);

            operat = operators[Random.Range(0, operators.Length)];
        }
        else
            operat = SelectLevel.SharedInstance.GetGameMode();

        if (operat.Equals("x"))
        {
            if (PlayerPrefs.GetInt("diffCode") == 1)
            {
                num1 = Random.Range(1, 11);
                num2 = Random.Range(1, 11);
            }
            else if (PlayerPrefs.GetInt("diffCode") == 2)
            {
                num1 = Random.Range(11, 16);
                num2 = Random.Range(9, 17);
            }
            else
            {
                num1 = Random.Range(16, 21);
                num2 = Random.Range(15, 21);
            }
        }
        else if (operat.Equals("/"))
        {
            if (PlayerPrefs.GetInt("diffCode") == 1)
            {
                num1 = Random.Range(1, 11);
                num2 = Random.Range(1, 11);
            }
            else if (PlayerPrefs.GetInt("diffCode") == 2)
            {
                int rnd = Random.Range(1, 4);
                if(rnd == 1)
                {
                    num1 = Random.Range(5, 11);
                    num2 = Random.Range(20, 25);
                }
                else if (rnd == 2)
                {
                    num1 = Random.Range(11, 16);
                    num2 = Random.Range(9, 17);
                }
                else
                {
                    num1 = Random.Range(16, 21);
                    num2 = Random.Range(6, 13);
                }
            }
            else
            {
                int rnd = Random.Range(1, 4);
                if (rnd == 1)
                {
                    num1 = Random.Range(21, 26);
                    num2 = Random.Range(12, 17);
                }
                else if (rnd == 2)
                {
                    num1 = Random.Range(25, 30);
                    num2 = Random.Range(10, 15);
                }
                else
                {
                    num1 = Random.Range(31, 37);
                    num2 = Random.Range(8, 12);
                }
            }
            num1 *= num2;
        }
        else
        {
            if (PlayerPrefs.GetInt("diffCode") == 1)
            {
                num1 = Random.Range(1, 11);
                num2 = Random.Range(1, 11);
            }
            else if (PlayerPrefs.GetInt("diffCode") == 2)
            {
                num1 = Random.Range(10, 51);
                num2 = Random.Range(10, 51);
            }
            else
            {
                num1 = Random.Range(50, 101);
                num2 = Random.Range(50, 101);
            }
        }
        
        texts = operation.GetComponentsInChildren<Text>();
        texts[0].text = num1.ToString();
        texts[1].text = operat;
        texts[2].text = num2.ToString();
    }
    void CreateChoices()
    {
        if (activeOperations.Count != 0)
        {
            texts = activeOperations.Peek().GetComponentsInChildren<Text>();
            
            num1 = int.Parse(texts[0].text);
            operat = texts[1].text;
            num2 = int.Parse(texts[2].text);

            switch (operat)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "x":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    break;
            }

            float wrongRes1, wrongRes2;

            if(result < 0)
            {
                wrongRes1 = Mathf.Ceil(result * 9 / 10);
                wrongRes2 = Mathf.Floor(result * 11 / 10);
            }
            else if(result == 0)
            {
                wrongRes1 = 0 - Random.Range(1,10);
                wrongRes2 = 0 + Random.Range(1, 10);
            }
            else if(result < 10)
            {
                wrongRes1 = Mathf.Floor(result * 9 / 10);
                wrongRes2 = Mathf.Ceil(result * 11 / 10);
            }
            else if (result < 200)
            {
                wrongRes1 = result - 10;
                wrongRes2 = result + 10;
            }
            else
            {
                wrongRes1 = result - 30;
                wrongRes2 = result + 30;
            }

            int rndChoice = Random.Range(1, 4);
            if (rndChoice == 1)
            {
                chTexts[0].text = result.ToString();
                chTexts[1].text = wrongRes1.ToString();
                chTexts[2].text = wrongRes2.ToString();
            }
            else if (rndChoice == 2)
            {
                chTexts[1].text = result.ToString();
                chTexts[2].text = wrongRes1.ToString();
                chTexts[0].text = wrongRes2.ToString();
            }
            else
            {
                chTexts[2].text = result.ToString();
                chTexts[0].text = wrongRes1.ToString();
                chTexts[1].text = wrongRes2.ToString();
            }
        }
        else
        {
            for (int i = 0; i < chTexts.Length; i++)
            {
                chTexts[i].text = "";
            }
        }
    }
    void CheckAnswer(int index)
    {
        if (result == int.Parse(chTexts[index].text))
        {
            DestroyOperation();
            AudioPlayer.Instance.PlaySfx("sfxTrue");
            
            StartCoroutine(ChangeColor(index, new Color32(119, 255, 0, 255), 0.217f));
            StartCoroutine(UpdateScore(true, 0.05f));

            CreateChoices();

            trueCount++;
        }
        else
        {
            AudioPlayer.Instance.PlaySfx("sfxWrong");
            
            StartCoroutine(ChangeColor(index, new Color32(255, 0, 119, 255), 0.217f));
            StartCoroutine(UpdateScore(false, 0.05f));

            if(slidingSpeed <= 360)
                slidingSpeed += 10;
            
            falseCount++;
        }
    }
    public IEnumerator ChangeColor(int index, Color color, float delay)
    {
        chImages[index].color = color;
        chTexts[index].color = Color.white;
        yield return new WaitForSeconds(delay);
        chImages[index].color = Color.white;
        chTexts[index].color = Color.black;
    }
    public IEnumerator UpdateScore(bool isTrue, float delay)
    {
        tempScore.enabled = true;
        int tmpPoint = point;
        if (isTrue)
            point += 10;
        else
            point -= 10;
        for (int i = 10; i >= 1; i--)
        {
            if (isTrue)
            {
                tmpPoint++;
                tempScore.color = new Color32(119, 255, 0, 255);
                tempScore.text = "+" + i;
            }
            else
            {
                tmpPoint--;
                tempScore.color = new Color32(255, 0, 119, 255);
                tempScore.text = "-" + i;
            }
            score.text = tmpPoint + "";
            yield return new WaitForSeconds(delay);
        }
        tempScore.text = "";
        tempScore.enabled = false;
    }
    public IEnumerator PopUpLevelInfo(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelInfo.enabled = false;
    }
    public float GetSlidingSpeed()
    {
        return slidingSpeed;
    }
    public float [] GetCountStats()
    {
        float[] counts = { trueCount, falseCount, successRate};
        return counts;
    }
}
