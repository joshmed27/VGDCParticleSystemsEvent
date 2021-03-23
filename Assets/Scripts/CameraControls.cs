using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float followThreshold = 0.5f;
    [SerializeField] private float smoothTime = 0.3f;

    private float cameraOffsetX = 0f;
    private float cameraOffsetY = 0f;
    private Vector3 velocity = Vector3.zero;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Calculate camera follow offset
        float cameraTargetX = Camera.main.ViewportToWorldPoint(Vector3.right * 0.5f).x;
        float playerTargetX = Camera.main.ViewportToWorldPoint(Vector3.right * followThreshold).x;
        cameraOffsetX = playerTargetX - cameraTargetX;

        float cameraTargetY = Camera.main.ViewportToWorldPoint(Vector3.right * 0.5f).y;
        float playerTargetY = Camera.main.ViewportToWorldPoint(Vector3.right * followThreshold).y;
        cameraOffsetY = playerTargetY - cameraTargetY;
    }

    void FixedUpdate()
    {
        float playerViewportX = Camera.main.WorldToViewportPoint(player.transform.position).x;
        float playerViewportY = Camera.main.WorldToViewportPoint(player.transform.position).y;
        if (playerViewportX > followThreshold)
        {
            // Move camera forward to follow the player
            Vector3 cameraTargetPosition = new Vector3(player.transform.position.x - cameraOffsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);
        }

        if (playerViewportX < followThreshold)
        {
            // Move camera forward to follow the player
            Vector3 cameraTargetPosition = new Vector3(player.transform.position.x + cameraOffsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);
        }

        if (playerViewportY < followThreshold)
        {
            // Move camera forward to follow the player
            Vector3 cameraTargetPosition = new Vector3(transform.position.x , player.transform.position.y - cameraOffsetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);
        }

        if (playerViewportY > followThreshold)
        {
            // Move camera forward to follow the player
            Vector3 cameraTargetPosition = new Vector3(transform.position.x , player.transform.position.y + cameraOffsetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);
        }
    }
}
