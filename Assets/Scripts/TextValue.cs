using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextValue : MonoBehaviour
{
    public int currentNumberOfEnemies;
    public int maxNumberOfEnemies;

    private TextMesh texttMesh;

    void Start()
    {
        currentNumberOfEnemies = 0;

        texttMesh = GetComponent<TextMesh>();

        SetText();
    }

    void Update()
    {
        SetText();

        if(currentNumberOfEnemies == maxNumberOfEnemies)
        {
            GameWon();
        }
    }

    void SetText()
    {
        texttMesh.text = currentNumberOfEnemies + " / " + maxNumberOfEnemies;
    }

    void GameWon()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
