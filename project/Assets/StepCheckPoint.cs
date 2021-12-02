using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCheckPoint : MonoBehaviour
{
    public RaycastAgent agent;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Raycast(other.transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, 2.1f))
            {
                if (hit.collider.gameObject.tag == "Step")
                {
                    this.agent.StepCheckPoint();
                }
                else
                {
                    this.agent.AntiStepCheckPoint();
                }
            }
        }
    }
}
