using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandler : MonoBehaviour {

	// Use this for initialization
	public int Damage;
	public int MoveSpeed;
	public float MeleeTimer;
	public float MeleeCooldown;
	public bool SightedPlayer;
	public GameObject Claw;
	public GameObject Player;
	public Collider2D PlayerCollider;
	public Rigidbody2D Monster;
	public Animator Animator;
	public AudioSource SoundSource;
	public AudioClip SightedClip;
	public float ChangeTime;
	private float DegreesTurned;
	private int DirectionOfTurn;
	public bool MeleeAttacking;
	public Collider2D selfCollider;
	public AudioClip Playerhit;
	public AudioClip Miss;
	public PlayerDeathHandler PlayerDeathHandler;
	public int Health;
	public bool Dieing;
	void Start () { // run at start
		Player = GameObject.Find("Player"); // finds the player
		PlayerCollider = GameObject.Find("Player").GetComponent<Collider2D>(); // finds player collider
		SightedPlayer = false; // set to false at start
		ChangeTime = 0;
		DegreesTurned = 0; // set to initial values
		DirectionOfTurn = -1;
		MeleeAttacking = false;
		PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>(); // gets the player death handler
		Dieing = false;
		Health = 100; // starting health
	}
	void FixedUpdate () { // run once per frame used for physics.
		float Passed = Time.deltaTime; // time past since last call
		ChangeTime += Passed;// change time used to change direction of movement
		if(MeleeAttacking){ // if MeleeAttacking add to the timer
			MeleeTimer += Passed;
		}
		if(MeleeTimer >= MeleeCooldown){ // If we have surpassed the cooldown
			Animator.SetBool("Attacking",false); // stop the animation of attacking
			MeleeAttacking = false; // set bool to false
		}
		if(!SightedPlayer && !Dieing){ // if we havent seen the player, and we are not dieing
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D Ray = Physics2D.Raycast(Claw.transform.position, RayDirection); // cast a ray to the player
			if(Ray.collider == PlayerCollider){ // if the first thing we hit is the player then we can see them
				SightedPlayer = true; // set to true
				SoundSource.clip = SightedClip;
				SoundSource.Play(); // play a sound file
			}
			else{ // if we cannot see the player
				Monster.AddForce(gameObject.transform.up * (MoveSpeed /4) * -1); // Move the Monster forward
				if (ChangeTime >= 1.5f){ // if it is time to change direction
					transform.Rotate(Vector3.forward * DirectionOfTurn); // Rotate the monster by 1 degree in the correct direction
					DegreesTurned += 1; // increment this by 1
					if (DegreesTurned >= 60){ // if we have turned by 60 degrees
						DegreesTurned = 0; // reset this counter
						ChangeTime = 0; // reset the timer
						DirectionOfTurn = UnityEngine.Random.Range(-1,1); // randomly pick weather or not to reverse direction
						if (DirectionOfTurn == 0){ // if it = 0 then change reset direction change so that we have 2/3 chance to change direction
							DirectionOfTurn = -1;
						}
					}
				}
			}
		}
		if(SightedPlayer && !Dieing){ // if we can see the player
			Vector3 Rotation = transform.position - Player.transform.position;// Get vector to player
			Rotation.Normalize(); // Turn into a unit vector
         	float rot = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg; // Turn into a rotation relative to correct axis
         	transform.rotation = Quaternion.Euler(0f, 0f, rot - 90); // face the player
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y); // cast a ray to check line of sight
			RaycastHit2D Ray = Physics2D.Raycast(Claw.transform.position, RayDirection);
			Monster.AddForce(gameObject.transform.up * MoveSpeed * -1); // Add a force on the montser, move it toward the player
			if(Ray.collider != PlayerCollider && Ray.collider.tag != "Projectile"){ // if we cannot see the player anymore
				SightedPlayer = false; // no longer able to see player
			}
		}
		if(Monster.velocity.x != 0 || Monster.velocity.y != 0){ // if the monster is moving change animation
			Animator.SetBool("Moving",true);
		}
		else if(MeleeAttacking){ // if we are melee attacking set moving false
			Animator.SetBool("Moving",false);
		}
	}
	public void OnCollisionEnter2D(Collision2D Hit){ 
		if(Hit.collider == PlayerCollider && !MeleeAttacking){ // if the player touches the monster
			MeleeAttack(); // hit the player
		}
	}
	public void OnTriggerStay2D(Collider2D Hit){
		if(Hit == PlayerCollider && !MeleeAttacking){ // if the player remains in the trigger area
			MeleeAttack(); // hit the player
		}
	}
	public void MeleeAttack(){
		Animator.SetBool("Moving",false); // set moving false and attacking to true
		Animator.SetBool("Attacking",true);
		MeleeAttacking = true; // melee attacking to true to prevent wrong animations and such
		MeleeTimer = 0; // reset the timer
		Vector2 RayDir = new Vector2(0,transform.rotation.z);
		RaycastHit2D Attack = Physics2D.Raycast(Claw.transform.position, RayDir); // cast a ray to the player
		if (Attack.collider != null && Attack.distance <= 0.7f && Attack.collider != selfCollider){ // if the player is close enough to the monster
			if(Attack.collider == PlayerCollider){ // If we hit the player
				PlayerDeathHandler.TakeDamage(10); // Do 10 damage to the player
				SoundSource.clip = Playerhit; // play the correct sound
			}
			else{
				SoundSource.clip = Miss; // play the sound for missing
			}
			SoundSource.Play(); // play the sound assigned
		}
	}
	public void TakeDamage(int damage){ // when the monster takes damage
		Health =  Health - damage; // take away the damage from health
		if (Health <= 0){ // if the monster is dead
			Animator.SetBool("IsDead",true); // play death animation
			Dieing = true; // set dieing to true so monster cant move anymore
			Destroy(gameObject,1.017f); // destroy object after animation has been played.
		}
	}
}
