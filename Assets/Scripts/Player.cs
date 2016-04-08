using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private GUI gui;
	public Text GoldCount;
	public Text SilverCount;
	private int countGold;
	private int countSilver;

	/// <summary>
	/// Start the game.
	/// </summary>
	void Start ()
	{
        rbody = GetComponent<Rigidbody2D>();
		countGold = 0;
		countSilver = 0;
		GoldCount =  GameObject.Find("countGold").GetComponent<Text>();
		SilverCount =  GameObject.Find("countSilver").GetComponent<Text>();
		GoldCount.text = "Count Gold: 0";
		SilverCount.text = "Count Silver: 0";

		gui = GameObject.Find ("GUI").GetComponent<GUI>();



	}
	

	/// <summary>
	/// Gets called per frame.
	/// </summary>
	void Update ()
    {
        UpdateControls();
        UpdateMovement();
		breaking ();
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
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(GUI.InventoryOpen){
				gui.ShowInventory (false);
			}
			else{gui.ShowInventory (true);}
		}
    }

    /// <summary>
    /// Updates the movement.
    /// </summary>
    private void UpdateMovement()
    {
        rbody.velocity = new Vector2(moveSpeed * direction, rbody.velocity.y);

    }
	private void breaking(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (pos, transform.position);
			if(hit.collider != null){
				// & (hit.collider.gameObject.transform.position.x == rbody.transform.position.x+1.72 || hit.collider.gameObject.transform.position.x == rbody.transform.position.x-1.72) || hit.collider.gameObject.transform.position.y == rbody.transform.position.y+1.72
				if(hit.collider.gameObject.tag == "block"){
					GameObject.Find ("World").GetComponent<WorldGen> ().destroyBlock (hit.collider.gameObject);
					if(hit.collider.gameObject.name == "gold"){
						 countGold = countGold + 1;
						GoldCount.text = "Count Gold: " + countGold.ToString();
					}
					if(hit.collider.gameObject.name == "silver"){
						countSilver = countSilver + 1;
						SilverCount.text = "Count Silver: " + countSilver.ToString();
					}
				}
			}
			
		}
	
	}
    
	/// <summary>
	/// Jumps the player.
	/// </summary>
    private void Jump()
    {
        rbody.AddForce(transform.up * jumpForce);
    }
}
