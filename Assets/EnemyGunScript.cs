using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    public GameObject bullet;

    public bool CanShot = true;

    private float interval = 0.7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyScript enemy = FindAnyObjectByType<EnemyScript>();

        if (enemy.isView)
        {
            interval -= Time.deltaTime;
            if (interval <= 0 && bullet != null)
            {
                interval = 0.7f;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
