using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : State
{
    Vector2 moveDirection;
    Vector2 lookDirection;
    float speed;
    Rigidbody2D rigidbody2d;
    CustomerController customerController;

    public Queue(GameObject _customer, Animator _anim, GameObject _player) :
        base(_customer, _anim, _player)
    {
        name = STATE.QUEUE;
        rigidbody2d = customer.GetComponent<Rigidbody2D>();
        customerController = customer.GetComponent<CustomerController>();
        speed = customerController.speed;
    }

    public override void Enter()
    {
        // customer enters the laundromat

        float random = Random.Range(0f, 260f);
        moveDirection = new Vector2(Mathf.Cos(random), Mathf.Sin(random));

        base.Enter();
    }

    public override void Update()
    {
        // customer queues up at the furthest queue spot
        // if there is a further queue spot available 
        // then the customer moves up

        if (!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveDirection.magnitude);

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * moveDirection.x * Time.deltaTime;
        position.y = position.y + speed * moveDirection.y * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (customerController.isInteractedWith) // and is at the furthest queue spot
        {
            nextState = new Order(customer, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
