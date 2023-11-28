using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : CustomerState
{
    Vector2 lookDirection;
    CustomerController customerController;
    Rigidbody2D rigidbody2D;

    public Leave(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.LEAVE;
        customerController = customer.GetComponent<CustomerController>();

        customerController.isInteractedWith = false;

        rigidbody2D = customerController.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        Spawn.instance.DequeueCustomer();

        if (customerController.laundry != null)
        {
            if (customerController.laundry.state == Laundry.STATE.DONE)
            {
                customerController.patience.SetDone();
            }
            if (customerController.laundry.state == Laundry.STATE.DISCARD)
            {
                customerController.patience.SetDiscard();
            }
        }

        base.Enter();
    }

    public override void Update()
    {
        Vector2 currentPosition = rigidbody2D.position;
        float speed = customerController.speed;
        Vector2 exitPosition = Spawn.instance.exitPoint;

        float step = speed * Time.deltaTime;
        if (currentPosition != exitPosition)
        {
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, exitPosition, step);

            lookDirection = newPosition - currentPosition;
            lookDirection.Normalize();

            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
            anim.SetFloat("Speed", lookDirection.magnitude);

            rigidbody2D.MovePosition(newPosition);
        }
        else
        {
            customerController.DestroyGameObject();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
