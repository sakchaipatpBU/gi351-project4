using UnityEngine;

public class FallingStone : MonoBehaviour
{
    public GameObject stonePrefab;
    public GameObject shadowPrefab;
    public Transform startPos;
    public Transform[] stopPos;

    public float shadowGrowTime = 1f;
    public float shadowFinalSize = 0.6f; // control final size here
    public bool isFell = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFell)
        {
            isFell = true;

            for (int i = 0; i < stopPos.Length; i++)
            {
                StartCoroutine(SpawnStoneWithWarning(stopPos[i]));
            }
        }
    }

    private System.Collections.IEnumerator SpawnStoneWithWarning(Transform stop)
    {
        // Spawn stone
        GameObject stone = Instantiate(stonePrefab, startPos.position, Quaternion.identity);
        DirectMove d = stone.GetComponent<DirectMove>();
        d.stopPos = stop.position;
        SoundManager.Instance.PlaySFX("stoneFalling1");

        // Create shadow
        Vector3 finalDir = stop.position;
        finalDir.x += 0.3f;
        finalDir.y -= 0.2f;
        GameObject shadow = Instantiate(shadowPrefab, finalDir, Quaternion.identity);
        shadow.transform.localScale = Vector3.zero;

        // Grow shadow
        float t = 0f;
        while (t < shadowGrowTime)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(0f, shadowFinalSize, t / shadowGrowTime);
            shadow.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }

        
    }
}
