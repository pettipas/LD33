using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EyeWeapon : MonoBehaviour {

	public GameObject curser;
	public Transform hitObject;
	public float deadZone;
	public Renderer beamRenderer;
	public Transform eyeCore;
	float degreeDelta = 3.0f;
	public float ideal = 3.0f;
	public float fireing = 1.0f;
	public void Update(){
		Camera main = Camera.main;
		if(main == null){
			return;
		}

		if(Input.GetMouseButtonDown(0) && !beamRenderer.enabled){
			beamRenderer.enabled = true;
			degreeDelta = fireing;
		}

		if(Input.GetMouseButtonUp(0) && beamRenderer.enabled){
			beamRenderer.enabled = false;
			degreeDelta = ideal;
		}

		Ray ray = main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit)) {
			Vector3 mPos = hit.point;
			if(curser)curser.transform.position = mPos;
			hitObject = hit.transform;
		}

		if(Vector3.Distance(curser.transform.position,transform.position)>deadZone){
			Quaternion lookat = Quaternion.LookRotation((curser.transform.position-transform.position).normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,lookat,degreeDelta);
		}else {
			transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.identity,degreeDelta);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireSphere(new Vector3(transform.position.x,0,transform.position.z),deadZone);
	}
}
