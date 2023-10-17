using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCounterUI : MonoBehaviour
{
    private static OrbCounterUI instance;

    public Text orbCounterText;
    
    public GameManagerData gameManagerData;

    [SerializeField] private OrbCounter orbCounter;

    void Awake()
    {
        orbCounter.orbsCollected = 0;
        
        if (!gameManagerData.level.Equals(GameManagerData.Level.Tutorial))
        {
            if (GameStateManager.Instance.IsLevel2Completed)
            {
                if (SelectedUpgradeLevel2.Instance != null &&
                    SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
                {
                    GameStateManager.Instance.SetMaxOrbCapacity(6);
                }
            }

            if (GameStateManager.Instance.IsLevel3Completed)
            {
                if (SelectedUpgradeLevel3.Instance != null &&
                    SelectedUpgradeLevel3.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel3.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
                {
                    GameStateManager.Instance.SetMaxOrbCapacity(8);
                }
            }
            
        }
        else
        {
            GameStateManager.Instance.SetMaxOrbCapacity(4);
        }
        
        UpdateOrbText();
        instance = this;
    }

    public static OrbCounterUI GetInstance()
    {
        if (instance == null)
            instance = new OrbCounterUI();
        
        return instance;
    }

    // Update is called once per frame
    public void IncrementOrbs(int numOrbs = 1)
    {
        gameManagerData.numberOfOrbsCollected += numOrbs;
        orbCounter.orbsCollected += numOrbs;
        UpdateOrbText();
    }
    
    public void DecrementOrbs(int numOrbs = 1)
    {
        orbCounter.orbsCollected -= numOrbs;
        UpdateOrbText();
    }

    public void SetOrbText(int numOrbs)
    {
        orbCounter.orbsCollected = numOrbs;
        UpdateOrbText();
    }

    private void UpdateOrbText()
    {
        orbCounterText.text = orbCounter.orbsCollected + "/" + GameStateManager.Instance.GetMaxOrbCapacity();
    }
}