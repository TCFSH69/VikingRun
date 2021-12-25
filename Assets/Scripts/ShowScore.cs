using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public GameObject Txt;
    // Start is called before the first frame update
    void Start()
    {
        Text txt = gameObject.GetComponent<Text>();

        txt.text = $"SCORE: {VikingControl.score}\r\nTIME: {VikingControl.totalTime} seconds";
    }

}
