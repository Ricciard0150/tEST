using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _bloodEffect; 
    [SerializeField] float _lifeMax = 2;
    private float currentLife;
    public void Hitted(float damage, Vector3 shootPoint)
    {
        currentLife -= damage;
        if (currentLife > 0)
            return;
        GameObject blood = Instantiate(_bloodEffect, shootPoint, Quaternion.LookRotation(shootPoint - transform.position))  ;
        blood.transform.SetParent(transform);

        Destroy(gameObject);
          
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLife = _lifeMax;
    }
}
