using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text textScore;
    public Text upSkill;
    public int score;

    [Header("UI")]
    public GameObject UI_TextUpSkill;
    public GameObject UI_GameOver;

    [Header("Prefabs")]
    public GameObject potion;
    public GameObject obstacle;

    [SerializeField]
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score : " + 0.ToString();
        UI_GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int _addscore)
    {
        score += _addscore;
        textScore.text = "Score : " + score.ToString();
        if (score >= 250)
        {
            playerController.OnShield();
            StartCoroutine(UIUpSkill("Shield"));
        }
        else if (score >= 500)
        {
            playerController.OnSword();
            StartCoroutine(UIUpSkill("Sword"));

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
        Time.timeScale = 0;
    }
}
