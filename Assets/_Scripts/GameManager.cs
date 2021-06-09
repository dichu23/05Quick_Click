using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 

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
    

    // Start is called before the first frame update
    void Start()
    {
        gameState = Gamestate.inGame;

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

    public void Gameover()
    {

        gameState = Gamestate.gameOver;

        gameOverText.gameObject.SetActive(true);

    }

}
