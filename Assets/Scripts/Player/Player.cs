using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController controller;

    public bool isImmune = false;

    public int hp = 3;

    #region singletonn
    private static Player instance;
    public static Player GetInstance()
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
        gameManager = GameManager.GetInstance();
        controller = gameObject.GetComponent<PlayerController>();
    }
    public void AddHp(int p = 1)
    {
        if (hp <= 0) return;
        hp += p;
        gameManager.AddHpUI(p);
    }
    public void ReduceHp(int p = 1)
    {
        if (hp <= 0 || isImmune) return;
        hp -= p;
        gameManager.RemoveHpUI(p);
    }

    public void GameOver()
    {
        controller.enabled = false;
    }

}
