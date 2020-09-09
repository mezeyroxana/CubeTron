using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject prefab;
    public string horizontalAxis;
    public string verticalAxis;
    public ResultHandler resultHandler;
    public Player1Data player1Data;
    public Player2Data player2Data;

    private float horizontal, vertical;
    private float newHorizontal, newVertical;
    private Renderer playerRenderer;
    private TrailRenderer trailRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        if (this.name == "Player1")
        {
            if (playerRenderer != null)
                playerRenderer.material = player1Data.color;
            if (trailRenderer != null)
                trailRenderer.material = player1Data.color;
        }
        else
        {
            if (playerRenderer != null)
                playerRenderer.material = player2Data.color;
            if (trailRenderer != null)
                trailRenderer.material = player2Data.color;
        }
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
        if (GameData.gameEnabled)
            transform.Translate(0, 0, Time.deltaTime * GameData.speed);
    }

    public IEnumerator TrailMaker()
    {
        Quaternion rotation = transform.rotation;
        Vector3 position;
        while (GameData.gameEnabled)
        {
            position = transform.position + transform.forward * 0.1f;
            float waitForSecs = 0.1f * (4 - GameData.speed);
            yield return new WaitForSeconds(waitForSecs);
            Instantiate(prefab, position, rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float timer = other.gameObject.GetComponent<TrailManager>().timer;
        if (gameObject.tag == "Player1")
        {
            if (other.gameObject.tag == "Trail1" && timer > 0)
                return;
            GameData.gameEnabled = false;
            GameData.isPlayer1Wins = false;
            resultHandler.ResultPanelHandler();
            Destroy(gameObject);
        }

        if (gameObject.tag == "Player2")
        {
            if (other.gameObject.tag == "Trail2" && timer > 0)
                return;
            GameData.gameEnabled = false;
            GameData.isPlayer1Wins = true;
            resultHandler.ResultPanelHandler();
            Destroy(gameObject);
        }
    }
}
