using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class Skill : MonoBehaviour
{
    private Player player;

    public int skillCount;
    public TMP_Text skillCountText;

    public Image fillImg;
    public float cooldownTime;

    private Coroutine cooldownCoroutine;

    private void Start()
    {
        player = this.gameObject.GetComponent<Player>();
        UpdateSkillCountText();
    }
    public void UseSkill()
    {
        if(skillCount <= 0) // can Not use skill
        {
            GameManager.GetInstance().SkillDebug("You can not use skill");
            return;
        }

        SoundManager.Instance.PlaySFX("Pick_Item", 0.2f, 1); // Sound

        skillCount--;
        UpdateSkillCountText();
        fillImg.fillAmount = 0;
        if(cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cooldownCoroutine = StartCoroutine(CooldownUI());
    }

    IEnumerator CooldownUI()
    {
        player.isImmune = true;

        float elapsedTime = 0f; // ตัวแปรนับเวลาที่ผ่านไป
        while (elapsedTime < cooldownTime)
        {
            // เพิ่มเวลาที่ผ่านไปในแต่ละเฟรม
            elapsedTime += Time.deltaTime;

            // คำนวณค่า t (0 ถึง 1) 
            float t = elapsedTime / cooldownTime;
            float fill = 1 - t;
            fillImg.fillAmount = fill;

            yield return null;
        }

        player.isImmune = false;

    }

    public void AddSkillCount(int c = 1)
    {
        skillCount += c;
        UpdateSkillCountText();

    }

    public void UpdateSkillCountText()
    {
        skillCountText.text = skillCount.ToString();
    }
}
