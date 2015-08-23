using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	Vector3 dir;
	public float speed;
	public int damage;
	bool Launched = false;

	Transform myTarget;

	public void Launch(Transform target){
		dir = (target.position-transform.position).normalized;
		transform.forward = dir;
		Launched = true;
		myTarget = target;
	}

	void Update () {
		if(!Launched){
			return;
		}
		transform.position+=dir*speed*Time.smoothDeltaTime;

		if(Close){
			Health h = myTarget.gameObject.GetComponent<Health>();
			if(h != null){
				h.DoDamage(damage);
			}
			Destroy(gameObject);
		}
	}

	public bool Close{
		get{
			return Vector3.Distance(transform.position,myTarget.position) < 1.0f;
		}
	}

	public void OnDrawGizmos(){
		if(myTarget != null){
			Gizmos.DrawSphere(myTarget.position,1.0f);
			Gizmos.DrawLine(transform.position,myTarget.position);
		}
	}
}
                          