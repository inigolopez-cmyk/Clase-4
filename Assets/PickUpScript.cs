using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] private int amountHealth = 1;
    [SerializeField] private AudioSource pickUpAudio;

    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float blinkTime = 2f;      
    [SerializeField] private float blinkInterval = 0.15f; 

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Destroy(this.gameObject, lifetime);
        Invoke(nameof(StartBlinking), Mathf.Max(0, lifetime - blinkTime));
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 45, 0);
    }

    private void StartBlinking()
    {
        InvokeRepeating(nameof(ToggleSprite), 0f, blinkInterval);
    }

    private void ToggleSprite()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerScript>().AddHealth(amountHealth);
            pickUpAudio.Play();

            CancelInvoke(); 

            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(this.gameObject, pickUpAudio.clip.length);
        }
    }
}