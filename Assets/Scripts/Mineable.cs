using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineable : MonoBehaviour
{
    [SerializeField] public float breakTime = 1.0f;
    [SerializeField] public int worth = 1;

    private float miningProcess = 0.0f;
    // Start is called before the first frame update

    public IEnumerator Mining()
    {
        while (miningProcess < 1.0f)
        {
            miningProcess += Time.deltaTime / breakTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
