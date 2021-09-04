using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    private float speed = 1f;
    public string id;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void setID(string id)
    {
        this.id = id;
    }

    public void updatePosition(PlayerPosition position)
    {
        Vector3 newPosition = new Vector3(position.x,position.y);
        newPosition.z = transform.position.z;

        targetPosition = newPosition;
        
    }
}