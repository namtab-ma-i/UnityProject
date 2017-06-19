using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {

	public float time_to_grow = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void OnRabbitHit (HeroRabbit rabbit){
		rabbit.grow(time_to_grow);
		this.CollectedHide ();
	}
}
