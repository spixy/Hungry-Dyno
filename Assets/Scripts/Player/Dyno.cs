using UnityEngine;
using UnityStandardAssets._2D;

public enum DynoState
{
	Death,
	Normal,
	Godmode,
	Berserk
}

/// <summary>
/// Pohyb dyna
/// </summary>
public class Dyno : MonoBehaviour
{
	[SerializeField] private Animator anim;

	[SerializeField] private SpriteRenderer spriteRenderer;

	[SerializeField] private const float baseDur = 10f;

	[SerializeField] private float godmodeDur = baseDur;

	[SerializeField] private float berserkDur = baseDur;

	[SerializeField] private Attack attack;

	[SerializeField] private float hpDecay = 20f;

	[SerializeField] private Vector3 startPos;

	private PlatformerCharacter2D platformerCharacter2d;

	private float lastPosX = 0f;

	private const float FACTOR_CAP = 3f;

	public float Hp { get; private set; }

	public bool Alive
	{
		get { return Hp > 0; }
	}

	public bool Attacking { get; set; }
	public int AttackCd { get; set; }

	private DynoState _state;

	public DynoState State
	{
		get { return _state; }
		set
		{
			_state = value;
			GameManager.Instance.Gui.GameMenu.SetIcon(value);
		}
	}

	void Awake()
	{
		ResetStartingPosition();
		platformerCharacter2d = gameObject.GetComponent<PlatformerCharacter2D>();
	}

	private void ResetStartingPosition()
	{
		transform.position = Camera.main.ViewportToWorldPoint(startPos); // orthographic -> perspective
	}

	public void StartGame()
	{
		ResetStartingPosition();
		Hp = 100;
		lastPosX = transform.position.x;
		State = DynoState.Normal;
		platformerCharacter2d.SetFactor(1f);
		spriteRenderer.enabled = true;
	}

	void Update()
	{
		switch (State)
		{
			case DynoState.Death:
				return;

			case DynoState.Godmode:
				godmodeDur -= Time.deltaTime;
				if (godmodeDur < 0)
				{
					DisableGodmode();
				}
				break;

			case DynoState.Berserk:
				berserkDur -= Time.deltaTime;
				if (berserkDur < 0)
				{
					DisableBerserk();
				}
				break;
		}

		float factor = transform.position.x / 500; // 500 is a magic constant

		// make sure factor is between 1 and FACTOR_CAP
		factor = Mathf.Clamp(factor, 1f, FACTOR_CAP);

		GameManager.Instance.Score += (transform.position.x - this.lastPosX) * 0.25f * factor;
		this.lastPosX = transform.position.x;

		UpdateHP(Time.deltaTime * factor * -hpDecay);

		platformerCharacter2d.SetFactor(factor);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Bar"))
		{
			platformerCharacter2d.MoveBack(0.25f);
		}
	}
	
	public void UpdateHP(float diff)
	{
		Hp = Mathf.Clamp(Hp + diff, 0f, 100f);

		if (!Alive)
		{
			Die();
		}
	}

	public void Die()
	{
		if (Debug.isDebugBuild)
			Debug.Log("Dead!");

		switch (State)
		{
			case DynoState.Godmode:
				DisableGodmode();
				break;

			case DynoState.Berserk:
				DisableBerserk();
				break;
		}

		State = DynoState.Death;
		Attacking = false;
		spriteRenderer.enabled = false;

		GameManager.Instance.ExitGame();
	}

	public void EnableBerserk()
	{
		if (State == DynoState.Normal)
		{
			if (Debug.isDebugBuild)
				Debug.Log("BERSERK!!!");

			anim.SetBool("Berserk", true);
			attack.SetBerserk(true);
			berserkDur = baseDur;

			State = DynoState.Berserk;
		}
	}

	public void EnableGodmode()
	{
		if (State == DynoState.Normal)
		{
			if (Debug.isDebugBuild)
				Debug.Log("GODMODE!!!");

			anim.SetBool("Godmode", true);
			godmodeDur = baseDur;

			State = DynoState.Godmode;
		}
	}

	private void DisableBerserk()
	{
		State = Alive ? DynoState.Normal : DynoState.Death;

		attack.SetBerserk(false);
		berserkDur = baseDur;
		anim.SetBool("Berserk", false);

		if (Debug.isDebugBuild)
			Debug.Log("Berserk off.");
	}

	private void DisableGodmode()
	{
		State = Alive ? DynoState.Normal : DynoState.Death;

		godmodeDur = baseDur;
		anim.SetBool("Godmode", false);

		if (Debug.isDebugBuild)
			Debug.Log("Godmode off.");
	}
}