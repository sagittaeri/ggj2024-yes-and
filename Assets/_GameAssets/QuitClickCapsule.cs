using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitClickCapsule : MonoBehaviour
{
    public void quitGame()
    {
        Debug.Log("wer done");    
        Application.Quit();
    }
}
