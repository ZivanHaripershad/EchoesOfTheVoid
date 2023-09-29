using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{

    public float speed;
    public float innacuracy;
    private float jitter;

    // Start is called before the first frame update
    void Start()
    {
        if (SelectedUpgradeLevel3.Instance != null &&
            SelectedUpgradeLevel3.Instance.GetUpgrade() != null &&
            SelectedUpgradeLevel3.Instance.GetUpgrade().GetName() == "BulletAccuracyUpgrade")
        {
            innacuracy -= (innacuracy * SelectedUpgradeLevel3.Instance.GetUpgrade().GetValue());
        }
        
        jitter = Random.Range(-innacuracy, innacuracy);
    }

    // Update is called once per frame
    void Update()
    {
        if (jitter > 0)
            jitter += 0.001f;
        else
            jitter -= 0.001f;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.Translate(Vector3.up * jitter * Time.deltaTime);

        if (transform.position.x < -10 || transform.position.x > 10)
            Destroy(gameObject);
        if (transform.position.y < -5 || transform.position.y > 5)  
            Destroy(gameObject);
    }
}
