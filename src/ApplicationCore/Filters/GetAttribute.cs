using System;

namespace ApplicationCore.Filters
{
    internal class GetAttribute : Attribute
    {
        public GetAttribute(params string[] param)
        {
        }
    }
}