using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Correct namespace for TextMeshPro
using Dan.Main;

public class WaterPollutionLeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> names; // Use TMP_Text for displaying names
    [SerializeField]
    private List<TMP_Text> scores; // Use TMP_Text for displaying scores

    private string publicLeaderboardKey = "b0de67bcda89323794d64d21f89dfbc08c23d475eb4f028920baf427b2d741c1";

    private void Start()
    {
        GetWaterLeaderboard();
    }

    public void GetWaterLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;

            // Clear existing names and scores
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = ""; // Clear existing names
                scores[i].text = ""; // Clear existing scores
            }

            // Populate leaderboard with fetched data
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        // Check for an empty slot before uploading a new entry
        for (int i = 0; i < names.Count; i++)
        {
            if (string.IsNullOrEmpty(names[i].text)) // Check if the slot is empty
            {
                LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, (msg) =>
                {
                    LeaderboardCreator.ResetPlayer(); // Reset player ID to allow new entry
                    GetWaterLeaderboard(); // Refresh the leaderboard after adding a new entry

                    // Disable input fields after submission
                    var scoreGetter = FindObjectOfType<WaterPollutionScoreGetter>();
                    if (scoreGetter != null)
                    {
                        scoreGetter.DisableInputFields();
                    }
                });
                return; // Exit after adding to the first empty slot
            }
        }

        Debug.LogWarning("No empty slots available on the leaderboard.");
        // Optional: you can implement logic to remove the lowest score or prevent adding new entries
    }
}