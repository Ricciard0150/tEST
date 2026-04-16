using UnityEngine;

[System.Serializable]
public class GunElement 
{

    [SerializeField] private float _damage;
    [SerializeField] private float _shootRate;
    [SerializeField] private float _ammunation; //municao total da arma para referencia pro jogador
    [SerializeField] private string _name;
    [SerializeField] private float clipSize;//quantidade ed balas q o pente suporta, para referencia do jogador
    private float ammunationClip;  //pente tual sendo utilizado ate ter q puxar mais

    public void Initialize()
    {
        ammunationClip = clipSize;
    }
    public bool UseAmmunation()
    {
        if (ammunationClip <= 0)
            return false;
        ammunationClip--;
        return true;
    }

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
        _handGun.Initialize();
        shootTimer = _handGun.ShootRate;    
    }
    void Update()
    {
        shootTimer += Time.deltaTime; 
        if (shootTimer < _handGun.ShootRate)
            return;

        //verifica se o plauyer atirou
        if (!Input.GetButtonDown("Fire1"))
            return;

        if (!_handGun.UseAmmunation())
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
