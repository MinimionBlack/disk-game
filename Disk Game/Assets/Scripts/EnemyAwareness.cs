using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{

    public bool isAggro;
    public Material aggroMat;

    private void Update()
    {
        if(isAggro)
        {
                    GetComponent<MeshRenderer>().material = aggroMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            isAggro = true;
        }
    }


}
