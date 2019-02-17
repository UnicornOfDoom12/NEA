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
	void Start () {
		Player = GameObject.Find("Player");
		PlayerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
		SightedPlayer = false;
		ChangeTime = 0;
		DegreesTurned = 0;
		DirectionOfTurn = -1;
		MeleeAttacking = false;
		PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>();
		Dieing = false;
		Health = 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float Passed = Time.deltaTime;
		ChangeTime += Passed;
		if(MeleeAttacking){
			MeleeTimer += Passed;
		}
		if(MeleeTimer >= MeleeCooldown){
			Animator.SetBool("Attacking",false);
			MeleeAttacking = false;
		}
		if(!SightedPlayer && !Dieing){
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D Ray = Physics2D.Raycast(Claw.transform.position, RayDirection);
			if(Ray.collider == PlayerCollider){
				SightedPlayer = true;
				SoundSource.clip = SightedClip;
				SoundSource.Play();
			}
			else{
				Monster.AddForce(gameObject.transform.up * (MoveSpeed /4) * -1);
				if (ChangeTime >= 1.5f){
					transform.Rotate(Vector3.forward * DirectionOfTurn);
					DegreesTurned += 1;
					if (DegreesTurned >= 60){
						DegreesTurned = 0;
						ChangeTime = 0;
						DirectionOfTurn = UnityEngine.Random.Range(-1,1);
						if (DirectionOfTurn == 0){
							DirectionOfTurn = -1;
						}
					}
				}
			}
		}
		if(SightedPlayer && !Dieing){
			Vector3 Rotation = transform.position - Player.transform.position;
			Rotation.Normalize();
         	float rot = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
         	transform.rotation = Quaternion.Euler(0f, 0f, rot - 90);
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D Ray = Physics2D.Raycast(Claw.transform.position, RayDirection);
			Monster.AddForce(gameObject.transform.up * MoveSpeed * -1);
			if(Ray.collider != PlayerCollider && Ray.collider.tag != "Projectile"){
				SightedPlayer = false;
			}
		}
		if(Monster.velocity.x != 0 || Monster.velocity.y != 0){
			Animator.SetBool("Moving",true);
		}
		else if(MeleeAttacking){
			Animator.SetBool("Moving",false);
		}
	}
	public void OnCollisionEnter2D(Collision2D Hit){
		if(Hit.collider == PlayerCollider && !MeleeAttacking){
			MeleeAttack();
		}
	}
	public void OnTriggerStay2D(Collider2D Hit){
		if(Hit == PlayerCollider && !MeleeAttacking){
			MeleeAttack();
		}
	}
	public void MeleeAttack(){
		Animator.SetBool("Moving",false);
		Animator.SetBool("Attacking",true);
		MeleeAttacking = true;
		MeleeTimer = 0;
		Vector2 RayDir = new Vector2(0,transform.rotation.z);
		RaycastHit2D Attack = Physics2D.Raycast(Claw.transform.position, RayDir);
		if (Attack.collider != null && Attack.distance <= 0.7f && Attack.collider != selfCollider){
			if(Attack.collider == PlayerCollider){
				//PlayerDeathHandler.TakeDamage(10);
				SoundSource.clip = Playerhit;
			}
			else{
				SoundSource.clip = Miss;
			}
			SoundSource.Play();
		}
	}
	public void TakeDamage(int damage){
		Health =  Health - damage;
		if (Health <= 0){
			Animator.SetBool("IsDead",true);
			Dieing = true;
			Destroy(gameObject,1.017f);
		}
	}
}
