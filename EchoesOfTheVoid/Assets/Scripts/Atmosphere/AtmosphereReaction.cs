using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereReaction : MonoBehaviour
{

    // Define initial and target sizes herE;
    private float minSize = 0.13f;
    private float maxSize = 0.4f;
    private float targetSize = 0.4f;

    
    // Define speed of resizing
    public float resizeSpeed = 2f;

    [SerializeField]
    public GameObject bulletFactory;

    [SerializeField]
    public GameObject powerFactory;

    // Start is called before the first frame update
    void Start()
    {
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (transform.localScale.x < maxSize)
            {
                targetSize = 0.4f;
            }
        }
        else
        {
            bulletFactory.SetActive(false);
            powerFactory.SetActive(false);
            targetSize = minSize;
        }

        // Resize circle using Mathf.Lerp function
        float size = Mathf.Lerp(transform.localScale.x, targetSize, Time.deltaTime * resizeSpeed);
        transform.localScale = new Vector3(size, size, 1f);

        if (size >= (maxSize - 0.05))
        {
            bulletFactory.SetActive(true);
            powerFactory.SetActive(true);
        }
    }
}
