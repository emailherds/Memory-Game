using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private float target;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            target = Mathf.Round(transform.position.x) - 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            target = Mathf.Round(transform.position.x)+ 1;
        }
        if (target <= 2 && target >= -2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target, 0.75f, transform.position.z), Time.deltaTime*5f);
        }
    }
}