using UnityEngine;

public class LevelController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform leverTransform;

    public Color color1;
    public Color color2;

    private void Start()
    {
        spriteRenderer.color = color1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = color2;
        Quaternion rotation = leverTransform.rotation;
        rotation.y = 180f;
    }
}
