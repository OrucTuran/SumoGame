using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] Vector3 offSet;
    void Start()
    {
        offSet = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offSet;
    }
}
