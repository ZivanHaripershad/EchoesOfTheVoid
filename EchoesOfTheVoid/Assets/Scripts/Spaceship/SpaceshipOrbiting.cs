using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipOrbiting : MonoBehaviour
{
    [SerializeField]
    public Transform centerObject; // the center object to rotate around
    public float radius = 1.5f; // the radius of the circular path
    private float angle = 90f; // the current angle of rotation
    private Vector3 offset; // the offset from the center object position
    public float rotationDirection = 0f; // the direction of the rotation
    [SerializeField]
    public float angularSpeedFactor = 20f; // the factor to apply to base angular speed
    public float baseAngularSpeed = 2f; // the base angular speed

    void Start()
    {
        //set the original position of the spaceship
        transform.position = new Vector3(0f, 0f, 0f);

        offset = transform.position - centerObject.position;
    }

    void Update()
    {
        // Determine if clockwise or anticlockwise rotation should be applied based on key presses
        rotationDirection = Input.GetKey(KeyCode.D) ? -1f : Input.GetKey(KeyCode.A) ? 1f : 0f;

        // Calculate the position of the center of rotation by adding the center position and the offset vector
        Vector3 centerPos = centerObject.position + offset;

        // Calculate the new position of the object based on current angle
        angle += baseAngularSpeed * angularSpeedFactor * Time.deltaTime * rotationDirection;
        var x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius + centerPos.x;
        var y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius + centerPos.y;
        var newPosition = new Vector3(x, y, transform.position.z);

        // Set the rotation of the object to face the current angle of rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Set the position of the object
        transform.position = newPosition;
     
    }

    void OnDrawGizmosSelected()
    {
        // Draw a circle to represent the circular path
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(centerObject.position, radius);
    }

    
}
