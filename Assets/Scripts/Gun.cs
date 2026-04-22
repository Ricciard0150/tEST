using System.Collections;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[System.Serializable]
public class GunElement 
{
    public UnityEvent OnReload;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootRate;
    [SerializeField] private float _ammunation; //municao total da arma para referencia pro jogador
    [SerializeField] private string _name;
    [SerializeField] private float ammunation;//quantidade ed balas q o pente suporta, para referencia do jogador
    [SerializeField] private float _reloadTime;
    private float ammunationClip;  //pente tual sendo utilizado ate ter q puxar mais
    private float clipSize;

    public void Initialize()
    {
        ammunationClip = ammunation;
    }
    public bool UseAmmunation()
    {
        if (ammunationClip <= 0)
        {
            if(ammunation <= 0)
            {
                OnReload.Invoke();
            }
        }
        ammunationClip--;
        return false;
    }

    public void Reload()
    {
        if(ammunation <= 0)
        return;

        float ammunationToReload = clipSize - ammunationClip;
        if (ammunationToReload <= 0)
            return;
        ammunationClip += ammunation >= ammunationToReload ? ammunationToReload : ammunation; 
        ammunation = ammunationToReload;
    }
    public GunElement(float damage, float shootRate, float ammunation, string name, float reloadTime)
    {
        _damage = damage;
        _shootRate = shootRate;
        _ammunation = ammunation;
        _name = name;
        _reloadTime = reloadTime;
    }

    public string Name { get => _name;}
    public float ShootRate { get => _shootRate;}
    public float Ammunation { get => _ammunation;}
    public float Damage { get => _damage; }

    public float ReloadTime { get => _reloadTime;}
}
public class Gun : MonoBehaviour
{
    [SerializeField] private GunElement _handGun;    
    private Transform _camera;
    private float shootTimer;
    private float _shootTimer;
    private bool _isReloading; 
    void Start()
    {
        _camera = Camera.main.transform;
        _handGun.Initialize();
        shootTimer = _handGun.ShootRate;
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
    }
    void Update()
    {
        if(Input.GetButtonDown("Reload"))
        {
            if(_handGun.Ammunation <=  0)
            {
                _handGun.OnReload.Invoke();

            }
        }
        shootTimer += Time.deltaTime;
        if (_isReloading)
            return;
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
    IEnumerator Reload()
    {
       
        yield return new WaitForSeconds(_handGun.ReloadTime);
        _handGun.Reload();
        _shootTimer = _handGun.ShootRate;
        _isReloading = true;
    }
}
