using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keys
{
	UP, DOWN, LEFT, RIGHT, JUMP, ACTION
}

public enum Direction
{
	UP, DOWN, LEFT, RIGHT, UP_RIGHT, DOWN_RIGHT, UP_LEFT, DOWN_LEFT, NONE
}

public class Controller : MonoBehaviour
{
	public Direction lastDirection = Direction.RIGHT; //used in destroyable block placement

	private PhysicsObject physics;
	private Animator anim;
	private SpriteRenderer sprite;
	private InputControl input;
	public ControlType control;
	private TimeStuff time;

	public Camera c;

	public bool moving = false;

	[SerializeField]
	private Direction direction;

	// Use this for initialization
	void Start()
	{
		physics = this.GetComponent<PhysicsObject>();
		anim = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();
		input = new InputControl();
		input.setControlType(control);
		time = this.GetComponent<TimeStuff>();
	}

	public bool checkKeyPressed(Keys k)
	{
		return input.keysPressed.Contains(k);
	}

	public bool checkKeyHeld(Keys k)
	{
		return input.keysHeld.Contains(k);
	}

	public bool checkKeyReleased(Keys k)
	{
		return input.keysReleased.Contains(k);
	}

	public PhysicsObject getCollider()
	{
		return physics;
	}

	private bool checkInput(Keys a, Keys b)
	{
		return input.keysHeld.Contains(a) && input.keysHeld.Contains(b);
	}

	public Direction getCursorDirection()
	{
		if (input.control == ControlType.Keyboard)
		{

			if (checkInput(Keys.RIGHT, Keys.UP))
				return Direction.UP_RIGHT;
			if (checkInput(Keys.RIGHT, Keys.DOWN))
				return Direction.DOWN_RIGHT;
			if (checkInput(Keys.LEFT, Keys.UP))
				return Direction.UP_LEFT;
			if (checkInput(Keys.LEFT, Keys.DOWN))
				return Direction.DOWN_LEFT;
			if (input.keysHeld.Contains(Keys.LEFT))
				return Direction.LEFT;
			if (input.keysHeld.Contains(Keys.RIGHT))
				return Direction.RIGHT;
			if (input.keysHeld.Contains(Keys.UP))
				return Direction.UP;
			if (input.keysHeld.Contains(Keys.DOWN))
				return Direction.DOWN;

		}
		else if (input.control == ControlType.MouseAndKeyboard)
		{
			Vector2 mousePosition = c.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
			bool right = false, left = false, up = false, down = false;
			if (mousePosition.x >= this.transform.position.x + .16)
			{
				right = true;
			}
			if (mousePosition.x <= this.transform.position.x - .16)
			{
				left = true;
			}
			if (mousePosition.y >= this.transform.position.y + .16)
			{
				up = true;
			}
			if (mousePosition.y <= this.transform.position.y - .16)
			{
				down = true;
			}
			if (right && up)
				return Direction.UP_RIGHT;
			if (right && down)
				return Direction.DOWN_RIGHT;
			if (left && up)
				return Direction.UP_LEFT;
			if (left && down)
				return Direction.DOWN_LEFT;
			if (right)
				return Direction.RIGHT;
			if (left)
				return Direction.LEFT;
			if (up)
				return Direction.UP;
			if (down)
				return Direction.DOWN;
		}
		return direction;

	}

	// Update is called once per frame
	void Update()
	{
		input.updateKeys();
		if (getCursorDirection() != Direction.NONE)
		{
			direction = getCursorDirection();
		}
		if (!time.lockAction)
		{
			physics.effectedByGravity = true;
			Move();
		}
		else
		{
			physics.effectedByGravity = false;
			physics.Move(0, 0);
		}
	}

	public void Move()
	{
		float x = 0;
		float y = 0;
		if (input.keysHeld.Contains(Keys.LEFT))
		{
			x = -1;
			sprite.flipX = true;
			lastDirection = Direction.LEFT;
			moving = true;
		}
		if (input.keysHeld.Contains(Keys.RIGHT))
		{
			x = 1;
			sprite.flipX = false;
			lastDirection = Direction.RIGHT;
			moving = true;
		}
		if (input.keysPressed.Contains(Keys.JUMP) && physics.checkGrounded())
			y = 5;

		if (x == 0)
			anim.Play("Idle");
		else
			anim.Play("Run");

		physics.Move(x, y);
	}
}