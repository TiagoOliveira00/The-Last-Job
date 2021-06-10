using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliders : MonoBehaviour
{
    public bool isPickable = true;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerInteractionZone")
        {
            collider.GetComponentInParent<PickUp>().ObjectToPickUp = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "PlayerInteractionZone")
        {
            coll.GetComponentInParent<PickUp>().ObjectToPickUp = null;
        }
    }
}
