using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int score=0;
    private Text myText;

    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
