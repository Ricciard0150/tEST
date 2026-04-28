using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private GunSystem _gunSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _gunSystem = GetComponentInParent<GunSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out ICollectable collectable))
        return;

        _gunSystem.AddNewGun((GunElement)collectable.Collect());
        print("colliding");
    }
}

