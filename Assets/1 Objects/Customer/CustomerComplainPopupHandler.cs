using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerComplainPopupHandler : MonoBehaviour
{
    [SerializeField] private GameObject complainPrefab = null;

    public void Complain()
    {
        StartCoroutine(SpawnComplaint());
    }
    
    private IEnumerator SpawnComplaint()
    {
        GameObject complait = Instantiate<GameObject>(complainPrefab, this.transform);
        yield return new WaitForSeconds(5);
        Destroy(complait); 
    }
}
