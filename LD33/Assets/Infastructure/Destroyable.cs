﻿using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

	public Transform plant;

	public int health = 100;
	int lastHealth = 0; 
	public Transform firstHit;
	public Transform lastHit;
	public Transform normal;
	public Transform alienPlant;

	public ParticleSystem firstHitEmmiter;
	public ParticleSystem lastHitEmmiter;
	public ParticleSystem smokeEmmiter;
	public ParticleSystem plantEmmitter;
	bool firstHitSwitch = false;
	bool lasthitSwitch = false;

	public AudioSource hit;
	public AudioSource burn;

	public void Update(){

		if(health == 100){
			FullHelath();
		}

		if(health != 100 && health != lastHealth && health > 0){
			if(Random.value > 0.7){

				firstHitEmmiter.Emit(5);
			}else {

				firstHitEmmiter.Emit(1);
			}
		}

		if(health < 100 && !firstHitSwitch){
			firstHitSwitch = true;
			FireFirstHit();
		}

		if(health < 50 && !lasthitSwitch){
			lasthitSwitch = true;
			FireLastHit();
		}

		lastHealth = health;
	}

	public void TryGrow(){
		if(alienPlant != null && !alienPlant.gameObject.activeSelf){
			alienPlant.gameObject.SetActive(true);
			plantEmmitter.Emit(10);
			plantEmmitter.Emit(5);
		}

		if(plant != null){
			plant.gameObject.SetActive(true);
		}
	}

	public void FullHelath(){
		normal.gameObject.SetActive(true);
		firstHit.gameObject.SetActive(false);
		lastHit.gameObject.SetActive(false);
		if(alienPlant != null){
			alienPlant.gameObject.SetActive(false);
		}
	}

	public void FireFirstHit(){
		normal.gameObject.SetActive(false);
		firstHit.gameObject.SetActive(true);
		lastHit.gameObject.SetActive(false);
		firstHitEmmiter.Emit(20);
		hit.Play();
	}

	public void FireLastHit(){
		normal.gameObject.SetActive(false);
		firstHit.gameObject.SetActive(false);
		lastHit.gameObject.SetActive(true);
		StartCoroutine(WaitThenTurnOff(8,lastHitEmmiter));
		StartCoroutine(WaitThenTurnOff(8,smokeEmmiter));
		burn.Play();
	}

	public IEnumerator WaitThenTurnOff(float duration, ParticleSystem emmitter){
		float timer = 0;
		while(timer < duration){
			emmitter.Emit(2);
			timer+=Time.deltaTime;
			yield return null;
		}
		TryGrow();
	}

}
