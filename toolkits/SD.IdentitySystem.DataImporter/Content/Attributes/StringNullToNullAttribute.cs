using ArxOne.MrAdvice.Advice;
using System;

namespace SD.IdentitySystem.DataImporter.Content.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Assembly)]
    public class StringNullToNullAttribute : Attribute, IPropertyAdvice
    {
        public void Advise(PropertyAdviceContext context)
        {
            if (context.HasValue)
            {
                if (context.Value != null && context.Value.ToString().ToLower() == "null")
                {
                    context.Value = null;
                }
            }

            context.Proceed();
        }
    }
}
