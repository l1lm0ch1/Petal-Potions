using UnityEngine;

public class Holster : MonoBehaviour
{
    public GameObject centerEyeAnchor;
    private float rotationspeed = 50;

    // Update is called once per frame
    void Update()
    {
        //Put holster halfway between on the body
        transform.position = new Vector3(centerEyeAnchor.transform.position.x, centerEyeAnchor.transform.position.y / 2, centerEyeAnchor.transform.position.z);

        var rotationDifference = Mathf.Abs(centerEyeAnchor.transform.eulerAngles.y - transform.eulerAngles.y);
        var finalRotationSpeed = rotationspeed;

        //make rotation speed faster if holster rotation is further away from the central eye camera
        if(rotationDifference > 60)
        {
            finalRotationSpeed = rotationspeed * 2;
        }

        else if (rotationDifference > 40 && rotationDifference < 60)
        {
            finalRotationSpeed = rotationspeed;
        }

        else if (rotationDifference < 40 &&  rotationDifference > 20)
        {
            finalRotationSpeed = rotationspeed / 2;
        }

        else if(rotationDifference < 20 &&  rotationDifference > 0)
        {
            finalRotationSpeed = rotationspeed / 4;
        }

        // the step size is equal to speed times frame time.
        var step = finalRotationSpeed * Time.deltaTime;

        //Rotate our transform a step closer to the target's
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, centerEyeAnchor.transform.eulerAngles.y, 0), step);
    }
}
