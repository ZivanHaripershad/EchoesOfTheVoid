using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isShieldEnabled;

    public void DisableShield()
    {
        isShieldEnabled = false;
    }
    
    public void EnableShield()
    {
        isShieldEnabled = true;
    }

    public bool IsShieldEnabled()
    {
        return isShieldEnabled;
    }
}
