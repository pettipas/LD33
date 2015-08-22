using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SmoothOperator : MonoBehaviour {

	public GameObject camera;
	public Transform ourMonster;
	Vector3 dampingVelocity;
	float refVelocity;
	public float targetZOffset;
	
	void Update () {
		
		if(ourMonster == null){
			return;
		}

#if UNITY_EDITOR
		//camera.transform.LookAt(ourMonster.transform);
#endif

		Vector3 targetPosition = ourMonster.position;
		transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, targetPosition.y, targetPosition.z-targetZOffset), ref dampingVelocity,1.0f);
	}
}