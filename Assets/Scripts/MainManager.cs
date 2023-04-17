// --------------------------------------------------------------------------------
//  Copyright (C) 2023 TwoAmigos
// --------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text BestScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    public Button MenuButton;

    private bool m_Started;
    private int m_Points;

    private bool m_GameOver;

    public void GameOver()
    {
        if (m_Points > DataStore.Instance.Score)
        {
            DataStore.Instance.Score = m_Points;
            DataStore.Instance.Save();
            UpdateBestscoreText();
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
        MenuButton.gameObject.SetActive(true);
    }

    private void UpdateBestscoreText()
    {
        BestScoreText.text = $"Bestscore: {DataStore.Instance.Name}: {DataStore.Instance.Score}";
    }

    // Start is called before the first frame update
    private void Start()
    {
        MenuButton.onClick.AddListener(Back);
        UpdateBestscoreText();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = { 1, 1, 2, 2, 5, 5 };
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
    }

    private void Update()
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

    private void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    private void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }
}