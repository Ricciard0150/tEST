using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private GunSystem _gunSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _gunSystem = GetComponent<GunSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollectable collectable))
        return;

        _gunSystem.AddNewGun(collectable.Collect());
    }
}

