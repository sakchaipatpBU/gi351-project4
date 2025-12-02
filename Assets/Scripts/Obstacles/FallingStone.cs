using UnityEngine;

public class FallingStone : MonoBehaviour
{

    public GameObject stonePrefab;
    public Transform startPos;

    public Transform[] stopPos;

    public bool isFell = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFell)
        {
            for(int i = 0; i < stopPos.Length; i++)
            {
                GameObject s = Instantiate(stonePrefab, startPos.position, Quaternion.identity);
                DirectMove d = s.GetComponent<DirectMove>();
                d.stopPos = stopPos[i].position;
            }
            isFell = true;
        }
    }

}
