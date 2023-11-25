using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wait : CustomerState
{
    CustomerController customerController;
    Rigidbody2D rigidbody2D;
    Patience patience;
    bool arrived;
    float speed;
    Vector2 queuePosition;
    public static event Action<Laundry> OnPatienceEnded;

    public Wait(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.WAIT;
        customerController = customer.GetComponent<CustomerController>();
        rigidbody2D = customerController.GetComponent<Rigidbody2D>();
        patience = customerController.patience;
        speed = customerController.speed;
        queuePosition = customerController.queuePosition;

        Timer.OnTimerEnded += Leave;
    }

    public override void Enter()
    {
        patience.StartPatienceMeter();

        base.Enter();
    }

    public override void Update()
    {
        WalkToRegister();

        if (patience.hasPatienceEnded)
        {
            Leave();
            OnPatienceEnded.Invoke(customerController.laundry);
        }
        if (customerController.laundry.state == Laundry.STATE.DONE)
        {
            Leave();
        }
        if (customerController.laundry.state == Laundry.STATE.DISCARD)
        {
            Leave();
        }
    }

    private void WalkToRegister()
    {
        if (!arrived)
        {
            Vector2 currentPosition = rigidbody2D.position;

            float step = speed * Time.deltaTime;
            if (currentPosition != queuePosition)
            {
                Vector2 newPosition = Vector2.MoveTowards(currentPosition, queuePosition, step);

                Vector2 lookDirection = newPosition - currentPosition;
                lookDirection.Normalize();

                anim.SetFloat("Look X", lookDirection.x);
                anim.SetFloat("Look Y", lookDirection.y);
                anim.SetFloat("Speed", lookDirection.magnitude);

                rigidbody2D.MovePosition(newPosition);
            }
            else
            {
                arrived = true;

                anim.SetFloat("Speed", 0f);
                anim.SetTrigger("Idle");
            }
        }
    }

    private void Leave()
    {
        nextState = new Leave(customer, anim, player);
        stage = EVENT.EXIT;
        QueuePointsController.instance.SetQueuePointAvailable(customerController.queueIndex);
    }

    public override void Exit()
    {
        Timer.OnTimerEnded -= Leave;

        base.Exit();
    }
}
