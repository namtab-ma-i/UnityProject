using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void OnRabbitHit (HeroRabbit rabbit){
		if(rabbit.isRabbitBig()) {
			rabbit.shrink();
		} else {
			LevelController.current.onRabitDeath (rabbit);
		}
		this.CollectedHide ();
	}
	
}
