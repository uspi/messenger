using System;
using System.Collections.Generic;
using System.Linq;

namespace Messenger.Server
{
    /// <summary>
    /// Extensions for working with collections and data
    /// </summary>
    static class ListHelpers
    {
        // allows you to clone a collection whose elements support the interface ICloneable
        public static IList<T> Clone<T>(this IList<T> listToClone)
            where T : ICloneable
        {
            return listToClone
                    .Select(item => (T)item.Clone())
                    .ToList();
        }
    }
}
