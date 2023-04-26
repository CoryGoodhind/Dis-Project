using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCabinetHandle : MonoBehaviour
{
    public UserMovement user;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<UserMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "unit")
        {
            user.collisionWithUnit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "unit")
        {
            user.collisionWithUnit = false;
        }
    }
}
