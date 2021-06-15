using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

 

public class GameManager : MonoBehaviour
{
    public enum Gamestate
    {
        loading,
        inGame,
        gameOver,

    }

    public Gamestate gameState;
    


    public List<GameObject> targetprefabs;
    private float spawnRate = 1f;

    public TextMeshProUGUI scoreText;

    public Button restartButton;



    private int _score;
    private int Score
    {
        set
        {
            _score = value;
            
            if (value < 0)
            {
                _score = 0;
            }
        }
        get
        {
            return _score;
        }
     
    }

    public TextMeshProUGUI gameOverText;

    public GameObject tittleScreen;


    private void Start()
    {
        ShowMaxScore();
    }



    /// <summary>
    /// metodo que inicia la partida cambiando el estado del valor del juego.
    /// </summary>
    /// <param name="difficulty"> numero entero que indica la dificultal del juego</param>
    public void StarGame(int difficulty)
    {
        gameState = Gamestate.inGame;
        tittleScreen.gameObject.SetActive(false);

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());

        Score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (gameState == Gamestate.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetprefabs.Count);
            Instantiate(targetprefabs[index]);
          
        }
    }
    /// <summary>
    /// actualizar la puntuacion y lo muestra por pantalla
    /// </summary>
    /// <param name="scoreToAdd">numero de puntos to add a la puntuacion global</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = ("Puntuacion:\n" + Score); //los parentesis no son necesarios // \n = intro (escaping symbol)
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("Max Score",0);

        scoreText.text = "Max Score:" + maxScore;

    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("Max Score", 0);
        if(Score >maxScore)
        {
            PlayerPrefs.SetInt("Max Score", Score);

        }
       

    }

    public void Gameover()
    {

        SetMaxScore();


        gameState = Gamestate.gameOver;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
