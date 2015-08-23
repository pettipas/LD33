using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EyeWeapon : MonoBehaviour {

	public AudioSource mcbeam;
	public AudioSource beamstart;
	public AudioSource beamStop;
	public Blaster blaster;
	public GameObject curser;
	public Transform hitObject;
	public float deadZone;
	public Renderer beamRenderer;
	public Transform eyeCore;
	float degreeDelta = 3.0f;
	public float ideal = 3.0f;
	public float fireing = 1.0f;
	public ParticleSystem effect;

	public void Update(){
		Camera main = Camera.main;
		if(main == null){
			return;
		}

		if(Input.GetMouseButtonDown(0) && !beamRenderer.enabled){
			mcbeam.Play();
			beamstart.Play(); 
			beamRenderer.enabled = true;
			degreeDelta = fireing;
		}

		if(Input.GetMouseButtonUp(0) && beamRenderer.enabled){
			mcbeam.Stop();
			beamStop.Play();
			beamRenderer.enabled = false;
			degreeDelta = ideal;
		}

		if(beamRenderer.enabled){

			List<RaycastHit> hits = blaster.CastAll();
			for(int i=0; i < hits.Count;i++){
				Destroyable dest = hits[i].transform.gameObject.GetComponentInParent<Destroyable>();
				Enemy enemy = hits[i].transform.gameObject.GetComponentInParent<Enemy>();
				if(dest != null){
					dest.health--;
				}
				if(enemy != null){
					enemy.health--;
				}
				effect.transform.position = hits[i].point;
				effect.Emit(5);
			}
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
