using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1;
    public GameObject prefab;
    public string horizontalAxis;
    public string verticalAxis;

    private float horizontal, vertical;
    private float newHorizontal, newVertical;

    void Start()
    {
        horizontal = 0;
        vertical = 1;
        StartCoroutine(TrailMaker());
    }

    void Update()
    {
        newHorizontal = Input.GetAxisRaw(horizontalAxis);
        newVertical = Input.GetAxisRaw(verticalAxis);
        Quaternion targetRotation = transform.rotation;
        if (newHorizontal != 0 && vertical != 0)
        {
            if (newHorizontal == vertical)
                targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
            else
                targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
            horizontal = newHorizontal;
            vertical = 0;
        }
        if (newVertical != 0 && horizontal != 0)
        {
            if (newVertical == horizontal)
                targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
            else
                targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
            vertical = newVertical;
            horizontal = 0;
        }
        transform.rotation = targetRotation;

    }

    void FixedUpdate()
    {
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);
    }

    public IEnumerator TrailMaker()
    {
        Quaternion rotation = transform.rotation;
        Vector3 position;
        while (true)
        {
            position = transform.position + transform.forward * 0.1f;
            //position = EdgeExamination(position);
            yield return new WaitForSeconds(0.4f);
            Instantiate(prefab, position, rotation);
        }
    }

    private Vector3 EdgeExamination(Vector3 trail)
    {
        float x = trail.x, y = trail.y, z = trail.z;

        if (trail.x >= 2.625f || trail.x <= -2.625f)
        {
            x = 2.625f * GetSign(trail.x);
            if (trail.y > 2.4f || trail.y < -2.4f)
                y = 2.4f * GetSign(trail.y);
            if (trail.z > 2.4f || trail.z < -2.4f)
                z = 2.4f * GetSign(trail.z);
            return new Vector3(x, y, z);
        }
        if (trail.y >= 2.625f || trail.y <= -2.625f)
        {
            y = 2.625f * GetSign(trail.y);
            if (trail.x > 2.4f || trail.x < -2.4f)
                x = 2.4f * GetSign(trail.x);
            if (trail.z > 2.4f || trail.z < -2.4f)
                z = 2.4f * GetSign(trail.z);
            return new Vector3(x, y, z);
        }
        if (trail.z >= 2.625f || trail.z <= -2.625f)
        {
            z = 2.625f * GetSign(trail.z);
            if (trail.x > 2.4f || trail.x < -2.4f)
                x = 2.4f * GetSign(trail.x);
            if (trail.y > 2.4f || trail.y < -2.4f)
                y = 2.4f * GetSign(trail.y);            
            return new Vector3(x, y, z);
        }
        return new Vector3(x, y, z);
    }

    private float GetSign(float number)
    {
        if (number < 0)
            return -1;
        return 1;
    }
}
