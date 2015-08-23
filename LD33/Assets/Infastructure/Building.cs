using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public Transform topFloor;

	void Start () {
		if(Random.value > 0.4f){
			Destroy(topFloor.gameObject);
		}
	}

}
