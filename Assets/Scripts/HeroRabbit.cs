using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	Rigidbody2D myBody = null;
	Transform heroParent = null;
	
	bool isGrounded = false;
	bool JumpActive = false;
	bool isBig = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	
	float toGrow = 0;

	// Use this for initialization
	void Start () {
		this.heroParent = this.transform.parent;
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelController.current.setStartPosition (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(toGrow > 0) {
			toGrow -= Time.deltaTime;
			if(toGrow <= 0) {
				toGrow = 0;
				shrink();
			}
		}
		
		float value = Input.GetAxis ("Horizontal");
		
		Animator animator = GetComponent<Animator> ();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}
		
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
		
		if(this.isGrounded) {
			animator.SetBool ("jump", false);
		} else {
			animator.SetBool ("jump", true);
		}

		if(hit) {
			isGrounded = true;
			if(hit.transform != null
			&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
				SetNewParent(this.transform, hit.transform);
			}
		} else {
			isGrounded = false;
			SetNewParent(this.transform, this.heroParent);
		}
		
		
		
		//Намалювати лінію (для розробника)
		Debug.DrawLine (from, to, Color.red);
		
		if(Input.GetButtonDown("Jump") && isGrounded) {
			this.JumpActive = true;
		}
		if(this.JumpActive) {
			//Якщо кнопку ще тримають
			if(Input.GetButton("Jump")) {
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}
		
		

	}

	void FixedUpdate () {
	
	}
	
	public bool isRabbitBig() {
		return isBig;
	}
	
	public void shrink() {
		if(isBig) {
			this.transform.localScale -= new Vector3(1f, 1f, 0);
			isBig = false;
		}
	}
	
	public void grow(float time) {
		if(!isBig) {
			toGrow = time;
			this.transform.localScale += new Vector3(1f, 1f, 0);
			isBig = true;
		}
	}
	
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}
}
