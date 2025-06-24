using Unity.VisualScripting;
using UnityEngine;

public class StirringStick : MonoBehaviour
{
    public CauldronController controller;

    private Vector3 lastPosition;
    private float stirAmount = 0f;
    private bool IsInCauldron = false;

    private void Update()
    {
        Vector3 movement = transform.position - lastPosition;
        float speed = movement.magnitude / Time.deltaTime;

        if(IsInCauldron && speed > 0.2f)
        {
            stirAmount += speed * Time.deltaTime;
            if(stirAmount >= 8f)
            {
                controller.TryCraftPotion();
                stirAmount = 0f;
            }
        }

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cauldron")
        {
            IsInCauldron = true;
        }
    }
}
