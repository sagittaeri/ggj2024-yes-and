using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartClicked()
    {
        Debug.Log("Start clicked");
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
