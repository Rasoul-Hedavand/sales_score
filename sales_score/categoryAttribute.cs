using System;

namespace sales_score
{
    internal class categoryAttribute : Attribute
    {
        private string v;

        public categoryAttribute(string v)
        {
            this.v = v;
        }
    }
}