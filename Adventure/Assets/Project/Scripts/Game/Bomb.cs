using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float duration = 5f;
    public float radius = 3f;
    public float explosionDuration = 0.25f;
    public GameObject ExplosionObject;

    private float explosionTimer;
    private bool exploded;

	// Use this for initialization
	void Start () {
        explosionTimer = duration;
        ExplosionObject.transform.localScale = Vector3.one * radius;
        ExplosionObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        explosionTimer -= Time.deltaTime;
        if (explosionTimer <= 0f && exploded == false)
        {
            exploded = true;
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in hitObjects)
            {
                if (collider.GetComponent<Enemy>() != null)
                {
                    collider.GetComponent<Enemy>().Hit();
                }
            }

            StartCoroutine(Explode());
            }
	}

    private IEnumerator Explode ()
    {
        ExplosionObject.SetActive(true);

        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
}
