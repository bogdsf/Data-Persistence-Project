using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.ComponentModel.Design;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    //public string nameText;
    public TMP_InputField inputField;
    public string userName;

    // Start is called before the first frame update
    void Start()
    {
        userName = Bus.Instance.userName;
        inputField.text = userName;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartNew()
    {
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

    public void WriteName()
    {
        userName = inputField.text;
        Bus.Instance.userName = userName;

    }
}
