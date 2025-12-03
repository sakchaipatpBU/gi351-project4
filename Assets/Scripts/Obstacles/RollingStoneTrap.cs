using System.Collections;
using UnityEngine;

public class RollingStoneTrap : MonoBehaviour
{
    public CameraController cameraController;

    public float shakingTime = 10f;
    public bool isActive = false;
    public GameObject stonePrefab;
    public Transform startPos;
    public Transform stopPos;
    public Transform deadPos;

    public Player player;

    private void Start()
    {
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SoundManager.Instance.PlaySFX("rolling1");
        {
            if (!isActive)
            {
                cameraController.StartShaking(shakingTime);
                StartCoroutine(CheckDeadAfterTrapDone());
                isActive = true;
                GameObject stone = Instantiate(stonePrefab, startPos.position, Quaternion.identity);
                DirectMove dir = stone.GetComponent<DirectMove>();
                dir.stopPos = stopPos.position;
                RollingStone rollingStone = stone.GetComponent<RollingStone>();
                rollingStone.rollingTime = shakingTime;
            }
        }
    }
    IEnumerator CheckDeadAfterTrapDone()
    {
        yield return new WaitForSeconds(shakingTime);
        if(player.transform.position.x < deadPos.position.x)
        {
            Debug.Log("< deadPos" + deadPos.position.x);
            GameManager.GetInstance().ShowLosePanel();
        }
    }

}
