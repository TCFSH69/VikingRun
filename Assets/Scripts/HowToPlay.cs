using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour, IPointerClickHandler
{
    public int SceneIndexDestination;

    public void OnPointerClick(PointerEventData e)
    {
        // get current scene
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0) SceneIndexDestination = 2;
        else SceneIndexDestination = 0;

        //load new scene
        SceneManager.LoadScene(SceneIndexDestination);
    }
}