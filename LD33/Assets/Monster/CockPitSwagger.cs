﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CockPitSwagger : MonoBehaviour {

	public List<Transform> influence = new List<Transform>();
	public float cockPitOffSet =  1.5f;
	public void Update(){
		Vector3 averagePos = Vector3.zero;

		influence.ForEach(x=>{
			averagePos+= x.transform.position;
		});

		averagePos = averagePos/influence.Count;
		transform.position = new Vector3(averagePos.x,averagePos.y + cockPitOffSet,averagePos.z);
	}
}