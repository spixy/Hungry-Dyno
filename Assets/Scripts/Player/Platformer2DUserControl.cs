using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;


	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}

	private void Update()
	{
		if (!m_Jump)
		{
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

			if (Debug.isDebugBuild && m_Jump)
			{
				Debug.Log("Jump");
			}
		}
	}

	private void FixedUpdate()
	{
		// Read the inputs.
		// Pass all parameters to the character control script.
		m_Character.Move(1, m_Jump);
		m_Jump = false;
	}
}