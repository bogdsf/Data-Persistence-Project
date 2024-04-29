using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text pointsText;

    public string userName;
    public int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        userName = Bus.Instance.userName;

        if (userName != "")
        {
            bestScore = Bus.Instance.points;
            inputField.text = userName;
            pointsText.text = bestScore.ToString();
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void StartNew()
    {
        if (userName == "")
        {
            return;
        }

        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }

    [System.Serializable]
    struct UserData
    {
        public UserData(string userName, int userBestScore)
        {
            UserName = userName;
            UserBestScore = userBestScore;
        }

        public string UserName;
        public int UserBestScore;
    }

    public void SaveUserData()
    {
        if (userName == "")
        {
            return;
        }

        UserData data = new UserData(userName, bestScore);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + $"/{userName}_data.json", json);
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + $"/{inputField.text}_data.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UserData data = JsonUtility.FromJson<UserData>(json);

            userName = data.UserName;
            bestScore = data.UserBestScore;
        }
        else
        {
            userName = inputField.text;
            bestScore = 0;
        }

        inputField.text = $"<b>{userName}</b>";
        pointsText.text = $"<b>{bestScore.ToString()}</b>";

        Bus.Instance.userName = userName;
        Bus.Instance.points = bestScore;
    }
}






