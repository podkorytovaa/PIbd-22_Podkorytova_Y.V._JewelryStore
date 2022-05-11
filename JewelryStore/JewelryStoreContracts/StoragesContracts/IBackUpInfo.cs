﻿using System.Collections.Generic;
using System.Reflection;

namespace JewelryStoreContracts.StoragesContracts
{
    public interface IBackUpInfo
    {
        Assembly GetAssembly();

        List<PropertyInfo> GetFullList();

        List<T> GetList<T>() where T : class, new();
    }
}
