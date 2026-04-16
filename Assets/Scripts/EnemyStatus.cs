using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] float _lifeMax = 2;
    private float currentLife;
    public void Hitted(float damage)
    {
        currentLife -= damage;
        if (currentLife > 0)
            return;

           Destroy(gameObject);
          
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLife = _lifeMax;
    }
}
