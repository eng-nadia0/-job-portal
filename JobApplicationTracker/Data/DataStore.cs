using System;

using JobApplicationTracker.Models;
namespace JobApplicationTracker.Data;

public class DataStore<T> where T : class,IEntity
{
    private Dictionary<int, T> Items { get; set; } = new();
    private int nextId = 1;

    public void Add(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "Item cannot be null.");
        }
        item.Id= nextId++;
        Items.Add(item.Id, item);
       
    }

    public bool Update(T update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Update cannot be null.");
        }
        
        if (Items.ContainsKey(update.Id))
        {
            Items[update.Id] = update;
            return true;
        }
        return false;
        
    }

    public void Remove(int id)=>Items.Remove(id);
    
    public T? GetById(int id)
    {
        if(!Items.ContainsKey(id))
        {
            return null;
        }
        return Items[id];

    }

    public IEnumerable<T> GetAll(){
        return new List<T>(Items.Values); 
    }

}

