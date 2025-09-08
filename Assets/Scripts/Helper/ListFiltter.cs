using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Filtter<T,U>
{
    private Func< T,U> getter;
    private Func< U,U,bool> filter;
    private U filterCompairValue;
    private U defaultValue;
    
    public Filtter( Func< T,U> getter, Func< U,U,bool> filter,U _defaultValue)
    {
        this.getter = getter;
        this.filter= filter;
        this.defaultValue = _defaultValue;
    }
    
    
    public IEnumerable<T> GetFilteredList( IEnumerable<T> items,U filterValue)
    {
        filterCompairValue = filterValue;
        if (filterCompairValue.Equals(defaultValue)) return items;
       return items.Where(item => filter( getter(item), filterValue));
    }
}
