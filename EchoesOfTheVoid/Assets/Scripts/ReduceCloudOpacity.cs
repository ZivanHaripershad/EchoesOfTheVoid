using UnityEngine;

public class ReduceCloudOpacity : MonoBehaviour
{
    [SerializeField] private float opacity; 
    void Start()
    {
        SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer>();
        sp.color = new Color(1f, 1f, 1f, opacity);
    }

}
