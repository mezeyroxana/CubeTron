using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 2;
    public GameObject prefab;
    private float horizontal, vertical;
    private float newHorizontal,newVertical;
    



    private Vector3 moveDirection;
    private Rigidbody myRigidbody;

    void Start()
    {
        horizontal = 0;
        vertical = 1;
        StartCoroutine(TrailMaker());
        //myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //90 fokos forgatás esetén
        newHorizontal = Input.GetAxisRaw("Horizontal_1");
        newVertical = Input.GetAxisRaw("Vertical_1");
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



        /*//fel-le-jobbra-balra mozgás esetén
        newHorizontal = Input.GetAxisRaw("Horizontal_1"); ;
        newVertical = Input.GetAxisRaw("Vertical_1");
        if (horizontal == 0 && newHorizontal != 0)
        {
            horizontal = newHorizontal;
            vertical = 0;
        }

        if (vertical == 0 && newVertical != 0)
        {
            vertical = newVertical;
            horizontal = 0;
        }
        moveDirection = new Vector3(horizontal, 0f, vertical);*/

    }

    void FixedUpdate()
    {
        //90 fokos forgatás esetén
        transform.Translate(0, 0, Time.deltaTime * moveSpeed);

        /*//fel-le-jobbra-balra mozgás esetén
        myRigidbody.MovePosition(myRigidbody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);*/
    }

    public IEnumerator TrailMaker()
    {
        while (true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Quaternion rotation = transform.rotation;
            yield return new WaitForSeconds(0.3f);
            Instantiate(prefab, position, rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trail")
            Debug.Log("Game over");
    }

}
