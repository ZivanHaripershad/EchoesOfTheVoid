using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialLevelController : MonoBehaviour
{
    public GameObject[] popUps;

    public TutorialData tutorialData;
    public GameManagerData gameManagerData;

    public MouseControl mouseControl;

    public EnemySpawning enemySpawning;

    public UIManager uiManager;

    private int popUpIndex;
    
    private GlobalVariables variables;

    [SerializeField] private OrbCounter orbCounter;
    
    private float waitTime;
    
    private void Start()
    {
        tutorialData.popUpIndex = 0;
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        waitTime = 10f;
        mouseControl.EnableMouse();
        variables = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVariables>();
    }

    private void Update()
    {
        popUpIndex = tutorialData.popUpIndex;

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            //show first screen
        }
        else if (popUpIndex == 1)
        {
            //show second screen
            enemySpawning.ResetSpawning();
            enemySpawning.StopTheCoroutine();
        }
        else if (popUpIndex == 2)
        {
            //show player how to move and wait for left and right arrow key input and shoot
            mouseControl.DisableMouse();
            uiManager.SetLevelObjectsToActive();
            variables.mustPause = true;
            enemySpawning.StartSpawningEnemies(3, false);
            gameManagerData.expireOrbs = false;
            gameManagerData.tutorialActive = true;

            if (gameManagerData.numberOfEnemiesKilled == 3)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //show player how to collect orbs
            mouseControl.DisableMouse();
            if (gameManagerData.numberOfOrbsCollected == 3)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            //show player how to switch back to shooting mode
            mouseControl.DisableMouse();
            if (Input.GetKey(KeyCode.Space))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            uiManager.SetAtmosphereObjectToActive();
            mouseControl.DisableMouse();
            enemySpawning.ResetSpawning();
            enemySpawning.StopTheCoroutine();
            
            if (orbCounter.orbsCollected < 3 && orbCounter.planetOrbsDeposited < 1)
            {
                Debug.Log("FAILED");
            }
            
            //show player how to spend orbs

            if (orbCounter.planetOrbsDeposited > 0)
                tutorialData.popUpIndex++;
        }
        else if (popUpIndex == 6)
        {
            mouseControl.DisableMouse();
            if (waitTime <= 0)
            {
                variables.mustPause = false;
                popUps[popUpIndex].SetActive(false);
                gameManagerData.expireOrbs = true;
                gameManagerData.tutorialActive = false;
                enemySpawning.StartSpawningEnemies(3, true);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            //let player play and win against enemies

            if (orbCounter.orbsCollected >= orbCounter.planetOrbMax)
            {
                Debug.Log("Player wins!!!");
                
                //hide everything
                uiManager.SetLevelObjectsToInactive();
                
                //show the end of level screen and play the animation
                
            }
        }
    }
}
