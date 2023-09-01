using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peicespawner : MonoBehaviour
{
    private peicetype type;
    private peices currentpeice;

   public void Spawn()
    {
       
        currentpeice.gameObject.SetActive(true);
        currentpeice.transform.SetParent(transform, false);
    }
   public
        void deSpawn()
    {
        currentpeice.gameObject.SetActive(false);

    }
}
