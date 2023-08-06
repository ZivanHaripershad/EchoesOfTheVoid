using System;
using System.Collections;
using UnityEngine;

public class AnimateEndingBar : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField] private GameObject leafIcon;
    [SerializeField] private GameObject orbIcon;
    [SerializeField] private GameObject enemyIcon;
    [SerializeField] private GameObject energyIcon;


    private SpriteRenderer leaf;
    private SpriteRenderer orb;
    private SpriteRenderer enemy;
    private SpriteRenderer energy;

    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float leafTime;
    [SerializeField] private float orbTime;
    [SerializeField] private float enemyTime;
    [SerializeField] private float energyTime;

    private bool leafStarted;
    private bool orbStarted;
    private bool enemyStarted;
    private bool energyStarted;

    // Start is called before the first frame update
    void Start()
    {
        leaf = leafIcon.GetComponent<SpriteRenderer>();
        orb = orbIcon.GetComponent<SpriteRenderer>();
        enemy = enemyIcon.GetComponent<SpriteRenderer>();
        energy = energyIcon.GetComponent<SpriteRenderer>();

        leaf.color = new Color(1f, 1f, 1f, 0f);
        orb.color = new Color(1f, 1f, 1f, 0f);
        enemy.color = new Color(1f, 1f, 1f, 0f);
        energy.color = new Color(1f, 1f, 1f, 0f);
        
        leafStarted = false;
        orbStarted = false;
        enemyStarted = false;
        energyStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        float normalisedTime = Math.Clamp(time, 0, 1);

        if (normalisedTime > leafTime && !leafStarted)
        {
            leafStarted = true;
            Debug.Log("leafTime");
            StartCoroutine(FadeIn(leaf));
        }

        if (normalisedTime > orbTime && !orbStarted)
        {
            orbStarted = true;
            StartCoroutine(FadeIn(orb));
        }

        if (normalisedTime > enemyTime && !enemyStarted)
        {
            enemyStarted = true;
            StartCoroutine(FadeIn(enemy));
        }

        if (normalisedTime > energyTime && !energyStarted)
        {
            energyStarted = true;
            StartCoroutine(FadeIn(energy));
        }
    }
    
    private IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        float alpha = 0f;

        while (alpha < 1f)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            alpha += fadeInSpeed * Time.deltaTime; 
            yield return null;
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
