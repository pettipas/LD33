using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health = 100;
	public AudioSource shot;
	public AudioSource motion;
	public Animator animaton;
	public Transform launchPoint;
	public GameObject target;
	public UnityEngine.AI.NavMeshAgent agent;
	public Projectile projectile;
	public float shotTimer;
	public float closeenough;

	public Transform geometry;
	public Transform deathPose;
	public ParticleSystem plantBlood;
	public ParticleSystem smoke;
	public ParticleSystem fire;
	public AudioSource plantSound;

	void Start(){
		lastHealth = health;
		target = Control.PlayerInstance.GetComponentInChildren<EyeWeapon>().gameObject;
		StartCoroutine(ShootWhenClose());
	}
	int lastHealth;
	bool started;
	void Update () {

		if(started){
			return;
		}

		if(target != null && !CloseEnough){
			PlayWalk();
			agent.updateRotation = true;
			agent.Resume();
			agent.SetDestination(target.transform.position);

		}else if(CloseEnough){
			PLayRest();
			agent.updateRotation = false;
			agent.Stop();
			FaceOpponent();
		}

		if(health <= 0 && !started){
			agent.Stop();
			started = true;
			agent.enabled = false;
			StartCoroutine(DeathRoutine());
		}

		if(!started && health!= lastHealth){
			Hit();
		}

		lastHealth = health;
	}

	public void Hit(){
		if(fire != null && Random.value > 0.6f){
			fire.Emit(3);
		}
	}

	public IEnumerator DeathRoutine(){

		if(smoke != null){
			smoke.Emit (10);
		}
		if(fire != null){
			fire.Emit(10);
		}

		plantSound.Play();
		yield return new WaitForSeconds(1.0f);
		geometry.gameObject.SetActive(false);
		deathPose.gameObject.SetActive(true);
		plantBlood.Emit(10);
		yield return new WaitForSeconds(10.0f);
		Destroy(gameObject);
		deathPose.parent = null;
	}

	void PlayWalk(){
		if(motion != null){
			motion.Play();
		}
		if(animaton != null){
			animaton.Play("walks");
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
		if(started){
			yield break;
		}
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
