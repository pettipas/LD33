using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {
	public GameObject prefab;
	public GameObject specialPrefab;

	public List<Transform> buildingSpawns = new List<Transform>();
	protected List<GameObject> instances = new List<GameObject>(); 

	public Transform SpecialPosition;

	public void Awake(){
		BuildStructures();
	}


	public void BuildStructures(){

		Transform t = buildingSpawns[Random.Range(0,buildingSpawns.Count)];
		buildingSpawns.Remove( t);
		SpecialPosition = t;

		buildingSpawns.ForEach(loc=>{
			GameObject go = Instantiate(prefab,loc.transform.position,Quaternion.identity) as GameObject;
			instances.Add(go);
		});

		GameObject specialGuy = Instantiate(specialPrefab,SpecialPosition.position,Quaternion.identity) as GameObject;
		instances.Add(specialGuy);
	}

	public void OnDrawGizmos(){
		buildingSpawns.ForEach(x=>{
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube(x.position,Vector3.one);
		});
	}
}
