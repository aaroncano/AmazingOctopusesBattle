using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject endGamePanel = null;
    private bool canPause = true;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        GameIsPaused = false;
        canPause = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        if(canPause == true)
        {
            clicSound();
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    public void pauseGame()
    {
        if(canPause == true)
        {
            clicSound();
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            GameIsPaused = true;
        }
    }

    public void restartGame()
    {
        clicSound();

        GameManagement.inst.StopAllCoroutines(); //detener corutinas
        Time.timeScale = 1;

        Transitions transitions = FindObjectOfType<Transitions>();  //transition manager
        if (transitions != null) transitions.startTransition(1, 2);
    }

    public void goToMenu()
    {
        clicSound();
        Time.timeScale = 1f;

        FindObjectOfType<AudioManager>().StopPlaying("mainTheme"); //pausar musica
        Transitions transitions = FindObjectOfType<Transitions>();  //transition manager
        if (transitions != null) transitions.startTransition(0, 3);
    }

    public void clicSound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }

    //panel final
    public void startEndGameMenu()
    {
        canPause = false;
        StartCoroutine(waitForEndGame());
    }
    IEnumerator waitForEndGame()
    {
        yield return new WaitForSeconds(5.9f);
        Time.timeScale = 0f;
        endGamePanel.gameObject.SetActive(true);
    }

    public void goToTutorialPanel()
    {
        canPause = false;
        clicSound();
    }

    public void setCanPause(bool x) { canPause = x; }
}
