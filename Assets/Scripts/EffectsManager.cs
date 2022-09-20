using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	public GameObject BuffEfectNitro;
	public GameObject BuffEfectSlow;
	public GameObject BuffEfectSuperGrow;
	public GameObject BuffEfectGrow;
	public GameObject EffectLose;

	public GameObject Player;


	public void EffectNitro()
	{
		SpawnAndDestroyPrefabs(BuffEfectNitro);
	}

	public void EffectSlow()
	{
		SpawnAndDestroyPrefabs(BuffEfectSlow);

	}
	public void EffectSuperGrow()
	{
		SpawnAndDestroyPrefabs(BuffEfectSuperGrow);

	}

	public void EffectGrow()
	{
		SpawnAndDestroyPrefabs(BuffEfectGrow);

	}
	public void EffectGameOver()
	{
		SpawnAndDestroyPrefabs(EffectLose);

	}


	private void SpawnAndDestroyPrefabs(GameObject buffEfect)
	{
		GameObject spawnPrefab = Instantiate(buffEfect, Player.transform.position, Player.transform.rotation);
		spawnPrefab.GetComponent<AudioSource>().Play();
		Destroy(spawnPrefab, 1f);
	}



}