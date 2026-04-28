using UnityEngine;

public class GunCollect : Item, ICollectable
{
    [SerializeField] private GunElement _attributes;
    public override Element Collect()
    {
        Destroy(gameObject);
        return _attributes;
    }

    protected override void Teste()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
