﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void OnRabbitHit (HeroRabbit rabbit){
		LevelController.current.addCoins (1);
		this.CollectedHide ();
	}
	
}
