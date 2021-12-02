using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public RaycastAgent agent;

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.agent.Wall();
        }
    }
}
