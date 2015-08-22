using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {
	public GameObject prefab;
	public List<Transform> buildingSpawns = new List<Transform>();
	protected List<GameObject> instances = new List<GameObject>(); 

	public void Awake(){
		BuildStructures();
	}
	[ContextMenu("Build")]
	public void BuildStructures(){
		buildingSpawns.ForEach(loc=>{
			GameObject go = Instantiate(prefab,loc.transform.position,Quaternion.identity) as GameObject;
			instances.Add(go);
		});
	}

	public void OnDrawGizmos(){
		buildingSpawns.ForEach(x=>{
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube(x.position,Vector3.one);
		});
	}
}
