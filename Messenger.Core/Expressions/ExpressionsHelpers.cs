using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Messenger.Core
{
    /// <summary>
    /// Additional user functions for <see cref="Expression"/>
    /// </summary>
    public static class ExpressionsHelpers
    {
        /// <summary>
        /// Compiles an <see cref="Expression"/> and gets the function return value
        /// </summary>
        /// <typeparam name="T">Type of return value</typeparam>
        /// <param name="lambda">Exspression to compile</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        /// <summary>
        /// Sets the underlying properties value to the given value, 
        /// from an expression that contains the property
        /// </summary>
        /// <typeparam name="T">Type of value to set</typeparam>
        /// <param name="lambda">Expression</param>
        /// <param name="value">Value to set the property to</param>
        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            // converts a lambda "() => some.Property" to "some.Property"
            var expression = (lambda as LambdaExpression).Body as MemberExpression;

            // get the property information so we can set
            var propInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression)
                .Compile()
                .DynamicInvoke();

            // set the property value
            propInfo.SetValue(target, value);
        }
    }
}
