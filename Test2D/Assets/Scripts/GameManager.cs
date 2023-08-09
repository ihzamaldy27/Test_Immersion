using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text textScore;
    public Text upSkill;
    public Text textGameOverScore;
    public int score;

    [Header("UI")]
    public GameObject UI_TextUpSkill;
    public GameObject UI_GameOver;
    public GameObject UI_WinGame;

    [Header("Prefabs")]
    public GameObject potion;
    public GameObject obstacle;

    [Header("SetPosition")]
    [SerializeField] Camera cam;
    [SerializeField] int offsetX, offsetY;
    int randomX, randomY;

    bool potionSpawn = true;
    bool obstacleSpawn = true;

    [SerializeField]
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score : " + 0.ToString();
        UI_GameOver.SetActive(false);
        UI_WinGame.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (potionSpawn)
        {
            potionSpawn = false;
            GenerateObject(potion);
        }
        else if (obstacleSpawn)
        {
            obstacleSpawn = false;
            GenerateObject(obstacle);
        }
    }

    public void AddScore(int _addscore)
    {
        score += _addscore;
        textScore.text = "Score : " + score.ToString();
        if (score == 250)
        {
            playerController.OnShield();
            StartCoroutine(UIUpSkill("Shield"));
        }
        else if (score == 500)
        {
            playerController.OnSword();
            StartCoroutine(UIUpSkill("Sword"));

        } else if (score >= 1000)
        {

        }
    }

    Vector2 GetRandomPosition()
    {
        randomX = Random.Range(0 + offsetX, Screen.width - offsetX);
        randomY = Random.Range(0 + offsetY, Screen.height - offsetY);

        Vector2 coordinates = new Vector2(randomX, randomY);

        Vector2 screenToWorldPosition = cam.ScreenToWorldPoint(coordinates);

        return screenToWorldPosition;
    }

    void GenerateObject(GameObject _object)
    {
        if (_object == potion)
            StartCoroutine(SpawnObject(_object, 3));
        else
            StartCoroutine(SpawnObject(_object, 5));

        
    }

    IEnumerator SpawnObject(GameObject _object, float _timeWait)
    {
        Vector2 position = GetRandomPosition();
        Instantiate(_object, position, Quaternion.identity);
        yield return new WaitForSeconds(_timeWait);
        if (_object == potion)
        {
            potionSpawn = true;
        }
        else if (_object == obstacle)
        {
            obstacleSpawn = true;
        }

    }

    IEnumerator UIUpSkill(string _skill)
    {
        string _baseText = "Skill Unlock [";
        UI_TextUpSkill.SetActive(true);
        upSkill.text = _baseText + _skill + "]";
        yield return new WaitForSeconds(4f);
        UI_TextUpSkill.SetActive(false);
    }

    public void GameOver()
    {
        UI_GameOver.SetActive(true);
        textGameOverScore.text = textScore.text;
        Time.timeScale = 0;
    }

    public void WinGames()
    {
        UI_WinGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void btnRestart()
    {
        SceneManager.LoadScene("InGames");
        Time.timeScale = 1;
    }
}
