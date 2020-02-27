using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 1;
    private float horizontal, vertical;
    private float newHorizontal, newVertical;

    void Start()
    {
        horizontal = 0;
        vertical = 1;
    }

    void Update()
    {
        newHorizontal = Input.GetAxisRaw("Horizontal_2");
        newVertical = Input.GetAxisRaw("Vertical_2");
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
}
