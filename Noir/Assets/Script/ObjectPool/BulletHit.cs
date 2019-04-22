using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    public GameObject Bullet;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Enemy")
        {

            //Bullet.SetActive(false);


           // Debug.Log(other.name);

        }

        
    }
    
}
