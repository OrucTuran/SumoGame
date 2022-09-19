using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreOrReload : MonoBehaviour
{
    public UIManager UIManager;
    public PauseUI PauseUI;

    private int counter = 0;
    [SerializeField] private float time = 30;
    private bool isGameDone;

    [SerializeField] private int numberOfWin;
    [SerializeField] private GameObject player;

    [SerializeField] private Text counterText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text timeText;


    private void Update()
    {
        if (isGameDone == false)
        {
            time -= Time.deltaTime;
        }


        if (time > 0)
        {
            timeText.text = "Time:" + time.ToString("0");
        }

        if (time <= 0)
        {
            gameOverText.text = "TIME IS UP!";
            player.GetComponent<Rigidbody>().isKinematic = true;
            PauseUI.Show();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            counter++;
            counterText.text = "Score:" + counter;

            if (counter == numberOfWin)
            {
                gameOverText.text = "YOU WIN THE GAME!";
                isGameDone = true;
                player.GetComponent<Rigidbody>().isKinematic = true;
                Invoke(nameof(LoadScene), 3f);


            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameOverText.text = "DEFEAT!";

            var enemies = FindObjectsOfType<EnemyFollow>();

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Rigidbody>().isKinematic = true;
            }

            Time.timeScale = 0f;
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1f;
                Invoke(nameof(LoadScene), 2f);

            }

        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
