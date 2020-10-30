using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public int speed;
    NavGridAgent agent;
    int counter = 0;
    List<Vector3> waypoints;

    void Start()
    {
        agent = GetComponent<NavGridAgent>();  
    }

    void Update()
    {
        if(waypoints != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[counter], speed*Time.deltaTime);
            if(Vector3.SqrMagnitude(transform.position - waypoints[counter]) < 0.005f)
            {
                if(counter + 1 < waypoints.Count)
                {
                    counter++;
                }
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            counter = 0;
            //waypoints.Clear();
            waypoints = agent.SetDestination(mousePosition);
        }

    }


}
