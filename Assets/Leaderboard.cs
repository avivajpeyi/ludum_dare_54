using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

// https://danqzq.itch.io/leaderboard-creator


public class Leaderboard : Singleton<Leaderboard>
{
    [SerializeField] private List<TMP_Text> boardTxtItems;


    private string publicKey =
        "6307e4d417f7149ade1a7aa54d47d8bd4015105247002f428384551e6da0487a";

    private string key =
        "09dd03a7582e5d8818017be4e79589426cd6b54c409c78ed4e27caca2657902c9eed76e3bc8e2f19180163f862ca34cea232894c1ca08cc1da583ffa762173b896de1a24600ce45e30daca82ef508823411ec6646db1f38f3b3ac411afa22e6db29b8522d7814eec43d3f25f9f72f8946344dd7414d5e049e77847b4cc21f5b8";

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            Debug.Log($"Number items in leaderboard: {msg.Length}");
            int numToDisplay = msg.Length < boardTxtItems.Count
                ? msg.Length
                : boardTxtItems.Count;
            
            
            for (int i = 0; i < boardTxtItems.Count; i++)
            {
                if (i < numToDisplay)
                {
                    Debug.Log($">> {msg[i].Username} {msg[i].Score}");
                    String usn = msg[i].Username.PadRight(10, ' ');
                    String scr = msg[i].Score.ToString().PadLeft(2, '0');
                    String ext = msg[i].Extra;
                    boardTxtItems[i].text = $"{i + 1:00}) {usn}\t {scr}\t [{ext}]";
                }
                else
                {
                    // hide the TMP_Text game object
                    boardTxtItems[i].gameObject.SetActive(false);
                    
                }
                
            }
        }));
    }

    public void SetLeaderboard(string username, int score, string time)
    {
        if (username.Length > 10)
        {
            Debug.Log("Username must be between 3 and 16 characters long");
            // cap the username to 16 characters
            username = username.Substring(0, 10);
        }

        Debug.Log($"Submitting score... {username} {score} {time}");
        LeaderboardCreator.UploadNewEntry(
            publicKey, 
            username,
            score,
            time,
            OnUploadComplete);
    }

    public void OnUploadComplete(bool success)
    {
        if (success)
        {
            Debug.Log("Upload successful");
            GetLeaderboard();
        }
        else
        {
            Debug.Log("Upload failed");
        }
    }

    void Start() => GetLeaderboard();
}