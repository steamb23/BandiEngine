// Copyright (c) 2017 SteamB23
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFly
{
    public class ModuleContainer : IEnumerable<Module>
    {
        Dictionary<Type, Module> modules = new Dictionary<Type, Module>();

        int Count => modules.Count;

        public void Add<T>(T module) where T : Module
        {
            this.Add(typeof(T), module);
            module.Initialize();
        }

        public bool Remove<T>() where T : Module
        {
            return this.Remove(typeof(T));
        }

        public bool Contains<T>() where T : Module
        {
            return this.Contains(typeof(T));
        }

        public T Find<T>() where T : Module
        {
            return Find(typeof(T)) as T;
        }

        public void Clear()
        {
            modules.Clear();
        }

        internal void Add(Type type, Module module)
        {
            // 종속성 검사
            var requiredType = GetRequiredType(type);
            if (requiredType.Length > 0)
                throw new RequiredTypeException(type, requiredType);

            modules.Add(type, module);
        }

        internal bool Remove(Type type)
        {
            // 종속성 검사
            var dependentType = GetDependentType(type);
            if (dependentType.Length > 0)
                throw new DependentTypeException(type, dependentType);

            return modules.Remove(type);
        }

        internal bool Contains(Type type)
        {
            return modules.ContainsKey(type);
        }

        internal Module Find(Type type)
        {
            return modules[type];
        }

        /// <summary>
        /// 해당 모듈에서 요구하는 모듈의 형식을 가져옵니다. 기본적으로 이미 탑재되어있는 모듈은 제외합니다.
        /// </summary>
        /// <param name="type">모듈의 형식입니다.</param>
        /// <param name="checkAllRequiredType">해당 모듈이 요구하는 모든 모듈의 형식을 가져옵니다.</param>
        /// <returns>해당 모듈에서 요구하는 모듈의 형식입니다.</returns>
        private Type[] GetRequiredType(Type type, bool checkAllRequiredType = false)
        {
            var requiredTypeAttributes = type.GetCustomAttributes(typeof(RequiredTypeAttribute), true) as RequiredTypeAttribute[];
            var requiredTypes = new List<Type>();
            foreach (var requiredTypeAttribute in requiredTypeAttributes)
            {
                if (checkAllRequiredType || !this.Contains(requiredTypeAttribute.Type))
                    requiredTypes.Add(requiredTypeAttribute.Type);
            }
            return requiredTypes.ToArray();
        }
        /// <summary>
        /// 해당 모듈에 의존하는 모든 모듈의 형식을 가져옵니다.
        /// </summary>
        /// <param name="type">모듈의 형식입니다.</param>
        /// <returns>해당 모듈에 의존하는 모든 모듈의 형식입니다.</returns>
        private Type[] GetDependentType(Type type)
        {
            var dependentType = new List<Type>();
            foreach (var targetType in modules.Keys)
            {
                if (GetRequiredType(targetType, true).Contains(type))
                    dependentType.Add(targetType);
            }
            return dependentType.ToArray();
        }

        public IEnumerator<Module> GetEnumerator()
        {
            return modules.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
