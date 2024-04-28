using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    public TMP_Text ScoreHistoryText;
    public Text bestScoreText;

    private List<int> scoreHistory;
    void Start()
    {
        scoreHistory = Bus.Instance.scoreHistory;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        WriteInScoreHistoryText();
        bestScoreText.text = "Best Score: " + Bus.Instance.userName + ": " + Bus.Instance.points.ToString();
    }
    void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        Bus.Instance.AddScoreHistiry(m_Points);
        WriteInScoreHistoryText();

        BestGameScore();
    }
    void BestGameScore()
    {
        scoreHistory = Bus.Instance.scoreHistory;
        int count = scoreHistory.Count;
        if (count > 0)
        {
            int busPoints = Bus.Instance.points;
            int maxGameScore = scoreHistory[count - 1];

            if (maxGameScore > busPoints)
            {
                Bus.Instance.points = maxGameScore;
                bestScoreText.text = "Best Score: " + Bus.Instance.userName + ": " + Bus.Instance.points.ToString();
            }
        }
    }

    public void BackToMenu()
    {
        scoreHistory = Bus.Instance.scoreHistory;
        scoreHistory.Clear();

        SceneManager.LoadScene(0);
    }

    void WriteInScoreHistoryText()
    {
        scoreHistory = Bus.Instance.scoreHistory;
        int count = scoreHistory.Count;
        string str = "";

        if (count > 0)
        {
            str = "Score History:" + "<br>";

            foreach (int score in scoreHistory)
            {
                str += (score.ToString() + "<br>");
            }
            ScoreHistoryText.text = str;
        }
    }
}
