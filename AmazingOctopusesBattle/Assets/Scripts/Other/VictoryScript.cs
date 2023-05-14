using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScript : MonoBehaviour
{
    [SerializeField] private GameObject[] players = null; //[0] = player1 [1] = player2
    [SerializeField] private Transform[] spawnPoints = null; //[0] = winner [1] == loser

    [SerializeField] private GameObject[] dummies = null; //[0] = player 1 [1] = player 2
    [SerializeField] private Transform[] winnerDummiesSpawnPoints = null;
    [SerializeField] private Transform[] loserDummiesSpawnPoints = null;

    [SerializeField] private TextMeshProUGUI winnerText = null;

    private void Awake()
    {
        GameManagement.inst.hideHearts();
        if (GameManagement.inst.getWinner() == 1) //naranja
        {
            Instantiate(players[0], spawnPoints[0].position, Quaternion.identity);
            Instantiate(players[1], spawnPoints[1].position, Quaternion.identity);

            foreach(Transform w in winnerDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[0], w.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.activateGun();
                dA.changeRotation();
                dA.changeAnimToIdle();
            }
            foreach(Transform l in loserDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[1], l.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.isLoser = true;
                dA.changeRotation();
                dA.changeAnimToDead();
            }
        }
        else if(GameManagement.inst.getWinner() == 2) //verde
        {
            Instantiate(players[1], spawnPoints[0].position, Quaternion.identity);
            Instantiate(players[0], spawnPoints[1].position, Quaternion.identity);

            foreach (Transform w in winnerDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[1], w.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.activateGun();
                dA.changeRotation();
                dA.changeAnimToIdle();
            }
            foreach (Transform l in loserDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[0], l.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.isLoser = true;
                dA.changeRotation();
                dA.changeAnimToDead();
            }
        }
        else
        {
            Instantiate(players[0], spawnPoints[0].position, Quaternion.identity);
            Instantiate(players[1], spawnPoints[1].position, Quaternion.identity);

            foreach (Transform w in winnerDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[0], w.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.activateGun();
                dA.changeRotation();
                dA.changeAnimToIdle();
            }
            foreach (Transform l in loserDummiesSpawnPoints)
            {
                GameObject dummy = Instantiate(dummies[1], l.position, Quaternion.identity);
                DummyAnimations dA = dummy.GetComponent<DummyAnimations>();
                dA.isLoser = true;
                dA.changeRotation();
                dA.changeAnimToDead();
            }
        }

    }
    private void Start()
    {   
        if (GameManagement.inst.getWinner() == 1)
        {
            winnerText.color = GameManagement.inst.getOrange();
            winnerText.text = "NARANJA HA GANADO";
        }
        else if (GameManagement.inst.getWinner() == 2)
        {
            winnerText.color = GameManagement.inst.getGreen();
            winnerText.text = "VERDE HA GANADO";
        }
        else
        {
            winnerText.color = Color.white;
            winnerText.text = "EMPATE";
        }
        winnerText.gameObject.SetActive(true);

        GameManagement.inst.GetComponentInChildren<PauseMenu>().startEndGameMenu();
    }
}
