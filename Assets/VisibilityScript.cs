using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityScript : MonoBehaviour
{
    public GameObject Question;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<EnemyScript>().isView = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<EnemyScript>().isSearch = true;
            transform.parent.GetComponent<EnemyScript>().searchTime = 2.0f;
            Instantiate(Question, new Vector3(transform.parent.position.x, transform.parent.position.y + 1.5f, transform.parent.position.z), Quaternion.identity);
        }
    }
}
