using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlock : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(transform.parent != null)
            {
                StartCoroutine(DestroyRock());
            }
        }
    }

    IEnumerator DestroyRock()
    {
        transform.parent.GetComponent<MeshDestroy>().DestroyMesh();
        transform.parent = null;
        Destroy(transform.parent);
        yield return new WaitForSeconds(1f);
        GameObject[] residus = GameObject.FindGameObjectsWithTag("ToDestroy");
        Debug.Log(residus.Length);
        for (int i = 0; i < residus.Length; i++)
        {
            Destroy(residus[i]);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
        Destroy(gameObject);
    }
}
