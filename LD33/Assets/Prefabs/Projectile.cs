using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	Vector3 dir;
	public float speed;
	public int damage;
	bool Launched = false;

	Transform myTarget;
	float timer = 0;
	float lifeTime = 10;

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
		timer+=Time.deltaTime;

		transform.position+=dir*speed*Time.smoothDeltaTime;

		if(Close){
			Health h = myTarget.gameObject.GetComponent<Health>();
			if(h != null){
				h.DoDamage(damage);
			}
			Destroy(gameObject);
		}

		if(timer > lifeTime && gameObject!=null){
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
                          