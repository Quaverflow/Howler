using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howler
{
    public class A<T>
    {
#pragma warning disable CS8601
        public static T Value = default;
#pragma warning restore CS8601
    }
}
