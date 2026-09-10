using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPlaying;

    [SerializeField] private AudioSource winAudio;


    [SerializeField]
    private float gameTime;

    [SerializeField]
    private TMP_Text gameTimeText;

    private UpdateUI uiScript;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        isPlaying = true;
        gameTime = 90;

        uiScript = FindFirstObjectByType<UpdateUI>();
        UpdateGameTimeText();
    }

    void Update()
    {
        if (isPlaying)
        {
            gameTime -= Time.deltaTime;

            if (gameTime <= 0)
            {
                gameTime = 0;
                isPlaying = false;
                uiScript.OpenWin();
                winAudio.Play();
            }

            UpdateGameTimeText();
        }
    }

    void UpdateGameTimeText()
    {
        int min = (int)gameTime / 60;
        int sec = (int)gameTime % 60;
        gameTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

}