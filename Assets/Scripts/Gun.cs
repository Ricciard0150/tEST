using UnityEngine;

[System.Serializable]
public class GunElement 
{

    [SerializeField] private float _damage;
    [SerializeField] private float _shootRate;
    [SerializeField] private float _ammunation;
    [SerializeField] private string _name;

    public GunElement(float damage, float shootRate, float ammunation, string name)
    {
        _damage = damage;
        _shootRate = shootRate;
        _ammunation = ammunation;
        _name = name;
    }

    public string Name { get => _name;}
    public float ShootRate { get => _shootRate;}
    public float Ammunation { get => _ammunation;}
    public float Damage { get => _damage; }
}
public class Gun : MonoBehaviour
{
    [SerializeField] private GunElement _handGun;    
    private Transform _camera;
    private float shootTimer;   
    void Start()
    {
        _camera = Camera.main.transform;
        shootTimer = _handGun.ShootRate;    
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer < _handGun.ShootRate)
            return;

        //verifica se o plauyer atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        //verifica se o player acertou algo
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))//raycast e estatico, sendo acessado atraves da class
                                                                                                                    //camera forward e a direção do raycast, out é para retornar o objeto acertado, layer é para verificar se o objeto acertado tem a layer "Shootable"
            return;
        //verifica se o objeto acertado tem o componente IShootable
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        shootable.Hitted(1, target.point);

        shootTimer = 0;
    }
}
