// =================================================================== 
// 项目说明
//====================================================================
// 幸运草工作室 @ CopyRight 2014-2020
// 文件： ISpecificationExtensions.cs
// 项目名称： 
// 创建时间：2014/6/4
// 创建人：丁富升
// ===================================================================

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repository.Specification
{
    public static class ISpecificationExtensions
    {
        //public static ISpecification<T> OrderBy<T>(this ISpecification<T> q,Expression<Func<T,object>> property,bool IsASC )
        //{
        //    var param = property.Parameters[0];
        //    var oper = ((UnaryExpression) property.Body).Operand;
        //    var lam = Expression.Lambda(oper, param);
        //    ParameterExpression expression = Expression.Parameter(typeof (ISpecification<T>), "");
        //    MethodInfo method;
        //    if (IsASC)
        //        method = typeof (System.Linq.Queryable).GetMethods()[16];
        //    else
        //        method = typeof(System.Linq.Queryable).GetMethods()[16];
        //    method = method.MakeGenericMethod(typeof (T), oper.Type);
        //    MethodCallExpression call = Expression.Call(method, expression, lam);
        //    var lamb = Expression.Lambda(call, expression).Compile();
        //    Func<IQueryable<T>, IOrderedQueryable<T>> func = (Func<IQueryable<T>, IOrderedQueryable<T>>) lamb;
        //    return func(q);
        //}
        /// <summary>
        /// 建立 Between 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="from">开始值</param>
        /// <param name="to">结束值</param>
        /// <returns></returns>
        public static ISpecification<T> Between<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P from, P to)
        {
            var parameter = property.GetParameters();
            var constantFrom = Expression.Constant(from);
            var constantTo = Expression.Constant(to);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var c1 = Expression.GreaterThanOrEqual(nonNullProperty, constantFrom);
            var c2 = Expression.LessThanOrEqual(nonNullProperty, constantTo);
            var c = Expression.AndAlso(c1, c2);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(c, parameter);

            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 Like ( 模糊 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> Like<T>(this ISpecification<T> q, Expression<Func<T, string>> property, string value)
        {
            value = value.Trim();
            if (!string.IsNullOrEmpty(value))
            {
                var expression = Expression.Lambda<Func<T, bool>>(
                 Expression.Call(property.Body, typeof(string).GetMethod("Contains"),
                    Expression.Constant(value)), property.Parameters);
                q.Predicate = q.Predicate.And(expression);
            }
            return q;
        }
        /// <summary>
        /// 建立 Like ( 模糊 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> StartWith<T>(this ISpecification<T> q, Expression<Func<T, string>> property, string value)
        {
            value = value.Trim();
            if (!string.IsNullOrEmpty(value))
            {
                var expression = Expression.Lambda<Func<T, bool>>(
                    Expression.Call(property.Body, typeof(string).GetMethod("StartsWith"),
                        Expression.Constant(value)), property.Parameters);
                q.Predicate = q.Predicate.And(expression);
            }
            return q;
        }

        /// <summary>
        /// 建立 Equals ( 相等 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> Equals<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.Equal(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 NotEquals ( 不相等 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> NotEquals<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.NotEqual(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 GreaterThan ( 大于 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> GreaterThan<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.GreaterThan(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 GreaterThanOrEqual ( 大于等于 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> GreaterThanOrEqual<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.GreaterThanOrEqual(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 LessThan ( 小于 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> LessThan<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.LessThan(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 LessThanOrEqual ( 小于等于 ) 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="value">查询值</param>
        /// <returns></returns>
        public static ISpecification<T> LessThanOrEqual<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, P value)
        {
            var parameter = property.GetParameters();
            var constant = Expression.Constant(value);
            Type type = typeof(P);
            Expression nonNullProperty = property.Body;
            //如果是Nullable<X>类型，则转化成X类型
            if (IsNullableType(type))
            {
                type = GetNonNullableType(type);
                nonNullProperty = Expression.Convert(property.Body, type);
            }
            var methodExp = Expression.LessThanOrEqual(nonNullProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            q.Predicate = q.Predicate.And(lambda);
            return q;
        }
        /// <summary>
        /// 建立 In 查询条件
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="q">动态查询条件创建者</param>
        /// <param name="property">属性</param>
        /// <param name="valuse">查询值</param> 
        /// <returns></returns>
        public static ISpecification<T> In<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, params P[] values)
        {
            if (null == property) { throw new ArgumentNullException("property"); }
            if (null == values) { throw new ArgumentNullException("values"); }
            ParameterExpression p = property.Parameters.Single();
            if (!values.Any())
            {
                return q;
            }
            var equals = values.Select(value => (Expression)Expression.Equal(property.Body, Expression.Constant(value, typeof(P))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));
            q.Predicate = q.Predicate.And(Expression.Lambda<Func<T, bool>>(body, p));
            return q;
        }
        /// <summary>
        /// Nots the in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="q">The q.</param>
        /// <param name="property">The property.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static ISpecification<T> NotIn<T, P>(this ISpecification<T> q, Expression<Func<T, P>> property, params P[] values)
        {
            if (null == property) { throw new ArgumentNullException("property"); }
            if (null == values) { throw new ArgumentNullException("values"); }
            ParameterExpression _p = property.Parameters.Single();
            if (!values.Any())
            {
                return q;
            }
            var equals = values.Select(value => (Expression)Expression.NotEqual(property.Body, Expression.Constant(value, typeof(P))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));
            q.Predicate = q.Predicate.And(Expression.Lambda<Func<T, bool>>(body, _p));
            return q;
        }
        private static ParameterExpression[] GetParameters<T, S>(this Expression<Func<T, S>> expr)
        {
            return expr.Parameters.ToArray();
        }

        static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        static Type GetNonNullableType(Type type)
        {
            return type.GetGenericArguments()[0];
            //return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }
    }
}
