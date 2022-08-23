using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public GameObject gameOverCanvas;
    Scoore score;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        score = FindObjectOfType<Scoore>();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        score.SumbitPoints();
        Time.timeScale = 0;
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
