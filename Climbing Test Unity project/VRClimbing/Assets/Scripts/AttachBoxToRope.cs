using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBoxToRope : MonoBehaviour
{
    [HideInInspector]
    public GameObject hangingCrate, secondLastRopePart;
    private bool isSpawned;

    private Vector3 offset = new Vector3(0,-0.5f,0);

    private void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            hangingCrate = GameObject.FindGameObjectWithTag("Crate");
            secondLastRopePart = GameObject.FindGameObjectWithTag("RopeSpawner").transform.GetChild(1).gameObject;

            isSpawned = true;
        }

        hangingCrate.transform.position = transform.position + offset;
        hangingCrate.transform.rotation = Quaternion.LookRotation(transform.position - secondLastRopePart.transform.position);
    }
}
