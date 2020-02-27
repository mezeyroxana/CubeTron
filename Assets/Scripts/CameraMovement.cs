using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public Transform center;
    float distance;
    Vector3 direction;

    // Use this for initialization
    void Start () {
        distance = Vector3.Distance(target.position, transform.position);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 direction = (target.position - center.position).normalized;
        transform.position = target.position + direction * distance;
        Vector3 closestToUp = target.forward;
        float greatestDot = 0f;
        foreach (Vector3 dir in new Vector3[]{
        target.forward, target.right, -target.forward, -target.right})
        {
            float curDot = Vector3.Dot(dir, transform.up);
            if (curDot > greatestDot)
            {
                greatestDot = curDot;
                closestToUp = dir;

            }

        }
        transform.rotation = Quaternion.LookRotation(-direction, closestToUp);


        /*//bal-jobb-fel-le mozgás esetén
        Vector3 direction = (target.position - center.position).normalized;
        transform.position = target.position + direction * distance;
        Vector3 cameraUp = Vector3.Cross(-direction, target.right);
        transform.rotation = Quaternion.LookRotation(-direction, cameraUp);*/
    }
}
