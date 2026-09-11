using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text lifesText;
    public int score;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [SerializeField] private AudioSource loseAudio;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private AudioSource buttonAudio;

    private Color defaultLifesColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        defaultLifesColor = lifesText.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddLifes(int value)
    {
        lifesText.text = "Lifes: " + value;

        if (value == 1)
        {
            lifesText.color = Color.red;
        }
        else if (value == 6)
        {
            lifesText.color = Color.green;
            lifesText.text += "/6";
        }
        else
        {
            lifesText.color = defaultLifesColor;
        }

    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Points: " + score.ToString();
    }

    public void OpenGameOver()
    {
        gameOverPanel.SetActive(true);
        loseAudio.Play();

    }

    public void OpenWin()
    {
        winPanel.SetActive(true);
        winAudio.Play();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        buttonAudio.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        buttonAudio.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
        buttonAudio.Play();
    }
  
}
