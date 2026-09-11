using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{

    [SerializeField] private AudioSource buttonAudio;

    void Start()
    {
        
    }

    public void NewGame()
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
