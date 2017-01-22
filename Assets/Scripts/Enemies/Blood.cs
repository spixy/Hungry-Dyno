using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	void OnEnable()
	{
		animator.SetBool("Dead", true);
	}
}
