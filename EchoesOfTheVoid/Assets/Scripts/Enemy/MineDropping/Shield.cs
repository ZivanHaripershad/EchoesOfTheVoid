using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private GameObject mineEnemy;

    [SerializeField] private float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mineEnemy = GameObject.FindGameObjectWithTag("MineEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (mineEnemy == null)
            Destroy(gameObject);

        transform.position = Vector3.Lerp(transform.position, mineEnemy.transform.position, followSpeed);

    }
}
