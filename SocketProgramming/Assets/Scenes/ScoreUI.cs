using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreMananager;

    // Start is called before the first frame update
    void Start()
    {
        scoreMananager.AddScore(new Score("Hello", 10));
        scoreMananager.AddScore(new Score("World", 20));

        var scores = scoreMananager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.username.text = scores[i].username;
            row.score.text = scores[i].score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
