using System.Collections;

using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Crystal _prefab;
    [SerializeField] private float _spawnDelay = 2f;

    private Crystal _crystal;

    private void Start()
    {
        StartCoroutine(CreateCrystal());
    }

    private IEnumerator CreateCrystal()
    {
        while (true)
        {
            if (_crystal == null)
            {
                _crystal = Instantiate(_prefab, transform.position, Quaternion.identity);
                _crystal.transform.parent = transform;
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
