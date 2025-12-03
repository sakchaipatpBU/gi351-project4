using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController controller;

    public bool isImmune = false;
    public int hp = 3;

    private SpriteRenderer sprite;     // <-- add this
    private Color originalColor;

    Coroutine immuneAfterHit;

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

        sprite = GetComponent<SpriteRenderer>();   // <-- cache sprite
        originalColor = sprite.color;
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

        // Flash red
        StartCoroutine(FlashRed());
        if(immuneAfterHit != null)
        {
            StopCoroutine(immuneAfterHit);
        }
        immuneAfterHit = StartCoroutine(ImmuneAfterHit());
    }

    private System.Collections.IEnumerator FlashRed()
    {
        sprite.color = Color.red;        // turn red
        yield return new WaitForSeconds(0.1f);   // flash time
        sprite.color = originalColor;    // return to normal
    }

    public void GameOver()
    {
        controller.enabled = false;
    }

    IEnumerator ImmuneAfterHit()
    {
        if (isImmune)
        {
            yield break;
        }
        isImmune = true;
        yield return new WaitForSeconds(0.5f);
        isImmune = false;
    }
}
