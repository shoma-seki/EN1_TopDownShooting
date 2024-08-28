using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public int wallNumber;
    public int openCount;
    public bool isMove;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (openCount <= 0)
        {
            isMove = true;
        }

        if (isMove)
        {
            position.y -= 0.5f * Time.deltaTime;
            transform.position = position;
        }
    }
}
