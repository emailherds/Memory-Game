using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTiles : MonoBehaviour
{
    public GameObject camera;
    public GameObject platform;
    public GameObject block;
    public GameObject character;
    public Text level;

    private float speed = 12f;
    private float speedC = 16f;
    private float zpos = -5f;
    private bool wait = true;
    private int levelNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            spawner(zpos, -0.25f, int.MaxValue, false);
            spawner(zpos, 12f, int.MaxValue, false);
            zpos = zpos + 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (character.transform.position.z < camera.transform.position.z + 12.5)
        {
            character.transform.Translate(Vector3.forward * speedC * Time.deltaTime);
        }
        else
        {
            character.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        camera.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (character.transform.position.z > zpos - 90)
        {
            spawner(zpos, 3f, 8, true);
            spawner(zpos, 9f, 8, true);
            zpos = zpos + 5f;
        }

        if (wait)
        {
            levelNum++;
            level.text = "Level: " + levelNum;
            wait = false;
            speed = speed * 1.1f;
            speedC = speedC * 1.1f;
            StartCoroutine(waitForLevel());
        }
    }

    public void spawner(float z, float y, int fact, bool yesBlock)
    {
        System.Random random = new System.Random();

        int holes = random.Next(1, fact);
        ArrayList a = new ArrayList();

        if (holes <= 5)
        {
            for (int i = 0; i < holes; i++)
                a.Add(random.Next(1, 6));
        }

        GameObject one = null;
        GameObject two = null;
        GameObject three = null;
        GameObject four = null;
        GameObject five = null;

        if (!a.Contains(1))
            one = Instantiate(platform, new Vector3(-4, y, z), Quaternion.identity) as GameObject;
        if (!a.Contains(2))
            two = Instantiate(platform, new Vector3(-2, y, z), Quaternion.identity) as GameObject;
        if (!a.Contains(3))
            three = Instantiate(platform, new Vector3(0, y, z), Quaternion.identity) as GameObject;
        if (!a.Contains(4))
            four = Instantiate(platform, new Vector3(2, y, z), Quaternion.identity) as GameObject;
        if (!a.Contains(5))
            five = Instantiate(platform, new Vector3(4, y, z), Quaternion.identity) as GameObject;
        if (yesBlock)
        {
            int obstacle = random.Next(1, 4);
            ArrayList b = new ArrayList();

            if (obstacle <= 1)
            {
                for (int i = 0; i < obstacle; i++)
                    b.Add(random.Next(1, 6));
            }

            if (b.Contains(1) && one != null)
                one = Instantiate(block, new Vector3(-4, y + 0.75f, z), Quaternion.identity) as GameObject;
            if (b.Contains(2) && two != null)
                two = Instantiate(block, new Vector3(-2, y + 0.75f, z), Quaternion.identity) as GameObject;
            if (b.Contains(3) && three != null)
                three = Instantiate(block, new Vector3(0, y + 0.75f, z), Quaternion.identity) as GameObject;
            if (b.Contains(4) && four != null)
                four = Instantiate(block, new Vector3(2, y + 0.75f, z), Quaternion.identity) as GameObject;
            if (b.Contains(5) && five != null)
                five = Instantiate(block, new Vector3(4, y + 0.75f, z), Quaternion.identity) as GameObject;
        }
    }

    IEnumerator waitForLevel()
    {
        yield return new WaitForSeconds(5);
        wait = true;
    }
}