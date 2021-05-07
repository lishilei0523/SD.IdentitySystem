using System;
using ArxOne.MrAdvice.Advice;
using Caliburn.Micro;

namespace SD.Infrastructure.WPF.Aspects
{
    /// <summary>
    /// WPF依赖属性AOP特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyPropertyAttribute : Attribute, IPropertyAdvice
    {
        /// <summary>
        /// 拦截属性
        /// </summary>
        /// <param name="context">属性上下文</param>
        public void Advise(PropertyAdviceContext context)
        {
            if (context.IsGetter)
            {
                context.Proceed();
            }
            else
            {
                context.Proceed();
                if (context.Target is INotifyPropertyChangedEx dataContext)
                {
                    dataContext.NotifyOfPropertyChange(context.TargetName);
                }
            }
        }
    }
}
