using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform player;
    
    public float speed = 20f;
    Vector3 Rotate;

    void Update()
    {
        Vector2 LookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.LookAt(new Vector3(0, LookAtPoint.y, LookAtPoint.x ) * Time.deltaTime);
    }
}
