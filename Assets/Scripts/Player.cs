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
	public Text DynamiteCount;
	public Text PickaxeDurability;

	private int countGold;
	private int countSilver;

	public Pickaxe pickaxe;
	public SpriteRenderer frontSlot;

	public Canvas quitMenu;
	public Button menuText;
	public Button exitText;


	/// <summary>
	/// Start the game.
	/// </summary>
	void Start ()
	{
		countGold = PlayerPrefs.GetInt ("goldCount");
		countSilver = PlayerPrefs.GetInt ("silverCount");

		quitMenu = quitMenu.GetComponent<Canvas> ();
		menuText = menuText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;

		pickaxe = GameObject.Find ("GameManager").GetComponent<PickaxeManager> ().GetPickaxe ();
		frontSlot.sprite = pickaxe.sprite;

		Input.multiTouchEnabled = true;
        rbody = GetComponent<Rigidbody2D>();

		GoldCount =  GameObject.Find("countGold").GetComponent<Text>();
		SilverCount =  GameObject.Find("countSilver").GetComponent<Text>();
		DynamiteCount =  GameObject.Find("dynamiteCount").GetComponent<Text>();
		PickaxeDurability = GameObject.Find ("pickaxeDurability").GetComponent<Text> ();

		GoldCount.text = "Count Gold: " + countGold;
		SilverCount.text = "Count Silver: " + countSilver;
		PickaxeDurability.text = "Pickaxe durability:" + pickaxe.durability;
		DynamiteCount.text = "Tnt: " + PlayerPrefs.GetInt ("dynamiteCount");
		gui = GameObject.Find ("GUI").GetComponent<GUI>();
	}
	

	/// <summary>
	/// Gets called per frame.
	/// </summary>
	void Update ()
    {
		if (pickaxe.durability == 0) {
			PlayerPrefs.SetInt ("goldCount", countGold);
			PlayerPrefs.SetInt ("silverCount", countSilver);
			PlayerPrefs.Save ();
			quitMenu.enabled = true;
		} else {
			if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel (0); }
			UpdateControls ();
			UpdateMovement ();
			breaking ();
		}
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void returnMenu()
	{
		Application.LoadLevel (0);
	}

	/// <summary>
	/// Updates the controls.
	/// </summary>
    private void UpdateControls()
    {
        //Movement Control
		if (Input.touchCount > 0) 
		{
			foreach (Touch touch in Input.touches) 
			{
				if (touch.position.x < Screen.width / 2) 
				{
					direction = -1;
				} else if (touch.position.x > Screen.width / 2) 
				{
					direction = 1;
				}
			}
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

	// changed by Tugberk Arkose in sprint 3
	// Breaks blocks by touch input and uses player position as a base
	private void breaking(){
		int playerX = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.x);
		int playerY = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.y);
		if(Input.GetMouseButtonDown(0)){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (pos, transform.position);
			if(hit.collider != null && Mathf.Abs(hit.collider.gameObject.transform.position.x -rbody.transform.position.x) < 2 &&  Mathf.Abs(hit.collider.gameObject.transform.position.y - rbody.transform.position.y) < 2 ){
				if(hit.collider.gameObject.tag == "block"){
					pickaxe.durability--;
					if (hit.collider.gameObject.name == "tnt") {
						GameObject.Find ("World").GetComponent<WorldGen> ().destroyBlock (hit.collider.gameObject);
						explode ();
						PickaxeDurability.text = "Pickaxe durability:" + pickaxe.durability;
						return;
					}
					GameObject.Find ("World").GetComponent<WorldGen> ().destroyBlock (hit.collider.gameObject);
					if(hit.collider.gameObject.name == "gold"){
						 countGold = countGold + 1;
						GoldCount.text = "Count Gold: " + countGold.ToString();
					}
					if(hit.collider.gameObject.name == "silver"){
						countSilver = countSilver + 1;
						SilverCount.text = "Count Silver: " + countSilver.ToString();
					}
					if (hit.collider.gameObject.name == "life") {
						pickaxe.durability += 10;
					}
					PickaxeDurability.text = "Pickaxe durability:" + pickaxe.durability;
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


	public void explode()
	{
		int playerX = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.x);
		int playerY = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.y);
		for (int x = playerX - 2; x < playerX + 2; x++) {
			for (int y = playerY - 2; y < playerY + 2; y++) {
				RaycastHit2D hit = Physics2D.Raycast (new Vector3(x,y), transform.position);
				if (hit.collider != null && hit.collider.tag == "block") {
					GameObject.Find ("World").GetComponent<WorldGen> ().destroyBlock (hit.collider.gameObject);
					if(hit.collider.gameObject.name == "gold"){
						countGold = countGold + 1;
						GoldCount.text = "Count Gold: " + countGold.ToString();
					}
					if(hit.collider.gameObject.name == "silver"){
						countSilver = countSilver + 1;
						SilverCount.text = "Count Silver: " + countSilver.ToString();
					}
					if (hit.collider.gameObject.name == "life") {
						pickaxe.durability += 10;
					}
					if (hit.collider.gameObject.name == "tnt") {
						explode ();
					}
				}
			}
	}
	}

	//created by Tugberk in sprint 3
	//Not complete yet
	public void useItem(int id)
	{
		int playerX = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.x);
		int playerY = Mathf.FloorToInt(GameObject.FindGameObjectWithTag ("Player").transform.position.y);
		if (id == 0) {
			Item item = GameObject.Find ("GameManager").GetComponent<ItemManager> ().GetItem (id);
			item.amount = PlayerPrefs.GetInt ("dynamiteCount", 5);
			if (item.amount > 0) {
				explode ();
				item.amount--;
				PlayerPrefs.SetInt ("dynamiteCount", item.amount);
				DynamiteCount.text = "TNT: " + item.amount;

			}
			}
		}
	}

