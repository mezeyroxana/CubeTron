using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{

    private float gravity = -10;

    public void Attract(Transform playerBody)
    {
        Vector3 direction = (transform.position - playerBody.transform.position).normalized;
        Vector3 prevForward = playerBody.transform.forward;
        RaycastHit hit;
        if (GetComponent<Collider>().Raycast(new Ray(playerBody.transform.position, direction), out hit, 8))
        {
            Vector3 newForward = Vector3.Cross(playerBody.transform.right, hit.normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(newForward, hit.normal);
            playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, rotation, Time.deltaTime * 15);
            playerBody.GetComponent<Rigidbody>().AddForce(hit.normal * gravity);
        }
    }
}
