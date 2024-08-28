using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction;
    private Vector3 velocity;
    private float speed = 100;

    private int targetNumber;
    private Vector3 targetPos;

    private int HP = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<EnemyMovePointScript>().targetNumber == targetNumber)
            {
                targetPos = target.transform.position;
            }
        }

        if (targetNumber > 4)
        {
            targetNumber = 0;
        }

        //à⁄ìÆèàóù
        direction = targetPos - transform.position;
        velocity = direction.normalized * speed * Time.deltaTime;
        rb.velocity = velocity;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction.normalized)
            , 300f * Time.deltaTime);

        if (HP <= 0)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (other.GetComponent<EnemyMovePointScript>().targetNumber == targetNumber)
            {
                targetNumber++;
            }
        }

        if (other.gameObject.tag == "Bullet")
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Damage");
            HP--;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        GameObject[] moveWalls = GameObject.FindGameObjectsWithTag("MoveWall");
        foreach (GameObject moveWall in moveWalls)
        {
            if (moveWall.GetComponent<MoveWall>().wallNumber == 4)
            {
                moveWall.GetComponent<MoveWall>().isMove = true;
            }
        }
    }
}
