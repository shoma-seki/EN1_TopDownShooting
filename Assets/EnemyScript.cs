using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 direction;
    private Vector3 velocity;
    private float speed = 50;

    private int targetNumber;
    private Vector3 targetPos;

    private int HP = 5;

    public bool isView;
    public bool isSearch;
    public float kSearchTime = 2.0f;
    public float searchTime;

    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        searchTime = kSearchTime;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScript player = FindAnyObjectByType<PlayerScript>();

        Ray ray = new Ray(transform.position, (player.transform.position - transform.position).normalized);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (isView == false && isSearch == false)
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
            speed = 50;
        }
        else if (isView)
        {
            speed = 0;
            direction = player.transform.position - transform.position;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Wall")
                {
                    if (isView)
                    {
                        isSearch = true;
                    }
                }
            }

        }

        if (isSearch)
        {
            if (isView)
            {
                //isSearch = false;
                isView = false;
            }

            searchTime -= Time.deltaTime;
            if (searchTime <= 0)
            {
                isSearch = false;
                isView = false;
            }

            speed = 0;

            Vector3 eulerAngle = Vector3.zero;
            if (searchTime > 1.0f)
            {
                eulerAngle.y = transform.rotation.eulerAngles.y - 2f;
            }
            else
            {
                eulerAngle.y = transform.rotation.eulerAngles.y + 4f;
            }

            direction = Quaternion.Euler(eulerAngle) * Vector3.forward;
        }

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
        Instantiate(particle, new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z), Quaternion.identity);
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