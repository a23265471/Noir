using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] Enemy;




	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < Enemy.Length; i++)
        {
            Instantiate(Enemy[i], new Vector3(0, 0, 0), Quaternion.EulerRotation(0, 0, 0));

        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}




}
