using TMPro.EditorUtilities;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable
{
    public GunElement Collect()
    {
        throw new System.NotImplementedException();
    }

    //metodos abstgratos
    //força os fihlos a implementarem
    //uasdo quando todos os filjos usam mas com comportamentos diferentes 
    //nao declara corpo apenas  assinatura
    protected abstract void Teste();

    protected virtual void Teste2()
    {
        //corpo do metodo 
    }

    //metodos normais
    //quando todos os filhos tem o mesmo comportamento
    protected void Teste3()
    {

    }
}
