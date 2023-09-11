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

    void Awake(){
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
        orbCounterText.text = orbCounter.orbsCollected.ToString();
    }
    
    public void DecrementOrbs(int numOrbs = 1)
    {
        orbCounter.orbsCollected -= numOrbs;
        orbCounterText.text = orbCounter.orbsCollected.ToString();
    }

    public void SetOrbText(int numOrbs)
    {
        orbCounter.orbsCollected = numOrbs;
        orbCounterText.text = orbCounter.orbsCollected.ToString();
    }
}