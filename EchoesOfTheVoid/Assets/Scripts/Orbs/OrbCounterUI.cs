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