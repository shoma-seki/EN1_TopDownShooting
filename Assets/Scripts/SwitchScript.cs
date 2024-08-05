using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public int switchNumber;
    private int HP = 2;

    [SerializeField] private GameObject breakParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            GameObject[] moveWalls = GameObject.FindGameObjectsWithTag("MoveWall");
            foreach (GameObject moveWall in moveWalls)
            {
                if (moveWall.GetComponent<MoveWall>().wallNumber == switchNumber)
                {
                    moveWall.GetComponent<MoveWall>().isMove = true;
                    DestroySelf();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP--;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        Instantiate(breakParticle, transform.position, Quaternion.identity);
    }
}
