using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    //variables initialized
    // bool isPaused = false;
    public Text pauseText, resumeText, quitText, timeText;
    public Button resume, quit;
    public static float timer = 900.0f;
    private static Animator anim;
    public GameObject player, healthBar, healthBack;
    public static AudioSource myLoop;
   
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        //when playing again resets the time
        if (sceneName == "Level")
        {
            timer = 900.0f;
        }
        //sets the pause game canvas to invisible
        pauseText.enabled = false;
        resumeText.enabled = false;
        quitText.enabled = false;
        resume.image.enabled = false;
        resume.interactable = false;
        quit.image.enabled = false;
        quit.interactable = false;
        anim = player.GetComponent<Animator>();
        myLoop = GetComponent<AudioSource>();
        myLoop.Play();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //when user presses escapes show the pause menu
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseText.enabled = true;
            resumeText.enabled = true;
            quitText.enabled = true;
            resume.image.enabled = true;
            resume.interactable = true;
            quit.image.enabled = true;
            quit.interactable = true;
            Time.timeScale = 0;
            // isPaused = true;
        }
        //has a countdown timer of 15 mins
        if (timer > 0)
        {
            StartCoroutine(CountDownTime());
        }
        //when time runs out game over
        else
        {
            timer = 0;
            anim.SetBool("isDead", true);
            GameOver();
            StartCoroutine(delayDissappear());
        }
        //if player loses all his health he dies
        if (playerHealth.health <= 0)
        {
            anim.SetBool("isDead", true);
            GameOver();
            StartCoroutine(delayDissappear());
        }
        if(BossMovement.isDead == true)
        {
            timer = 900.0f;
        }
        /*else if(isPaused && Input.GetKey(KeyCode.Escape))
        {
            pauseText.enabled = false;
            resumeText.enabled = false;
            quitText.enabled = false;
            resume.image.enabled = false;
            resume.interactable = false;
            quit.image.enabled = false;
            quit.interactable = false;
            Time.timeScale = 1f;
            isPaused = false;
        }*/
    }
    //funtion to resume the game after being paused
    public void resumeGame()
    {
        pauseText.enabled = false;
        resumeText.enabled = false;
        quitText.enabled = false;
        resume.image.enabled = false;
        resume.interactable = false;
        quit.image.enabled = false;
        quit.interactable = false;
        Time.timeScale = 1f;
        //isPaused = false;
    }
    //function when user clicks quit to end the game
    public void quitGame()
    {
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }
    //counts down the timer every second
    IEnumerator CountDownTime()
    {
        timer -= Time.deltaTime;
        timeFormat();
        yield return new WaitForSeconds(1);
    }
    //formats the timer into minutes and seconds
    void timeFormat()
    {
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        timeText.text = min.ToString("00") + ":" + sec.ToString("00");
    }
    //used to end the game
    public void GameOver()
    {
        //playerHealth.gameOver = true;
        playerFire.gameOver = true;
        PlatformerCharacter2D.gameOver = true;
        Destroy(healthBar);
        Destroy(healthBack);
    }
    public void WinGame()
    {
        playerFire.gameOver = true;
        PlatformerCharacter2D.gameOver = true;
        Destroy(healthBar);
        Destroy(healthBack);
        SceneManager.LoadScene("WinScreen");
    }

    //delays the game ending for the death animation
    IEnumerator delayDissappear()
    {

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }
}

