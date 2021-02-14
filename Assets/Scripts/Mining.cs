using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    GameObject Player;
    Inventory PlayerInventory;
    OreManager oreManager;

    [SerializeField]
    float maxDistance;

    [SerializeField]
    LayerMask layer;

    GameObject target;

    bool playParticle;

    public Animator Drill;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
        oreManager = GameObject.Find("OreManager").GetComponent<OreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, gameObject.transform.GetChild(0).transform.forward, out hit, maxDistance, layer))
        {
            target = hit.transform.gameObject;

            if (Input.GetMouseButton(0))
            {
                target.GetComponent<Ore>().currentTime += Time.deltaTime;
                if (!playParticle)
                {
                    target.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    playParticle = true;
                }
                Drill.SetBool("drilling", true);
                transform.GetChild(3).transform.LookAt(target.transform.position);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target.GetComponent<Ore>().currentTime = 0;
                target.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                playParticle = false;
                Drill.SetBool("drilling", false);
                transform.GetChild(3).transform.localEulerAngles = Vector3.zero;
            }

            if (target.GetComponent<Ore>().currentTime >= target.GetComponent<Ore>().timeToMine)
            {
                target.GetComponent<Ore>().currentTime = 0;
                if(PlayerInventory.coalNumber < PlayerInventory.maxCoalNumber)
                {
                    int temp = PlayerInventory.coalNumber + target.GetComponent<Ore>().coalGiven;
                    if(temp > PlayerInventory.maxCoalNumber)
                    {
                        PlayerInventory.coalNumber = PlayerInventory.maxCoalNumber;
                    }
                    else
                    {
                        PlayerInventory.coalNumber = temp;
                    }
                }
                //Destroy(target.gameObject);
                StartCoroutine(DestroyOre(target));
                target = null;
                playParticle = false;
                Drill.SetBool("drilling", false);
                transform.GetChild(3).transform.localEulerAngles = Vector3.zero;
            }
        }
        else
        {
            if(target != null)
            {
                target.GetComponent<Ore>().currentTime = 0;
                target.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                target = null;
                playParticle = false;
                Drill.SetBool("drilling", false);
                transform.GetChild(3).transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    IEnumerator DestroyOre(GameObject ore)
    {
        ore.GetComponent<MeshDestroy>().DestroyMesh();
        oreManager.ores.Remove(ore);
        Destroy(ore);
        yield return new WaitForSeconds(0.5f);
        GameObject[] residus = GameObject.FindGameObjectsWithTag("ToDestroy");
        Debug.Log(residus.Length);
        for (int i = 0; i < residus.Length; i++)
        {
            Destroy(residus[i]);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
    }
}
