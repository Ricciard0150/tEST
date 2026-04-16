using UnityEngine;

public class Gun : MonoBehaviour
{
    private Transform _camera;
     //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
    }
    void Update()
    {
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

        shootable.Hitted(1);

    }
}
