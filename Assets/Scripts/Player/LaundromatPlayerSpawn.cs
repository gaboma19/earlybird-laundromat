using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundromatPlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        Vector2 position = transform.position;
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

        if (existingPlayer != null)
        {
            PlayerController playerController = existingPlayer.GetComponent<PlayerController>();
            playerController.SetPosition(position);
        }
        else
        {
            Instantiate(player, position, Quaternion.identity);
        }

        Workshift.instance.SetLaundromatUI();
    }
}
