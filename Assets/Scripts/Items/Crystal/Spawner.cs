using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Crystal _prefab;
    
    private Crystal _crystal;
    private const float _callTime = 2f;

    private void Start()
    {
        StartCoroutine(CreateCrystal());
    }

    private IEnumerator CreateCrystal()
    {
        while(true)
        {
            if(_crystal == null)
            {
                _crystal = Instantiate(_prefab, transform.position, Quaternion.identity);
                _crystal.transform.parent = transform;
            }

            yield return new WaitForSeconds(_callTime);
        }
    }
}
