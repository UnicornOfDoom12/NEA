using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float Walkspeed;
	private Rigidbody2D rb;
	//public Weapon EquippedWeapon;
	static public bool ForwardFacing;

	Animator Animator;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator>();
		
	}
	void WalkHandler (){
		float xinput = Input.GetAxis("Horizontal");
		float yinput = Input.GetAxis("Vertical");
		xinput = xinput * Walkspeed * Time.deltaTime;
		yinput = yinput * Walkspeed * Time.deltaTime;

		Vector3 Vec = new Vector3 (xinput, yinput, 0);
		Vector3 NewPos = transform.position + Vec;
		rb.MovePosition(NewPos);
		if (xinput != 0 || yinput != 0){
			Animator.SetBool("IsWalking",true);
		}
		else{
			Animator.SetBool("IsWalking",false);
		}
	}
	void WalkHandler1 (){
		float xinput = Input.GetAxis("Vertical");
		float yinput = Input.GetAxis("Horizontal");
		rb.AddForce(gameObject.transform.right * Walkspeed * xinput);
		rb.AddForce(gameObject.transform.up * yinput * Walkspeed * -1);

		if (xinput != 0 || yinput != 0){
			Animator.SetBool("IsWalking",true);
		}
		else{
			Animator.SetBool("IsWalking",false);
		}
	}
	void FaceMouse(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Quaternion Rotation = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);

		transform.rotation = Rotation;
		transform.eulerAngles = new Vector3 (0,0, transform.eulerAngles.z + 90);
		rb.angularVelocity = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FaceMouse();
		if (!ForwardFacing){
			WalkHandler1();
		}
		else{
			Walkspeed = 10;
			WalkHandler();
		}
		//ChangeImageValue();
		
		
	}
	public void ChangeImageValue(string Category){
		print(Category.ToString());
		int catint = 0;
		if (Category == "Assault Rifle" || Category == "Marksman Rifle"){
			catint = 1;
			print("chaning weapon type to 1");
		}
		if (Category == "SMG"){
			catint = 3;
			print("chaning weapon type to 3");
		}
		if (Category == "Hand Cannon"){
			catint = 2;
			print("chaning weapon type to 2");
		}
		Animator.SetInteger("WeaponType", catint);
	}
	public void ToggleForward(){
		if (ForwardFacing){
			ForwardFacing = false;
		}
		else{
			ForwardFacing = true;
		}
	}
}
