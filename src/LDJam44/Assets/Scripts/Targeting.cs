using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private List<TrackedRenderer> Targets = new List<TrackedRenderer>();
    public GameObject Reticle;
    public Collider Collider;
    public TrackedRenderer Target;
    public float ChargeSpeed;

    void Update()
    {
        if (Input.GetButton("Fire2") || Input.GetButtonUp("Fire2"))
        {
            if (transform.localScale.x < 300)
                transform.localScale += new Vector3(Time.deltaTime * ChargeSpeed, Time.deltaTime * ChargeSpeed, 0);
            var applicableTargets = Targets.Where(x => x.gameObject != null && IsInside(Collider, x.gameObject.transform.position)).ToList();
            if (applicableTargets.Any())
            {
                if (!applicableTargets.Contains(Target))
                    Target = applicableTargets.OrderBy(x => x.Renderer.transform.position.x).First();
                Reticle.transform.position = Target.Renderer.transform.position;
                var size = Target.Renderer.bounds.size.x > Target.Renderer.bounds.size.y ? Target.Renderer.bounds.size.x : Target.Renderer.bounds.size.y;
                Reticle.transform.localScale = new Vector3(size * 0.9f, size * 0.9f, 1);
            }
            else
            {
                Target = null;
                Reticle.transform.position = new Vector3(-9, -9, -9);
            }
        }
        else
        {
            transform.localScale = new Vector3(0, 0, transform.localScale.z);
        }
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.layer == 10)
            Targets.Add(new TrackedRenderer(enemy));
    }

    bool IsInside(Collider test, Vector3 point)
    {
        Vector3 center;
        Vector3 direction;
        Ray ray;
        RaycastHit hitInfo;
        bool hit;

        // Use collider bounds to get the center of the collider. May be inaccurate
        // for some colliders (i.e. MeshCollider with a 'plane' mesh)
        center = test.bounds.center;

        // Cast a ray from point to center
        direction = center - point;
        ray = new Ray(point, direction);
        hit = test.Raycast(ray, out hitInfo, direction.magnitude);

        // If we hit the collider, point is outside. So we return !hit
        return !hit;
    }
}

public class TrackedRenderer
{
    public GameObject gameObject;
    public Renderer Renderer;

    public TrackedRenderer(Collider enemy)
    {
        gameObject = enemy.gameObject;
        Renderer = gameObject.GetComponent<Renderer>();
    }
}