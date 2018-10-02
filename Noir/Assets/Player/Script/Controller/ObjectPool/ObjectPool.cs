using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool objectPool;
    public int PooledAmount;
   
    public GameObject LongAttack;

    public List<GameObject> LongAttacks;

    // Use this for initialization
    private void Awake()
    {
        objectPool = this;

        LongAttack = GameObject.Find("LongAttack");
        LongAttack.SetActive(false);
    }

    void Start ()
    {
        LongAttacks = new List<GameObject>();

        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(LongAttack);
            obj.SetActive(false);
            LongAttacks.Add(obj);
        }

    }
	public GameObject LongAttackObj()
    {
        for (int i = 0; i < LongAttacks.Count; i++)
        {
            if (!LongAttacks[i].activeInHierarchy)
            {
                return LongAttacks[i];
            }
        }

        return null;

    }
}
