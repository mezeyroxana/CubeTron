using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColliderDetector : MonoBehaviour
{

    private float timer;

    void Start()
    {
        Destroy(gameObject, 5000);
        timer = 0.5f;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            if (gameObject.tag == "Trail1" && timer > 0)
                return;
            Debug.Log("Player1 is dead");
        }
        if (other.gameObject.tag == "Player2")
        {
            if (gameObject.tag == "Trail2" && timer > 0)
                return;
            Debug.Log("Player2 is dead");
        }
    }
}
