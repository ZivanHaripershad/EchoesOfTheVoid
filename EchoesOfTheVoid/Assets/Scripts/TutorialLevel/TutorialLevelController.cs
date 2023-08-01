using System;
using UnityEngine;

public class TutorialLevelController : MonoBehaviour
{
    public GameObject[] popUps;

    public TutorialData tutorialData;

    public MouseControl mouseControl;

    public EnemySpawning enemySpawning;

    private int popUpIndex;

    private void Start()
    {
        tutorialData.popUpIndex = 0;
        mouseControl.EnableMouse();
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
        }
        else if (popUpIndex == 2)
        {
            //show player how to move and wait for left and right arrow key input
        }
        else if (popUpIndex == 3)
        {
            //show player how to shoot
        }
        else if (popUpIndex == 4)
        {
            //show player how to switch and collect orbs
        }
        else if (popUpIndex == 5)
        {
            //show player how to go back to planet to orbit
        }
        else if (popUpIndex == 6)
        {
            //show player how to spend orbs
        }
        else if (popUpIndex == 7)
        {
            // enemySpawning.SpawnEnemies();
            //let player play and win against enemies
        }
    }
}
