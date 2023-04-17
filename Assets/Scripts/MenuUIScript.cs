// --------------------------------------------------------------------------------
//  Copyright (C) 2023 TwoAmigos
// --------------------------------------------------------------------------------

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScript : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    public TMP_InputField NameInput;
    public TextMeshProUGUI BestScoretext;

    public void StartNew()
    {
        SceneManager.LoadScene("main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    private void Start()
    {
        NameInput.text = DataStore.Instance.CurrentPlayerName;
        BestScoretext.text = $"Bestscore: {DataStore.Instance.Name}:{DataStore.Instance.Score}";
        StartButton.onClick.AddListener(StartNew);
        QuitButton.onClick.AddListener(Exit);
        NameInput.onValueChanged.AddListener(UpdateName);
    }

    // Update is called once per frame
    private void UpdateName(string input)
    {
        DataStore.Instance.CurrentPlayerName = input;
    }
}