using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public Transform topFloor;
	public Plant plant;

	void Start () {
		if(Random.value > 0.4f && plant==null){
			Destroy(topFloor.gameObject);
		}
	}

}
