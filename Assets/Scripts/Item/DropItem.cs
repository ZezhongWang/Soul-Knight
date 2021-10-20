using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject player;
    public string itemName;
    public float getDistance;
    public float speed;
    
    private Transform target;

    void Start()
    {
        getDistance = 3f;
        speed = 4f;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= getDistance)
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            if (itemName == "coin")
                player.GetComponent<KnightController>().AddCoin();
            else if (itemName == "magicStone" || itemName == "EnergyBottle")
                player.GetComponent<KnightController>().AddMagic();
            else if (itemName == "BloodBottle")
                player.GetComponent<KnightController>().AddHp();
            Destroy(gameObject);
        }
    }
}
