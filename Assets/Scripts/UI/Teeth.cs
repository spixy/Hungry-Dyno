using UnityEngine;

public class Teeth : MonoBehaviour
{
	private Vector3 defaultPosition;

	[SerializeField]
	private Animator animator;
	
	[SerializeField]
	private Vector3 moveVector;

	public Animator Animator
	{
		get { return animator; }
	}

	public Vector3 MoveVector
	{
		get { return moveVector; }
	}

	void Awake()
	{
		defaultPosition = transform.localPosition;
	}

	void OnEnable()
	{
		transform.localPosition = defaultPosition;
	}

	public void OnCompleteAnim()
	{
		GameManager.Instance.gui.MainMenu.OnCompleteAnimation();
	}
}
