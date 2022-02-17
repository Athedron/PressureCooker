using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab, parentObject;

    [SerializeField]
    [Range(1, 100)]
    int length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Rope"))
            {
                Destroy(tmp);
            }
        }

        if (spawn)
        {
            Spawn();
            spawn = false; 
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < length; i++)
        {
            GameObject tmp;
            
            tmp = Instantiate(partPrefab, 
                        new Vector3(transform.localPosition.x, 
                                    transform.localPosition.y + partDistance * (i + 1), 
                                    transform.localPosition.z), 
                        Quaternion.identity, 
                        parentObject.transform);

            tmp.transform.eulerAngles = new Vector3(180, 0, 0);
            tmp.gameObject.AddComponent<Throwable>();

            tmp.gameObject.GetComponent<Throwable>().onHeldUpdate.AddListener((Hand) => 
            { 
                Climb(); 
            });

            tmp.name = partPrefab.name + ": " + parentObject.transform.childCount.ToString();

            if (i == 0)
            {
                //tmp.GetComponent<CapsuleCollider>().isTrigger = true;
                //tmp.gameObject.AddComponent<AttachBoxToRope>();
                Destroy(tmp.GetComponent<ConfigurableJoint>());

                if (snapFirst)
                {
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else
            {
                tmp.GetComponent<ConfigurableJoint>().connectedBody = parentObject.transform.Find(partPrefab.name + ": " + (parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }

        if (snapLast)
        {
            parentObject.transform.Find(partPrefab.name + ": " + (parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Climb()
    {

        Player.instance.transform.position -= Player.instance.rightHand.transform.position;
    }
}
