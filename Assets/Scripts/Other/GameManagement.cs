using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagement : MonoBehaviour
{
    public static GameManagement inst;

    //puntuación
    [SerializeField] private int scoreP1 = 0;
    [SerializeField] private int scoreP2 = 0;
    [SerializeField] private TextMeshProUGUI winnerText = null;

    [SerializeField] private GameObject[] heartsP1 = null;
    [SerializeField] private GameObject[] heartsP2 = null;
    [SerializeField] private GameObject heartsCanvas = null;

    [SerializeField] private Color orange = Color.black;
    [SerializeField] private Color green = Color.black;

    private bool someoneWon;
    private bool thereAreHearts;

    private void Awake()
    {
        //singleton
        if (GameManagement.inst == null)
        {
            GameManagement.inst = this;
            DontDestroyOnLoad(gameObject);
            restartData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //añadir puntuación
    public void addScore(int index)
    {
        //se suma un punto al index contrario.
        if(someoneWon == false)
        {
            someoneWon = true;
            if (index == 1) //verde gana
            {
                scoreP2++;
                winnerText.color = green;
                winnerText.text = "VERDE GANA";
            }
            else if (index == 2) //naranja gana
            {
                scoreP1++;
                winnerText.color = orange;
                winnerText.text = "NARANJA GANA";
            }
            winnerText.gameObject.SetActive(true);
            changeScene();
        }
    }

    //cambiar de escena
    private void changeScene()
    {
        int numScenes = SceneManager.sceneCountInBuildSettings;
        if(numScenes-1 == SceneManager.GetActiveScene().buildIndex)
        {
            return;
        }
        else
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (numScenes - 1 > nextSceneIndex)
            {
                StartCoroutine(waitToChangeScene(nextSceneIndex));
            }
        }
    }
    IEnumerator waitToChangeScene(int nextSceneIndex)
    {
        yield return new WaitForSeconds(2.5f);
        winnerText.gameObject.SetActive(false);
        Transitions transitions = FindObjectOfType<Transitions>();  //transition manager
        if (transitions != null) transitions.startTransition(nextSceneIndex, 1);
    }

    //corazones UI
    public void loseHearts(int playerIndex, int maxHealth, int health, int numHeartToTake) // index-> 1=naranja, 2=verde
    {
        if (thereAreHearts)
        {
            int checkHealth = health - numHeartToTake;
            if (checkHealth > 0)
            {
                int skipHearts = maxHealth - health;

                if (playerIndex == 1)
                {
                    for (int i = skipHearts; i < skipHearts + numHeartToTake; i++)
                    {
                        heartsP1[i].SetActive(false);
                    }
                }
                else if (playerIndex == 2)
                {
                    for (int i = skipHearts; i < skipHearts + numHeartToTake; i++)
                    {
                        heartsP2[i].SetActive(false);
                    }
                }
                else return;
            }
            else if (checkHealth <= 0)
            {
                if (playerIndex == 1)
                {
                    foreach (GameObject h in heartsP1) h.SetActive(false);
                }
                else if (playerIndex == 2)
                {
                    foreach (GameObject h in heartsP2) h.SetActive(false);
                }
                else return;
            }
            else return;
        }
        else return;
    }

    //reestablecer datos
    public void restartData()
    {
        restartScore();
        restartHearts();
        restartWinnerText();
        showHearts();
    }
    public void restartWinnerText()
    {
        winnerText.gameObject.SetActive(false);
        someoneWon = false;
    }
    public void restartScore()
    {
        scoreP1 = 0;
        scoreP2 = 0;
    }
    public void restartHearts()
    {
        foreach (GameObject h in heartsP1) h.SetActive(true);
        foreach (GameObject h in heartsP2) h.SetActive(true);
    }

    //destruir singleton
    public void destroyGameManagement()
    {
        Destroy(gameObject);
    }

    //quitar y poner corazones
    public void hideHearts()
    {
        thereAreHearts = false;
        heartsCanvas.gameObject.SetActive(false);
    }
    public void showHearts()
    {
        thereAreHearts = true;
        heartsCanvas.gameObject.SetActive(true);
    }

    //getters y setters
    public int getWinner()
    {
        if (scoreP1 > scoreP2) return 1;
        else if (scoreP2 > scoreP1) return 2;
        else return 0;
    }
    public int getScoreP1() { return scoreP1; }
    public int getScoreP2() { return scoreP2; }
    public Color getOrange() { return orange; }
    public Color getGreen() { return green; }

    public void setSomeoneWon(bool sw) { someoneWon = sw; }
}
