using UnityEngine;
using System.Collections;

public interface IRepo<T>
{
     T GetObject(int _ID);
     void UpdateObject(int _ID, T _object);
     void AddObject(T _object);
     void RemoveObject(int _ID);
}

public class Repository
{
    static private Repository Repo;
    static public Repository repo
    {
        get
        {
            if (Repo == null)
                Repo = new Repository();
            return Repo;
        }
    }

    public Repository()
    {
        //spellRepository = new SpellRepository();
        //itemRepository = new ItemRepository();
    }

    
    

}