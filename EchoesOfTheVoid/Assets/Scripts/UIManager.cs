using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject earth;
    [SerializeField] private GameObject spaceship;
    [SerializeField] private GameObject atmosphereReaction;
    [SerializeField] private GameObject bulletFactory;
    [SerializeField] private GameObject powerFactory;
    [SerializeField] private GameObject shieldFactory;
    [SerializeField] private GameObject healthFactory;
    [SerializeField] private GameObject bulletUi;
    [SerializeField] private GameObject healthUi;
    [SerializeField] private GameObject reloadMessage;
    [SerializeField] private GameObject orbUi;
    [SerializeField] private GameObject orbText;
    

    public GameObject bulletMessages; 
    void Start()
    {
        atmosphereReaction.SetActive(false);
        earth.SetActive(false);
        spaceship.SetActive(false);
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);
        shieldFactory.SetActive(false);
        healthFactory.SetActive(false);
        bulletUi.SetActive(false);
        healthUi.SetActive(false);
        reloadMessage.SetActive(false);
        orbUi.SetActive(false);
        orbText.SetActive(false);
    }

    public void SetAtmosphereObjectToActive()
    {
        atmosphereReaction.SetActive(true);
    }
    
    public void SetLevelObjectsToActive()
    {
        earth.SetActive(true);
        spaceship.SetActive(true);
        bulletUi.SetActive(true);
        bulletUi.SetActive(true);
        healthUi.SetActive(true);
        orbUi.SetActive(true);
        orbText.SetActive(true);
        reloadMessage.SetActive(true);
    }
}
