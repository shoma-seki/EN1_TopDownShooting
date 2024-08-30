using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    private Vector3 velocity;
    private float speed = 20;

    private Vector3 rotation;

    private bool isEnd;

    private LineRenderer lineRenderer;
    private Vector3 startP;
    private Vector3 endP;

    EnemyScript enemy;
    EnemyGunScript gun;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindAnyObjectByType<EnemyScript>();
        gun = FindAnyObjectByType<EnemyGunScript>();
        position = transform.position;
        direction = enemy.direction + (enemy.transform.position - gun.transform.position);

        lineRenderer = GetComponent<LineRenderer>();
        endP = gun.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction.normalized * speed * Time.deltaTime;
        position += velocity;
        transform.position = position;

        if (enemy == null)
        {
            startP = transform.position;
            endP = transform.position - direction.normalized * 5f;
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) >= 5f)
            {
                startP = transform.position;
                endP = transform.position - direction.normalized * 5f;
            }
            else
            {
                startP = transform.position;
            }
        }

        lineRenderer.SetPosition(0, startP);
        lineRenderer.SetPosition(1, endP);

        rotation = direction.normalized;
        transform.rotation = Quaternion.LookRotation(rotation);

        DestroySelf();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Switch" ||
            other.gameObject.tag == "MoveWall" || other.gameObject.tag == "Player")
        {
            isEnd = true;
        }
    }

    private void DestroySelf()
    {
        if (isEnd)
        {
            direction = Vector3.zero;
            speed = 0;
            startP = Vector3.Lerp(startP, endP, 0.2f);
            if (Vector3.Distance(startP, endP) < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
