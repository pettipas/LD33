using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Blaster : MonoBehaviour {

	public List<Transform> casters = new List<Transform>();
	List<GameObject> hits = new List<GameObject>(30);
	List<RaycastHit> uniqueStrikes = new List<RaycastHit>(30);

	public List<RaycastHit> CastAll(){
		hits.Clear();
		uniqueStrikes.Clear();
		casters.ForEach(x=>{
			RaycastHit[] finds = Physics.RaycastAll(x.transform.position,x.transform.forward, 20.0f);
			foreach(RaycastHit h in finds){
				if(!hits.Contains(h.transform.gameObject)){
					hits.Add(h.transform.gameObject);
					uniqueStrikes.Add(h);
				}
			}
		});
		return uniqueStrikes;
	}

	public void OnDrawGizmos(){
		casters.ForEach(x=>{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(x.position,x.position+x.forward*5.0f);
		});
	}

}
