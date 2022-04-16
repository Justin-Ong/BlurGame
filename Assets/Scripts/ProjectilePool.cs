using CMF;
using UnityEngine;
using Impact.Utility.ObjectPool;

/// <summary>
/// Object pool for playing audio from AudioInteractionResults.
/// </summary>
public class ProjectilePool : ObjectPool<Projectile>
{
    public Projectile template;

    private Weapon weapon;
    private Collider parentCollider;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        parentCollider = GetComponentInParent<Collider>();

        Template = template;
        Initialize(_poolSize, _fallbackMode);
    }

    protected override Projectile createPooledObjectInstance(int index)
    {
        Projectile instance = Instantiate(Template, this.transform);
        if (weapon.isAffectedByGravity)
        {
            instance.Rb.useGravity = true;
        }
        else
        {
            instance.Rb.useGravity = false;
        }
        instance.transform.SetParent(null);
        instance.OriginalParent = null;
        instance.gameObject.name = Template.name + "_" + index;
        instance.Lifetime = weapon.projectileLifetime;
        instance.Damage = weapon.projectileDamage;
        instance.Mask = weapon.mask;
        Physics.IgnoreCollision(instance.GetComponent<Collider>(), parentCollider);
        instance.MakeAvailable();
        return instance;
    }
}
