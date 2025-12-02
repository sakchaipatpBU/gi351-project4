using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public Player player;
    public PlayerController playerController;

    public GameObject winPanel;
    public GameObject losePanel;
    public bool isGameOver;

    [Header("Hp UI")]
    public RectTransform hpPanel;
    public GameObject hpPrefab;
    private List<GameObject> listHp = new List<GameObject>();


    [Header("Timer UI")]
    public RectTransform timerPanel;
    public TMP_Text timerText;
    public float time;
    private Coroutine timerInRoomCoroutinne;


    [Header("Skill UI")]
    public TMP_Text skillDebug;
    private Coroutine SkillDebugCoroutinne;



    #region singleton
    private static GameManager instance;
    public static GameManager GetInstance()
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

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (playerController == null)
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        if(player != null)
        {
            int hp = player.hp;
            for(int i = 0; i < hp; i++)
            {
                GameObject hpObj = CreateHpUI();
                listHp.Add(hpObj);
            }
        }
        UpdateHpUIPos();

        // time close when start
        timerPanel.gameObject.SetActive(false);

        // skill ui
        skillDebug.gameObject.SetActive(false);

        CloseWinPanel();
        CloseLosePanel();
    }
    public void ShowWinPanel()
    {
        SoundManager.Instance.PlaySFX("WIN", 0.2f, 1); // Sound

        winPanel.SetActive(true);
        player.GameOver();

    }
    public void CloseWinPanel()
    {
        winPanel.SetActive(false);
    }
    public void CloseLosePanel()
    {
        losePanel.SetActive(false);
    }
    public void ShowLosePanel()
    {
        if (isGameOver) return;
        SoundManager.Instance.PlaySFX("LOSE", 0.2f, 1); // Sound


        losePanel.SetActive(true);
        player.GameOver();
        isGameOver = true;
    }


    #region SceneManagement

    public void LoadCurrentScene()
    {
        // sound click

        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }
    /*public void LoadMainMenuScene()
    {
        SoundManager.Instance.PlaySFX("UI_Cilck");

        SceneManager.LoadScene(mianScene);
    }*/
    public void ExitGame()
    {
        // sound click
        Application.Quit();
    }
    

    #endregion

    #region Hp UI
    public GameObject CreateHpUI()
    {
        GameObject hp = Instantiate(hpPrefab, hpPanel);
        return hp;
    }
    public void UpdateHpUIPos()
    {
        for(int i = 0; i < listHp.Count; i++)
        {
            float y = listHp[i].transform.position.y;
            float z = listHp[i].transform.position.z;
            listHp[i].transform.position = new Vector3(120 * (i+1), y, z);
        }
    }
    public void AddHpUI(int hpAdd = 1)
    {
        for (int i = 0; i < hpAdd; i++)
        {
            GameObject lastHpObj = CreateHpUI();
            listHp.Add(lastHpObj);
        }

        UpdateHpUIPos();
    }
    public void RemoveHpUI(int hpRemove = 1)
    {
        for (int i = 0; i < hpRemove; i++)
        {
            int last = listHp.Count - 1;
            GameObject lastHpObj = listHp[last];
            listHp.RemoveAt(last);
            Destroy(lastHpObj);

            if(player.hp <= 0)
            {
                ShowLosePanel();
                break;
            }
        }

        UpdateHpUIPos();
    }


    #endregion

    #region timer in room
    public void StartTimer(float t)
    {
        time = t;
        StopTimerInRoomCoroutine();
        timerPanel.gameObject.SetActive(true);
        timerInRoomCoroutinne = StartCoroutine(TimerInRoomCoroutine());
    }
    public void StopTimerInRoomCoroutine()
    {
        if (timerInRoomCoroutinne != null)
        {
            StopCoroutine(timerInRoomCoroutinne);
            timerPanel.gameObject.SetActive(false);
        }
    }
    IEnumerator TimerInRoomCoroutine()
    {
        int timeInt;
        while(time >= 0)
        {
            time -= Time.deltaTime;
            if(time >= 10)
            {
                timeInt = (int)time;
                timerText.text = timeInt.ToString();
            }
            else
            {
                timerText.text = time.ToString("0.00");
            }
            yield return null;
        }

        ShowLosePanel();
    }

    #endregion

    #region skill ui
    public void SkillDebug(string d)
    {
        skillDebug.text = d;
        if (SkillDebugCoroutinne != null)
        {
            StopCoroutine(SkillDebugCoroutinne);
        }
        SkillDebugCoroutinne = StartCoroutine(ShowSkillDebugCoroutinne());
    }
    IEnumerator ShowSkillDebugCoroutinne()
    {
        skillDebug.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        skillDebug.gameObject.SetActive(false);
    }
    #endregion
}
