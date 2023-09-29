using UnityEngine;

public class UpgradeScene3Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private Upgrade selectedUpgrade; 

    // Start is called before the first frame update
    void Start()
    {
        selectedUpgrade = null;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        selectedUpgrade = upgrade;
    }

    public Upgrade GetUpgrade()
    {
        return selectedUpgrade;
    }

}
