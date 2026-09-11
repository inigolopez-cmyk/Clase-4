using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject player;
    UpdateUI uiScript;

    [SerializeField] private GameObject pickupPrefab;
    [Range(0f, 1f)][SerializeField] private float dropChance = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime);
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            uiScript.AddScore(10);
            gameObject.SetActive(false);

            if (Random.value <= dropChance)
            {
                Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            }

            collision.gameObject.SetActive(false);
        }
    }
}
