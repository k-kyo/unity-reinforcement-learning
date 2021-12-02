using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour
{
    public RaycastAgent agent;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.agent.Goal();
        }
    }
}
