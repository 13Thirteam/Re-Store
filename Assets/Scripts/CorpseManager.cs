using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseManager : MonoBehaviour
{
    [SerializeField] GameObject corpse;

    int currentLayer = 0;

    public void makeCorpse(Vector3 pos) //instantiates corpse object under corpseManager object
    {
        GameObject currentCorpse = Instantiate(corpse, pos, Quaternion.identity, transform);
        //currentCorpse.GetComponent<SpriteRenderer>().sortingOrder = currentLayer;
        //currentLayer++;
    }
}
