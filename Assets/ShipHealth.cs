using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipHealth : MonoBehaviour
{
    public UnityEvent pilotEject;

    [SerializeField] GameObject pilot;
    [SerializeField] GameObject ship;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { spawnPilot(); }
    }

    void spawnPilot()
    {
        pilotEject.Invoke();
        pilot.GetComponent<PilotMovement>().velocity = ship.GetComponent<Movement>().velocity;
        pilot.transform.parent = null;
    }
}
