using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadScene(int index)
    {
        AudioPlayer.Instance.PlaySfx("SFX");
        SceneManager.LoadScene(index);    
    }
    public void ExitApplication()
    {
        AudioPlayer.Instance.PlaySfx("SFX");
        Application.Quit();
    }
}
