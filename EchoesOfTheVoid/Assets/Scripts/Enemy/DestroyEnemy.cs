using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{

    [SerializeField]
    GameObject explosion;

    private bool canBeDestroyed = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        canBeDestroyed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDestroyed)
        {
            canBeDestroyed = false;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}