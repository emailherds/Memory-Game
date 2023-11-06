using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TileSpawn : MonoBehaviour
{
    public GameObject camera;
    public GameObject platform;
    public GameObject character;
    public GameObject fallingPlatform;
    
    public TextMeshProUGUI level;
    public TextMeshProUGUI numbers;

    public Material mat1;
    public Material mat2;
    public Material mat3;

    private float speed = 12f;
    private float speedC = 16f;
    private float zpos = -5f;

    private bool wait = false;
    private bool startedLevel = false;
    private bool startedLevel2 = false;
    private bool onPlat = false;
    private bool lost = false;
    private bool canDrop = false;

    private int levelNum = 0;
    private int nonDrop = 0;

    private float guessTime = 3.5f;
    private ArrayList list;

    private bool hasMade = false;
    // Start is called before the first frame update
    void Start()
    {
        list = new ArrayList();
        for (int i = 0; i < 20; i++)
        {
            spawner(zpos, 0, false);
            zpos = zpos + 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedLevel)
        {
            StartCoroutine(playThrough());
            startedLevel = true;
        }
        else if(wait && guessTime >= 0f)
        {
            guessTime = guessTime - Time.deltaTime;
            guessTime = Mathf.Round(guessTime * 1000.0f) / 1000.0f;
            numbers.text = guessTime.ToString();
        }else if(guessTime <= 0f)
        {
            numbers.text = "";
        }

        if (character.transform.position.z < camera.transform.position.z + 8)
        {
            character.transform.Translate(Vector3.forward * speedC * Time.deltaTime);
        }
        else
        {
            character.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        camera.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (character.transform.position.z > zpos - 40)
        {
            if (!canDrop) { 
                spawner(zpos, 0, false);
                spawner(zpos, 0, false);
            }
            else if (canDrop)
            {
                spawner(zpos, nonDrop, true);
                spawner(zpos, nonDrop, true);
            }
            zpos = zpos + 3f;
        }
    }

    public void spawner(float z, int fact, bool fall)
    {
        System.Random random = new System.Random();

        ArrayList a = new ArrayList();
    
        GameObject one = null;
        GameObject two = null;
        GameObject three = null;
        GameObject four = null;
        GameObject five = null;


        if (fact != 1 && fall == true)
        {
            one = Instantiate(fallingPlatform, new Vector3(-2, 0, z), Quaternion.identity) as GameObject;
        }else
        {
            one = Instantiate(platform, new Vector3(-2, 0, z), Quaternion.identity) as GameObject;
        }

        if (fact != 2 && fall == true)
        {
            two = Instantiate(fallingPlatform, new Vector3(-1, 0, z), Quaternion.identity) as GameObject;
        }
        else
        {
            two = Instantiate(platform, new Vector3(-1, 0, z), Quaternion.identity) as GameObject;
        }

        if (fact != 3&& fall == true)
        {
            three = Instantiate(fallingPlatform, new Vector3(0, 0, z), Quaternion.identity) as GameObject;
        }
        else 
        {
            three = Instantiate(platform, new Vector3(0, 0, z), Quaternion.identity) as GameObject;
        }

        if (fact != 4 && fall == true)
        {
            four = Instantiate(fallingPlatform, new Vector3(1, 0, z), Quaternion.identity) as GameObject;
        }
        else 
        {
            four = Instantiate(platform, new Vector3(1, 0, z), Quaternion.identity) as GameObject;
        }

        if (fact != 5 && fall == true)
        {
            five = Instantiate(fallingPlatform, new Vector3(2, 0, z), Quaternion.identity) as GameObject;
        }
        else
        {
            five = Instantiate(platform, new Vector3(2, 0, z), Quaternion.identity) as GameObject;
        }

        one.GetComponent<MeshRenderer>().material = mat1;
        two.GetComponent<MeshRenderer>().material = mat2;
        three.GetComponent<MeshRenderer>().material = mat3;
        four.GetComponent<MeshRenderer>().material = mat2;
        five.GetComponent<MeshRenderer>().material = mat1;
    }

    public void listMake(int x)
    {
        System.Random random = new System.Random();
        for(int i = 0; i<x; i++)
        {
            list.Add(random.Next(1, 6));
        }
    }

    public void Reset()
    {
        startedLevel = false;
        levelNum++;
        level.text = "Level: "+(levelNum+1);
        list.Clear();
        wait = false;
        guessTime = 3.5f;
    }

    IEnumerator levelPlay()
    {
        int index = list.Count;
        int ind = 0;
        while (index > 0)
        {
            yield return new WaitForSeconds(3f); 
            int actual = (int)list[ind];
            int pos = (int)character.transform.position.x+3;
            if (pos != actual)
            {
                lost = true;
            }
            nonDrop = actual;
            canDrop = true;
            index--;
            ind++;
            yield return new WaitForSeconds(1.5f);
            canDrop = false;
            Debug.Log(index);
        }
        yield return new WaitForSeconds(3f);
        Reset();
    }

    IEnumerator playThrough()
    {
        numbers.text = "";
        yield return new WaitForSeconds(1);
        int listSize = levelNum + 2;
        listMake(listSize);
        string a = "";

        for (int i = 0; i < listSize; i++)
        {
            if(i == listSize-1)
                a += list[i];
            else
                a += list[i] + ", ";
        }
        numbers.text = a;
        hasMade = true;
        StartCoroutine(levelPlay());
        yield return new WaitForSeconds(3);
        wait = true;
    }
}
