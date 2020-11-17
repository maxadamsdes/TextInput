using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScoreBoard : MonoBehaviour
{
    public ScrollRect scoreBoard;
    public GameObject scorePrefab;
    public Text myScore;
    // Start is called before the first frame update
    void Awake()
    {
        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScrollRect>();
        scorePrefab = Resources.Load("Score") as GameObject;
        DefaultControls.Resources tempResource = new DefaultControls.Resources();
        GameObject newText = DefaultControls.CreateText(tempResource);
        newText.AddComponent<LayoutElement>();
        myScore.text = GameModel.cPlayer.Score.ToString();
        GameModel.ds.jsnGetAllPlayersScores();
    }

    public void jsnPlayerHighScoreListReceiverDel(List<Player> pReceivedList)
    {
        foreach(Player p in pReceivedList)
        {
            GameObject score = Instantiate(scorePrefab);
            int i = 1;
            //scroll = GameObject.Find("CardScroll");
            if (scoreBoard != null)
            {
                //ScrollViewGameObject container object
                score.transform.SetParent(scoreBoard.transform, false);
                score.GetComponent<Text>().text = i + ". " + p.PlayerName + " Score: " + p.Score;
                i += 1;
            }
        }
    }


}
