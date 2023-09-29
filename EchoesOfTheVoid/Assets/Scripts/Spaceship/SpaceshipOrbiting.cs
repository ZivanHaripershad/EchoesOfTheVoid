using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipOrbiting : MonoBehaviour
{
    [SerializeField]
    public Transform centerObject; // the center object to rotate around
    public float radius = 1.3f; // the radius of the circular path
    private float angle = 90f; // the current angle of rotation
    private Vector3 offset; // the offset from the center object position
    public float rotationDirection = 0f; // the direction of the rotation
    [SerializeField]
    public float angularSpeedFactor = 40f; // the factor to apply to base angular speed
    public float baseAngularSpeed = 2f; // the base angular speed
    public SpaceshipMode spaceshipMode;
    public float driftSpeed = 0.3f; //movment inertia
    public float inertiaReductionFactor; //how much the inertia reduces by each interval

    public float returnSpeed = 3f;

    public SpaceshipCollection spaceshipCollection;


    [SerializeField]
    private TrailRenderer trailRendererRight;
    [SerializeField]
    private TrailRenderer trailRendererLeft;

    public float inertia;

    [SerializeField]
    Animator animator;


    void Start()
    {
        //set the original position of the spaceship
        transform.position = new Vector3(0f, 0f, 0f);
        spaceshipMode.currentPosition = new Vector3(0f, 0f, 0f);

        offset = spaceshipMode.currentPosition - centerObject.position;
        spaceshipMode.returningToPlanet = false;
        spaceshipMode.isOnCenterObjectsRadius = false;
        spaceshipMode.canRotateAroundPlanet = true;
        inertia = 0;

        if (SelectedUpgradeLevel1.Instance != null && SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
            SelectedUpgradeLevel1.Instance.GetUpgrade().GetName() == "ShipHandlingUpgrade")
        {
            var upgradeInertia = SelectedUpgradeLevel1.Instance.GetUpgrade().GetValue();
            inertiaReductionFactor += (inertiaReductionFactor * upgradeInertia);
        }
        else if (SelectedUpgradeLevel2.Instance != null && SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
            SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "ShipHandlingUpgrade")
        {
            var upgradeInertia = SelectedUpgradeLevel2.Instance.GetUpgrade().GetValue();
            inertiaReductionFactor += (inertiaReductionFactor * upgradeInertia);
        }

        Cursor.visible = false;
    }

    void Update()
    {

        bool isPaused = Mathf.Approximately(Time.timeScale, 0f);

        if (isPaused)
            return;
        
        //this is for when the game starts and you initially move around the planet
        if(spaceshipMode.collectionMode == false && spaceshipMode.returningToPlanet == false && !spaceshipCollection.isEjecting){
            // Determine if clockwise or anticlockwise rotation should be applied based on key presses
            rotationDirection = Input.GetKey(KeyCode.D) ? -1f : Input.GetKey(KeyCode.A) ? 1f: 0f;
            
            //////////////// inertia //////////////////
            if (Input.GetKey(KeyCode.D))
                inertia = -driftSpeed;
            
            if (Input.GetKey(KeyCode.A))
                inertia = driftSpeed;
           
            if (inertia > 0)
                inertia -= inertiaReductionFactor * Time.deltaTime;

            if (inertia < 0)
                inertia += inertiaReductionFactor * Time.deltaTime;

            if (inertia < 0.0001 && inertia > -0.0001)
                inertia = 0; 
            /////////////////////////////////////////////////


            // // Calculate the position of the center of rotation by adding the center position and the offset vector
            Vector3 centerPos = centerObject.position + offset;

            // // Calculate the new position of the object based on current angle
            angle += baseAngularSpeed * angularSpeedFactor * Time.deltaTime * rotationDirection;

            //add the inertia
            angle += inertia; 

            var x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius + centerPos.x;
            var y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius + centerPos.y;
            var newPosition = new Vector3(x, y, transform.position.z);

            // // Set the rotation of the object to face the current angle of rotation
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // // Set the position of the object
            transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

            //set the trail renderer opacity to 0 while orbiting
            trailRendererLeft.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f));
            trailRendererRight.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f));
        }

        //this is for every instance after you do your first collection, which then allows you to go to the nearest position of the planet and rotate again
        if(spaceshipMode.collectionMode == false && spaceshipMode.returningToPlanet){

            //reset the inertia when returning
            inertia = 0; 
    
            Vector3 centerPos = centerObject.position + offset;
            // calculate nearest point on the centerObjects radius
            var dx = centerPos.x - transform.position.x;
            var dy = centerPos.y - transform.position.y;
            var length = Mathf.Sqrt(dx * dx + dy * dy);
            dx /= length;
            dy /= length;
            var nearestPoint = new Vector3(centerPos.x - dx * radius, centerPos.y - dy * radius, transform.position.z);

            spaceshipMode.isOnCenterObjectsRadius = length <= radius;
            angle = 180f + Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;


            if(spaceshipMode.isOnCenterObjectsRadius || spaceshipMode.canRotateAroundPlanet){
                spaceshipMode.collectionMode = false;
                spaceshipMode.returningToPlanet = false;
                spaceshipMode.canRotateAroundPlanet = true;

                if (animator != null)
                {
                    animator.SetBool("isCollectionMode", spaceshipMode.collectionMode);
                    animator.SetBool("isOrbitingMode", !spaceshipMode.collectionMode);
                }
                else{
                    Debug.LogWarning("animator is null");
                }
            }
            else{
                // move object towards nearest point
                var direction = (nearestPoint - transform.position).normalized;
                transform.Translate(direction * returnSpeed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            //set the trail renderer opacity to 0 while returning to planet
            trailRendererLeft.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f));
            trailRendererRight.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f));

        }
    }
    
}
