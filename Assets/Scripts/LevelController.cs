using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	
	public static LevelController current;
	public int coins_count = 0;
	
	void Awake() {
		current = this;
	}

	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}
	public void onRabitDeath(HeroRabbit rabit) {
		//При смерті кролика повертаємо на початкову позицію
		rabit.transform.position = this.startingPosition;
	}
	
	public void addCoins (int coins) {
		coins_count += coins;
	}
	
}
