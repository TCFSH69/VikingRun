using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour, IPointerClickHandler
{
    private int score;
    public static void ToScore()
    {
        SceneManager.LoadScene(3);
    }
    public void OnPointerClick(PointerEventData e)
    {
        //load new scene
        SceneManager.LoadScene(0);
    }
}