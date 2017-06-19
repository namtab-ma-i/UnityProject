using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public bool hideAnimation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected virtual void OnRabbitHit(HeroRabbit rabit) {
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if(!this.hideAnimation) {
			HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
			if(rabbit != null) {
				this.OnRabbitHit (rabbit);
			}
		}
	}

	public void CollectedHide() {
		Destroy(this.gameObject);
	}
	
}
