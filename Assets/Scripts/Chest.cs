using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //public GameObject[] ChestList;

    public RectTransform chestUiInteract;
    private PlayerController playerController;
    private Skill skill;
    private Player player;

    public Animator animator;
    public TMP_Text rewardText;

    private bool canOpenChest = true;

    private void Start()
    {
        rewardText.gameObject.SetActive(false);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        skill = playerController.gameObject.GetComponent<Skill>();
        player = playerController.gameObject.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowChestUIInteract();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseChestUIInteract();
        }
    }

    public void ShowChestUIInteract()
    {
        if (!canOpenChest) return;

        playerController.chestNear = this;
        chestUiInteract.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        chestUiInteract.gameObject.SetActive(true);
    }
    public void CloseChestUIInteract()
    {
        playerController.chestNear = null;
        chestUiInteract.gameObject.SetActive(false);
    }

    public void OpenChest()
    {
        if (!canOpenChest) return;

        SoundManager.Instance.PlaySFX("Open_Crate", 0.2f, 1); // Sound

        rewardText.gameObject.SetActive(true);
        animator.SetBool("Open", true);
        int r = Random.Range(0, 3);
        switch(r)
        {
            case 0:
                rewardText.text = "You got 1 Hp";
                player.AddHp(1);
                break;
            case 1:
                rewardText.text = "You got 2 Hp";
                player.AddHp(2);
                break;
            case 2:
                rewardText.text = "You got 1 Armor Skill";
                skill.AddSkillCount(1);
                break;
        }
        StartCoroutine(RewardUICoroutine());
        canOpenChest = false;
        CloseChestUIInteract();

    }

    private IEnumerator RewardUICoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        SoundManager.Instance.PlaySFX("Pick_Item", 0.2f, 1); // Sound

        yield return new WaitForSeconds(1f);
        rewardText.gameObject.SetActive(false);

    }
}
