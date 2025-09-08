using System;
using System.Collections.Generic;
using Sigleton;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPooling
{
   public class ObjectPoolManager : BaseSingleton<ObjectPoolManager>
   {
      private Dictionary<GameObject, ObjectPool<IPoolable>> ObjectPools = new Dictionary<GameObject, ObjectPool<IPoolable>>();

      public T Get<T>(T _object) where T : MonoBehaviour,IPoolable
      {
         if (_object == null) return null;
         ObjectPool<IPoolable> _pool;
         if (!ObjectPools.TryGetValue(_object.gameObject, out _pool))
         {
            ObjectPools.Add(_object.gameObject,new ObjectPool<IPoolable>(
               () => Instantiate(_object),
               (_obj) => _obj.onGet(),
               (_obj) => _obj.onRelease(),
               (_obj) => _obj.onDestroy()
            ));
            _pool = ObjectPools[_object.gameObject];
         }

         _pool.Get(out var _result);

         _result.OriginPref = _object.gameObject;
         return _result as T;
      }

      public void Release<T>(T _object) where T : MonoBehaviour, IPoolable
      {
         if (_object == null) return ;
         if (!ObjectPools.TryGetValue(_object.OriginPref, out var _pool))
         {
            return;
         }
         _pool.Release(_object);
      }

      private void OnDestroy()
      {
         foreach (var pair in ObjectPools)
         {
            pair.Value.Dispose();
         }
      }
   }
}
