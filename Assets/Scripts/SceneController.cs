using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string mianScene = "MainMenu";
    public string lv1Name = "lvl1";
    public string lv2Name = "lvl2";
    public string lv3Name = "lvl3";

    public int mainIdx = 0;
    public int gameIdx = 1;

    #region sigleton
    private static SceneController instance;
    public static SceneController GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void LoadCurrentScene()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }
    public void LoadMainMenuScene()
    {
        SoundManager.Instance.PlaySFX("UI_Cilck", 0.2f, 1);

        SceneManager.LoadScene(mainIdx);
    }
    public void LoadLvl1Scene()
    {
        SoundManager.Instance.PlaySFX("UI_Cilck", 0.2f, 1);
        SceneManager.LoadScene(gameIdx);
    }
    public void LoadLvl2Scene()
    {
        SceneManager.LoadScene(lv2Name);
    }
    public void LoadLvl3Scene()
    {
        SceneManager.LoadScene(lv3Name);
    }

    public void ExitGame()
    {
        SoundManager.Instance.PlaySFX("UI_Cilck", 0.2f, 1);
        Application.Quit();
    }
}