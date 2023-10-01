using System;
using UnityEngine;

public class MineDroppingEnemyDodging: MonoBehaviour
{
    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    [SerializeField] float dogeDistance;
    [SerializeField] private GameObject mine;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            //spawn a mine 
            Instantiate(mine, transform.position, transform.rotation);
            
            Vector3 otherPosition = other.transform.position;
    
            if ((otherPosition - leftWing.transform.position).magnitude > (otherPosition - rightWing.transform.position).magnitude)
            {
                //doge to the right 
                transform.Translate(Vector3.right * dogeDistance);
            }
            else
            {
                //doge to the left 
                transform.Translate(Vector3.left * dogeDistance);
            }
        }
        
        
    }
}
