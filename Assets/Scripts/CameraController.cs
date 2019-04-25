using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
// Purpose of this script is to fix the camera on the player
{
    public Transform target;
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfwidth;

    public int musicToPlay;
    private bool musicStarted;

    void Start()
    {
        //target = PlayerController.instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfwidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3 (halfwidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3 (-halfwidth, -halfHeight, 0f);

        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);

    }

    // Issue with lag caused because camera and player movement were both being
    // calculated in the Update function; LateUpdate handles those separately
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keeps camera within map
        transform.position = new Vector3(
            Mathf.Clamp (transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
            );

        if(!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayMusic(musicToPlay);

        }
   
    }
}
