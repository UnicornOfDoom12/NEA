using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float Walkspeed;
	private Rigidbody2D rb;
	static public bool ForwardFacing;
	Animator Animator;
	void Start () { // Start, run at the start of the scene
		rb = GetComponent<Rigidbody2D>(); // inherits the rigidbody of the player
		Animator = GetComponent<Animator>(); // Inherits the animator of the player
	}
	void WalkHandler (){ // First method of walking, player walks towards the mouse
		float xinput = Input.GetAxis("Horizontal"); // Gets the inputs of the user in X direction
		float yinput = Input.GetAxis("Vertical"); // Gets the inputs of the user in Y direction
		xinput = xinput * (Walkspeed/40); // Multiplies the values by walkspeed
		yinput = yinput * (Walkspeed/40); // Multiplies the values by walkspeed,

		Vector3 Vec = new Vector3 (xinput, yinput, 0); // Makes a new vector out of the inputs
		Vector3 NewPos = transform.position + Vec; // Creates a new position of of the vector + transform
		rb.MovePosition(NewPos); // Uses a rigidbody method to move over there
		if (xinput != 0 || yinput != 0){
			Animator.SetBool("IsWalking",true); // Sets is walking to true if player is moving
		}
		else{
			Animator.SetBool("IsWalking",false); // else sets to false.
		}
	}
	void WalkHandler1 (){ // second method of walking player moves in a constant directions
		float xinput = Input.GetAxis("Vertical"); // gets the inputs 
		float yinput = Input.GetAxis("Horizontal"); // gets the inputs
		rb.AddForce(gameObject.transform.right * Walkspeed * xinput);  // adds a force to the players based on those inputs, multiplied by walkspeed
		rb.AddForce(gameObject.transform.up * yinput * Walkspeed * -1); // adds a force to the player based on inputs, multiplied by walkspeed
		if (xinput != 0 || yinput != 0){
			Animator.SetBool("IsWalking",true); // Sets walking to true if the player is moving
		}
		else{
			Animator.SetBool("IsWalking",false); // sets walking to false if the player isnt moving
		}
	}
	void FaceMouse(){ // rotates the player to face the mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets the mouses position as a vector
		Quaternion Rotation = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward); // Creates a new rotation that is looking toward the mouse
		transform.rotation = Rotation; // sets the players rotation to equal the new rotation
		transform.eulerAngles = new Vector3 (0,0, transform.eulerAngles.z + 90); // This creates an off set of 90 degrees due to sprite orientation
		rb.angularVelocity = 0; // Sets angluar velocity to 0 to prevent continous spinning
	}
	void FixedUpdate () { // Works similar to update but always at a fixed time
		FaceMouse(); // Makes player face mouse
		if (!ForwardFacing){
			WalkHandler1(); // Based on the users preference use the correct walkhandler
		}
		else{
			Walkspeed = 10; // adjusts walkspeed here to balance the two movements
			WalkHandler(); // Based on the users preference use the correct walkhandler
		}
	}
	public void ChangeImageValue(string Category){ // Is run in the Weapon script, changes players animation
		int catint = 0; 
		if (Category == "Assault Rifle" || Category == "Marksman Rifle"){ // based on the category of weapon change the animation
			catint = 1;
		}
		if (Category == "SMG"){
			catint = 3;
		}
		if (Category == "Hand Cannon"){
			catint = 2;
		}
		Animator.SetInteger("WeaponType", catint); // set the parameter to the correct weapon type
	}
	public void ToggleForward(){ // Is run in the menu, switched the value of the forward facing variable.
		if (ForwardFacing){
			ForwardFacing = false;
		}
		else{
			ForwardFacing = true;
		}
	}
}
