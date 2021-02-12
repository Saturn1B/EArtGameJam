using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    GameObject Player;
    Inventory PlayerInventory;

    [SerializeField]
    float maxDistance;

    [SerializeField]
    LayerMask layer;

    GameObject target;

    bool playParticle;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
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
                target.GetComponent<Ore>().currentTime += 0.01f;
                if (!playParticle)
                {
                    target.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    playParticle = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                target.GetComponent<Ore>().currentTime = 0;
                target.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                playParticle = false;
            }

            if (target.GetComponent<Ore>().currentTime >= target.GetComponent<Ore>().timeToMine)
            {
                target.GetComponent<Ore>().currentTime = 0;
                PlayerInventory.coalNumber += target.GetComponent<Ore>().coalGiven;
                //Destroy(target.gameObject);
                StartCoroutine(DestroyOre(target));
                target = null;
                playParticle = false;
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
            }
        }
    }

    IEnumerator DestroyOre(GameObject ore)
    {
        ore.GetComponent<MeshDestroy>().DestroyMesh();
        Destroy(ore);
        yield return new WaitForSeconds(0.5f);
        GameObject[] residus = GameObject.FindGameObjectsWithTag("ToDestroy");
        Debug.Log(residus.Length);
        for (int i = 0; i < residus.Length; i++)
        {
            Destroy(residus[i]);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
        yield return new WaitForSeconds(1);
    }
}
