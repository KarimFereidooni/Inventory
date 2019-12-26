namespace Inventory.Extensions
{
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ToLatin
    {
        public const char ArabicYeChar = 'ي';
        public const char PersianYeChar = 'ی';

        public const char ArabicKeChar = 'ك';
        public const char PersianKeChar = 'ک';

        private static readonly char[] ArabicNumbers = new char[10] { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };
        private static readonly char[] PersianNumbers = new char[10] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
        private static readonly char[] LatinNumbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static object ApplyToLatin(this object data)
        {
            return data == null ? null : ApplyToLatin(data.ToString());
        }

        public static object ApplyToLatin(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return DBNull.Value;
            }
            else
            {
                string temp = data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();
                for (int i = 0; i < 10; i++)
                {
                    temp = temp.Replace(ArabicNumbers[i], LatinNumbers[i]).Replace(PersianNumbers[i], LatinNumbers[i]);
                }

                return temp;
            }
        }

        public static void ApplyToLatin(this DbCommand command)
        {
            // command.CommandText = command.CommandText.ApplyToLatin().ToString();
            foreach (DbParameter parameter in command.Parameters)
            {
                switch (parameter.DbType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Xml:
                        parameter.Value = parameter.Value is DBNull ? parameter.Value : parameter.Value.ApplyToLatin();
                        break;
                }
            }
        }
    }

    public class HintCommandInterceptor : DbCommandInterceptor
    {
        public override Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            command.ApplyToLatin();
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            command.ApplyToLatin();
            return base.NonQueryExecuting(command, eventData, result);
        }

        public override Task<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            command.ApplyToLatin();
            return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.ApplyToLatin();
            return base.ReaderExecuting(command, eventData, result);
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            command.ApplyToLatin();
            return base.ScalarExecuting(command, eventData, result);
        }

        public override Task<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            command.ApplyToLatin();
            return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }
    }

    public static class EntityFrameworkHelper
    {
        public static IOrderedQueryable<TSource> DynamicOrderBy<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            propertyName = char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1);
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = GetPropertyInfo(propertyName, entityType);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = GetPropertyExpression(propertyName, arg);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();

                     // Put more restriction here to ensure selecting the right overload
                     return parameters.Count == 2; // overload that has 2 parameters
                 }).Single();

            // The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static IOrderedQueryable<TSource> DynamicOrderByDescending<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            propertyName = char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1);
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = GetPropertyInfo(propertyName, entityType);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = GetPropertyExpression(propertyName, arg);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();

                     // Put more restriction here to ensure selecting the right overload
                     return parameters.Count == 2; // overload that has 2 parameters
                 }).Single();

            // The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static IOrderedQueryable<TSource> DynamicThenBy<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            propertyName = char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1);
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = GetPropertyInfo(propertyName, entityType);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = GetPropertyExpression(propertyName, arg);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == "ThenBy" && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();

                     // Put more restriction here to ensure selecting the right overload
                     return parameters.Count == 2; // overload that has 2 parameters
                 }).Single();

            // The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static IOrderedQueryable<TSource> DynamicThenByDescending<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            propertyName = char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1);
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = GetPropertyInfo(propertyName, entityType);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = GetPropertyExpression(propertyName, arg);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == "ThenByDescending" && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();

                     // Put more restriction here to ensure selecting the right overload
                     return parameters.Count == 2; // overload that has 2 parameters
                 }).Single();

            // The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

        public static MemberExpression GetPropertyExpression(string name, ParameterExpression parameterEx)
        {
            Expression ex = parameterEx;
            foreach (var member in name.Split('.'))
            {
                ex = Expression.Property(ex, member);
            }

            return ex as MemberExpression;
        }

        public static PropertyInfo GetPropertyInfo(string propertyName, System.Type entityType)
        {
            propertyName = char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1);
            PropertyInfo propertyInfo = null;
            System.Type type = entityType;
            foreach (var member in propertyName.Split('.'))
            {
                propertyInfo = type.GetProperty(member);
                type = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }
    }
}
