using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : CustomerState
{
    Vector2 queuePosition;
    Vector2 lookDirection;
    float speed;
    Rigidbody2D rigidbody2d;
    CustomerController customerController;
    bool arrived;

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
        customerController.isInteractable = true;

        queuePosition = QueuePointsController.instance.NextAvailablePosition(customerController);
        arrived = false;

        base.Enter();
    }

    public override void Update()
    {
        // customer queues up at the furthest queue spot
        // if there is a further queue spot available 
        // then the customer moves up

        WalkToRegister();

        if (customerController.isInteractedWith) // and is at the furthest queue spot
        {
            nextState = new Order(customer, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public Vector2 RandomVector2(float angle, float angleMin)
    {
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    private void WalkToRegister()
    {
        if (!arrived)
        {
            Vector2 currentPosition = rigidbody2d.position;

            float step = speed * Time.deltaTime;
            if (currentPosition != queuePosition)
            {
                Vector2 newPosition = Vector2.MoveTowards(currentPosition, queuePosition, step);

                lookDirection = newPosition - currentPosition;
                lookDirection.Normalize();

                anim.SetFloat("Look X", lookDirection.x);
                anim.SetFloat("Look Y", lookDirection.y);
                anim.SetFloat("Speed", lookDirection.magnitude);

                rigidbody2d.MovePosition(newPosition);
            }
            else
            {
                arrived = true;

                anim.SetFloat("Speed", 0f);
                anim.SetTrigger("Idle");
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
