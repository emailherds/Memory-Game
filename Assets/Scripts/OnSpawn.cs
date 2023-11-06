using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class OnSpawn : MonoBehaviour
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
        if (gameObject.transform.position.z + 10 <= character.transform.position.z)
            Destroy(gameObject);
      
    }
}
