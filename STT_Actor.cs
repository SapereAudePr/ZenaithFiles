using UnityEngine;

[System.Serializable]
public class STT_ActorParameters
{
    [Tooltip("Actor's Health")]
    public float toughness;
    [Tooltip("Threshold of the applied force")]
    public float armor;
    public float damageFactor;
}

[System.Serializable]
public class STT_ActorFX
{
    [Tooltip("Spawn this GameObject when the actor is hit")]
    public GameObject damageFX;
    [Tooltip("Spawn this GameObject when the actor is destroyed")]
    public GameObject deactivateFX;
}

[System.Serializable]
public class STT_ActorAudio
{
    public AudioClip destroyClip;
}

public class STT_Actor : MonoBehaviour
{
    public STT_ActorParameters parameters;
    public STT_ActorFX VFX;
    public STT_ActorAudio SFX;

    private Rigidbody actorRigidbody;
    private Collider actorCollider;
    private Renderer actorRenderer;

    // Event to notify when the actor is destroyed
    public delegate void ActorDestroyHandler();
    public event ActorDestroyHandler OnDestroyActor;

    void Awake()
    {
        actorRigidbody = GetComponent<Rigidbody>();
        actorCollider = GetComponent<Collider>();
        actorRenderer = GetComponent<Renderer>();

        // Check if Collider component exists
        if (actorCollider == null)
        {
            Debug.LogWarning("Collider component not found on the object.");
        }

        // Check if Renderer component exists
        if (actorRenderer == null)
        {
            Debug.LogWarning("Renderer component not found on the object.");
        }
    }

    public void ReceiveDamage(float damage, Vector3 position)
    {
        if (damage <= parameters.toughness)
        {
            parameters.toughness -= damage;
            actorRigidbody.AddExplosionForce(damage * parameters.damageFactor, position, 0.25f);

            if (VFX.damageFX != null)
            {
                GameObject newDamageFX = Instantiate(VFX.damageFX, position, Quaternion.identity);
                Destroy(newDamageFX, 3);
            }
        }
        else
        {
            if (VFX.deactivateFX != null)
            {
                GameObject newDeactivateFX = Instantiate(VFX.deactivateFX, transform.position, Quaternion.identity);
                Destroy(newDeactivateFX, 3);
            }

            DestroyActor();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > parameters.armor)
        {
            ReceiveDamage(collision.relativeVelocity.magnitude, transform.position);
        }
    }

    public void DestroyActor()
    {
        // Check if Collider component exists
        if (actorCollider != null)
        {
            actorCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("Collider component not found on the object.");
        }

        // Check if Renderer component exists
        if (actorRenderer != null)
        {
            actorRenderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the object.");
        }

        GetComponent<AudioSource>().PlayOneShot(SFX.destroyClip);

        // Invoke OnDestroyActor event when the actor is destroyed
        OnDestroyActor?.Invoke();

        Destroy(gameObject, 2);
    }
}
