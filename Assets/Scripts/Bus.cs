using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bus : MonoBehaviour
{
    public static Bus Instance;
    public string userName;
    public int points;
    public List<int> scoreHistory;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        scoreHistory = new List<int>();
    }
    public void AddScoreHistiry(int points)
    {
        scoreHistory.Add(points);
        scoreHistory.Sort();
    }
}
