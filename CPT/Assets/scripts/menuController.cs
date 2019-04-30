using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuController : MonoBehaviour
{
    private AudioSource menuMusic;

    // Start is called before the first frame update
    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
        menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //user clicks the quit button the game closes
    public void exitGame()
    {
        Application.Quit();
    }
    //player clicks the play button the game starts
    public void playGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //player goes back to the main menu from instructions
    public void backMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //player clicks to view the instructions
    public void instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
