using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class OnSpawn2 : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("MainCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z - 5 <= character.transform.position.z)
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        if (gameObject.transform.position.z + 10 <= character.transform.position.z)
            Destroy(gameObject);
    }
}
