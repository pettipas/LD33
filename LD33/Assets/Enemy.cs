using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public AudioSource shot;
	public AudioSource motion;
	public Animator animaton;
	public Transform launchPoint;
	public GameObject target;
	public NavMeshAgent agent;
	public Projectile projectile;
	public float shotTimer;
	public float closeenough;

	void Start(){
		target = Control.PlayerInstance.GetComponentInChildren<EyeWeapon>().gameObject;
		StartCoroutine(ShootWhenClose());
	}

	void Update () {
		if(target != null && !CloseEnough){
			PlayWalk();
			agent.updateRotation = true;
			agent.SetDestination(target.transform.position);
		}

		if(CloseEnough){
			PLayRest();
			agent.updateRotation = false;
			agent.Stop();
			FaceOpponent();
		}
	}

	void PlayWalk(){
		if(motion != null){
			motion.Play();
		}
		if(animaton != null){
			animaton.Play("walk");
		}
	}
	void PLayRest(){
		if(animaton != null){
			animaton.Play("rest");
		}
	}

	void FaceOpponent(){
		Quaternion lookThere = Quaternion.LookRotation(EnemyDirection);
		transform.rotation = Quaternion.RotateTowards(transform.rotation,lookThere,4);
	}

	Vector3 EnemyDirection{
		get{
			Vector3 dir = -(transform.position-target.transform.position).normalized;
			return new Vector3(dir.x,0,dir.z);
		}
	}


	bool CloseEnough{
		get{
			return Vector3.Distance(target.transform.position,transform.position) <=closeenough;
		}
	}

	
	bool CloseEnoughToShoot{
		get{
			return Vector3.Distance(target.transform.position,transform.position) <=closeenough+1;
		}
	}


	IEnumerator ShootWhenClose(){
		if(CloseEnoughToShoot){
			shot.loop = false;
			shot.Play();
			Projectile proj = Instantiate(projectile,launchPoint.position,Quaternion.identity) as Projectile;
			proj.Launch(target.transform);
		}
		yield return new WaitForSeconds(shotTimer);
		StartCoroutine(ShootWhenClose());
	}
}
