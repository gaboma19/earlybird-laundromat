using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    Animator anim;
    public Transform player;
    State currentState;
    Laundry laundry = new Laundry();

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = new Queue(this.gameObject, anim, player);

        // not all customers are queueing up for drop off service
        // in this case, their start state is Wash.
        // currentState = new Wash(this.gameObject, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
