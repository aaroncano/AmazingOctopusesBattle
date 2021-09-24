using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim = null;
    [SerializeField] private float transitionTime = 0f;

    public void startTransition(int sceneIndex, int extraCode = 0) => StartCoroutine(waitForTransition(sceneIndex, extraCode));
    IEnumerator waitForTransition(int sceneIndex, int extraCode)
    {
        transitionAnim.SetTrigger("startTransition");
        FindObjectOfType<AudioManager>().Play("endScene");
        PauseMenu pm = null;    //dejar de permitir pausar el juego mientras se realiza una transición
        if (FindObjectOfType<GameManagement>() != null)
        {
            pm = GameManagement.inst.GetComponentInChildren<PauseMenu>();
            if (pm != null) pm.setCanPause(false);
        }

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
        FindObjectOfType<AudioManager>().Play("startScene");
        if (sceneIndex == 10)
        {
            FindObjectOfType<AudioManager>().StopPlaying("mainTheme");
            FindObjectOfType<AudioManager>().Play("endTheme"); //si la escena es la final pon musica de fiesta
        }
        switch (extraCode)
        {
            case 1:
                gameManagementT(pm);
                break;
            case 2:
                pauseMenuT_Restart(pm, sceneIndex);
                break;
            case 3:
                pauseMenuT_GoToMenu();
                break;
            case 4:
                mainMenuT();
                break;
            default:
                break;
        }
    }

    public void gameManagementT(PauseMenu pm) // op 1
    {
        GameManagement.inst.restartHearts();
        GameManagement.inst.setSomeoneWon(false);
        if (pm != null) pm.setCanPause(true);
    }

    public void pauseMenuT_Restart(PauseMenu pm, int sceneIndex) // op 2
    {
        if (pm != null)
        {
            GameManagement.inst.restartData(); //restablecer datos
            pm.setCanPause(true);
        }
        FindObjectOfType<AudioManager>().Play("mainTheme");
    }
    public void pauseMenuT_GoToMenu() // op 3
    {
        GameManagement.inst.destroyGameManagement();
        FindObjectOfType<AudioManager>().Play("menuTheme");
    }
    public void mainMenuT()
    {
        FindObjectOfType<AudioManager>().Play("mainTheme");
    }
}
