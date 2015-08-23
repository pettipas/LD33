using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Animator face;
	public ParticleSystem hitsSystem;
	public AudioSource damaged;
	public int health = 100;

	public void DoDamage (int damage){
		face.Play("damage",0,0);
		if(damaged != null){
			damaged.loop = false;
			damaged.Play();
		}
		if(hitsSystem != null){
			hitsSystem.Emit(damage);
		}
		health-=damage;
	}
}
