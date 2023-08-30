using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController: MonoBehaviour
{
    [SerializeField]
    protected EnemySpawning enemySpawning;
    [SerializeField] 
    protected GameObject missionPopup;
    [SerializeField]
    protected Text planetHealthNum;
    [SerializeField]
    protected Text orbsNumber;
    [SerializeField]
    protected Text enemiesNumber;
    [SerializeField] 
    protected OrbCounter orbCounter;
    
    [SerializeField]
    protected float normalAudioSpeed;
    [SerializeField]
    protected float reducedAudioSpeed;

    [SerializeField] 
    protected float audioSpeedChangeRate;
    
    protected float audioSpeed;

    protected Coroutine audioCoroutine;
    
    private IEnumerator DecreaseSpeed()
    {
        while (audioSpeed > reducedAudioSpeed)
        {
            audioSpeed -= audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }

        audioSpeed = reducedAudioSpeed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (audioSpeed < normalAudioSpeed)
        {
            audioSpeed += audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }
        audioSpeed = normalAudioSpeed;
    }

    public void ReduceAudioSpeed()
    {
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);
        audioCoroutine = StartCoroutine(DecreaseSpeed());
    }

    public void IncreaseAudioSpeed()
    {
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);
        audioCoroutine = StartCoroutine(IncreaseSpeed());
    }
}
