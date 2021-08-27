using System;
using UnityEngine;

namespace Exceptions
{
    public class MissingEntityException : Exception
    {
        public MissingEntityException(GameObject gameObject) : base($"{gameObject.name}({gameObject.GetInstanceID()}) is missing entity component"){}
    }
}