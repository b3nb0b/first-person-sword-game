using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class PlayerMove : MonoBehaviour {
 
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool grounded = false;
	public float rollCooldown = 1f;
	public PlayerAttack playerAttack;
	public Rigidbody rb;
	public Animator CameraAnim;
	Vector3 dashDirection;
	bool airborne = false;
	public float bobbing = 0f;
	public GameObject camera;
	public Vector3 pos;
	
	void Awake () {
	    GetComponent<Rigidbody>().freezeRotation = true;
	    GetComponent<Rigidbody>().useGravity = false;
		rb = GetComponent<Rigidbody>();
		pos = camera.transform.localPosition;
	}
 
	void FixedUpdate () {

	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;

	        Vector3 velocity = rb.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rb.AddForce(velocityChange, ForceMode.VelocityChange);
 
		if (grounded)
		{
	        if (canJump && Input.GetButton("Jump")) {
	            rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				canJump = false;
	        }
		}

	    // We apply gravity manually for more tuning control
	   rb.AddForce(new Vector3 (0, -gravity * rb.mass, 0));
	}

	void Update()
	{
		//run animation
		if (grounded && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f))
		{
			camera.transform.localPosition = new Vector3 (0f, -Mathf.Sin(bobbing += Time.deltaTime * 10f) / 4f, 0f);
		}
		else
		{
			bobbing = 0f;
			camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, pos, 5f * Time.deltaTime);
		}

		//landing
		if (!grounded)
		{
			airborne = true;
		}

		if (grounded && airborne)
		{
			airborne = false;
			canJump = true;
		}

		//check for grounded
		RaycastHit hit;

		if (Physics.SphereCast(rb.transform.position, 0.66f, -transform.up, out hit, 0.4f))
        {
			grounded = true;
        }
		else
		{
			grounded = false;
		}
	}

	// private IEnumerator Roll()
    // { 
	// 	Vector3 dirFlat = rb.velocity;
	// 	dirFlat.Normalize();
	// 	playerAttack.anim.Play("sword_roll");
    //     canJump = false;
	// 	GetComponent<Rigidbody>().AddForce(dirFlat * 90f, ForceMode.Impulse);
	    
    //     yield return new WaitForSeconds(0.1f);

    //     canJump = true;
    //     yield return null;
    // }   
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}