using System;

namespace ApplicationCore.Filters
{
    internal class DtoAttribute : Attribute
    {
        public DtoAttribute(params string[] param)
        {
        }
    }
}