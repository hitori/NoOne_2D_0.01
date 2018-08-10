using UnityEngine;

public class Particles : MonoBehaviour {

	void Start () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
    }

}
