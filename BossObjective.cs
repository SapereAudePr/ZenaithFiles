using UnityEngine;
using UnityEngine.UI;

public class BossObjective : MonoBehaviour
{
    public Text objectiveText;
    public GameObject boss;

    private bool bossDestroyed = false;

    void Start()
    {
        // Make sure the objective text is initialized properly
        UpdateObjectiveText();
    }

    void Update()
    {
        // Check if the boss is destroyed
        if (!bossDestroyed && boss == null)
        {
            // Set the bossDestroyed flag to true
            bossDestroyed = true;

            // Update the objective text to show that the boss is destroyed
            UpdateObjectiveText();
        }
    }

    void UpdateObjectiveText()
    {
        if (bossDestroyed)
        {
            // Change the text to show the objective is completed
            objectiveText.text = "Objective Completed!";
        }
        else
        {
            // Change the text to show that the boss needs to be destroyed
            objectiveText.text = "Destroy the Boss!";
        }
    }
}
