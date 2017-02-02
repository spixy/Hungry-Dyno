using UnityEngine;
using Random = UnityEngine.Random;

public class BgSpawner : AutoSpawner
{
	private enum State
	{
		None,
		City,
		Nature,
		Vulcano
	}

	[SerializeField]
	private GameObject vulcano;

	[SerializeField]
	private GameObject[] nature;

	[SerializeField]
	private Transform parent;

	private State state = State.None;

	public override void Spawn(float x)
	{
		GameObject go = null;
		float random = Random.value;

		switch (state)
		{
			case State.None:
				state = (State) Random.Range(0, 4);
				this.Spawn(x);
				return;

			case State.City:
				go = this.SpawnObject(this.obj.GetRandomItem(), parent);
				if (random < 0.3f)
				{
					state = (State)Random.Range(0, 3);
				}
				break;

			case State.Nature:
				go = this.SpawnObject(this.nature.GetRandomItem(), parent);
				if (random < 0.3f)
				{
					state = (State)Random.Range(0, 4);
				}
				break;

			case State.Vulcano:
				go = this.SpawnObject(this.vulcano, parent);
				if (random < 0.75f)
				{
					state = State.Nature;
				}
				else
				{
					state = State.None;
				}
				break;
		}

		go.transform.position = new Vector3(x, Random.Range(this.minY, this.maxY), 0f);
	}
}
