using UnityEngine;

public class TutorialUi : MonoBehaviour
{
    [SerializeField]
    private int tutorial;
    public GameObject tutorialPanel;
    void Start()
    {
        tutorial = PlayerPrefs.GetInt("Tutorial");
        if(tutorial == 0)
        {
            tutorialPanel.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            tutorialPanel.SetActive(false);

        }
    }

    public void ReTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialPanel.SetActive(false);

        }
    }
}
