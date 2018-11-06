using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.playerController.Player_pre_pos.position, 0.1f);

            Debug.Log(transform.position);
        }

    }
}
