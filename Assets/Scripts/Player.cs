using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	/// <summary>
	/// The direction player headed.
	/// </summary>
    public int direction = 0;

	/// <summary>
	/// The predefined inputs.
	/// </summary>
	///

    public KeyCode leftKey, rightKey, jumpKey;
	/// <summary>
	/// The predefined move speed.
	/// </summary>
    public float moveSpeed = 3f;
	/// <summary>
	/// The predefined jump force.
	/// </summary>
    public float jumpForce = 250f;

	/// <summary>
	/// The two dimensional Rigid Body.
	/// </summary>
    private Rigidbody2D rbody;

	/// <summary>
	/// Start the game.
	/// </summary>
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>(); 
	}
	

	/// <summary>
	/// Gets called per frame.
	/// </summary>
	void Update ()
    {
        UpdateControls();
        UpdateMovement();
	}

	/// <summary>
	/// Updates the controls.
	/// </summary>
    private void UpdateControls()
    {
        //Movement Control
        if(Input.GetKey(leftKey))
        {
            direction = -1;
        }
        else if(Input.GetKey(rightKey))
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }

        //Jump controls
        if(Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
    }

    /// <summary>
    /// Updates the movement.
    /// </summary>
    private void UpdateMovement()
    {
        rbody.velocity = new Vector2(moveSpeed * direction, rbody.velocity.y);

    }
    
	/// <summary>
	/// Jumps the player.
	/// </summary>
    private void Jump()
    {
        rbody.AddForce(transform.up * jumpForce);
    }
}
