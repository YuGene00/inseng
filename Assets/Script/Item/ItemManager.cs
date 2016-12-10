using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //inspector
    public Transform createZoneTrans;
    public GameObject[] childItem;

    //variable


	void Start() {

    }

    IEnumerator CreateItem() {
        while(true) {
            
            yield return null;
        }
    }
}