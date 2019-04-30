using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private List<Renderer> Targets = new List<Renderer>();
    public GameObject Reticle;
    public Collider Collider;
    public Renderer Target;

    void Update()
    {
        Targets = Targets.Where(x => x != null).ToList();
        if (Targets.Any())
        {
            Target = Targets.OrderBy(x => x.transform.position.x).First();
            Reticle.transform.position = Target.transform.position;
            var size = Target.bounds.size.x > Target.bounds.size.y ? Target.bounds.size.x : Target.bounds.size.y;
            Reticle.transform.localScale = new Vector3(size * 0.9f, size * 0.9f, 1);
        }
        else
        {
            Target = null;
            Reticle.transform.position = new Vector3(-9, -9, -9);
        }
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.layer == 10)
            Targets.Add(enemy.gameObject.GetComponent<Renderer>());
    }

    void OnTriggerExit(Collider enemy)
    {
        if (enemy.gameObject.layer == 10 && !Collider.bounds.Contains(enemy.transform.position))
            Targets.Remove(enemy.gameObject.GetComponent<Renderer>());
    }
}
