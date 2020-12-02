using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    [SerializeField]private Sprite musicOn;
    [SerializeField]private Sprite musicOff;
    private PlayerController player;

    private void Awake()
    {
        player = PlayerController.player;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("music") == "No" && gameObject.name == "Music")
        {
            GetComponent<Image>().sprite = musicOff;
        }
    }

    public void MusicWork()
    {
        if (PlayerPrefs.GetString("music") == "No")
        {
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("music", "Yes");
            GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            PlayerPrefs.SetString("music", "No");
            GetComponent<Image>().sprite = musicOff;
        }
    }

    public void RestartGame()
    {
        if (PlayerPrefs.GetString("music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }

        //player.SetLose();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
