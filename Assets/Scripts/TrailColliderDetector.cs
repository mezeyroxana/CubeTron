using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColliderDetector : MonoBehaviour
{
    [HideInInspector] public float timer;
    void Start()
    {
        Destroy(gameObject, 5000);
        timer = 0.1f;
    }

    private void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
    }
}
