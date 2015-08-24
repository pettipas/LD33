using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LoookAt : MonoBehaviour {

	public void Update(){
		transform.LookAt(Camera.main.transform);
	}
}
