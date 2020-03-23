using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_SelfDestructingEnemy : MonoBehaviour
{
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        transform.position += new Vector3(Random.Range(-3, 3), Random.Range(-3, 3),Random.Range(-3, 3));
    }
}
