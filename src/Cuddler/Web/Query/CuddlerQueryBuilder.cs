using System.Linq.Expressions;
using Cuddler.Web.Api;
using Kendo.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Web.Query;

public class CuddlerQueryBuilder<TModel> where TModel : class
{
    private readonly CuddlerDbQuery _dbQuery;

    public CuddlerQueryBuilder()
    {
        _dbQuery = new CuddlerDbQuery(typeof(TModel));
    }

    public CuddlerDbQuery AsQueryObj()
    {
        return _dbQuery;
    }

    public CuddlerQueryBuilder<TModel> Filter(Action<FilterFactory> configurator)
    {
        configurator(new FilterFactory(_dbQuery));

        return this;
    }

    public CuddlerQueryBuilder<TModel> OrderBy<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        var key = GetKey(keySelector);
        _dbQuery.SortList.Add(new SortDescriptor(key, ListSortDirection.Descending));
        return this;
    }

    public CuddlerQueryBuilder<TModel> OrderByAscending<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        var key = GetKey(keySelector);
        _dbQuery.SortList.Add(new SortDescriptor(key, ListSortDirection.Ascending));
        return this;
    }

    private static string GetKey<TKey>(Expression<Func<TModel, TKey>> property)
    {
        var expression = property.Body;

        var propertyBody = expression.Print(); // ie. f.Payments.ChasePayment.ChaseApiKey
        propertyBody = propertyBody.Replace("(object)", string.Empty);
        var firstPart = propertyBody.Split('.')
                                    .First();

        var key = propertyBody[(firstPart.Length + 1)..];

        return key;
    }

    public class CuddlerBoolFilter : CuddlerBaseFilter
    {
        private readonly CuddlerAndOrFilter<CuddlerBoolFilter> _andOr;

        public CuddlerBoolFilter(string key) : base(key)
        {
            _andOr = new CuddlerAndOrFilter<CuddlerBoolFilter>(this);
        }

        public CuddlerAndOrFilter<CuddlerBoolFilter> IsEqualTo(bool b)
        {
            _query += $"{_key}~eq~{b.ToString().ToLower()}";

            return _andOr;
        }

        public CuddlerAndOrFilter<CuddlerBoolFilter> IsEqualTo(string b)
        {
            _query += $"{_key}~eq~{b}";

            return _andOr;
        }

        public CuddlerAndOrFilter<CuddlerBoolFilter> IsNotEqualTo(bool b)
        {
            _query += $"{_key}~neq~{b.ToString().ToLower()}";

            return _andOr;
        }

        public CuddlerAndOrFilter<CuddlerBoolFilter> IsNotNull()
        {
            _query += $"{_key}~isnotnull~null";

            return _andOr;
        }

        public CuddlerAndOrFilter<CuddlerBoolFilter> IsNull()
        {
            _query += $"{_key}~isnull~null";

            return _andOr;
        }
    }

    public class FilterFactory
    {
        private readonly CuddlerDbQuery _dbQuery;

        public FilterFactory(CuddlerDbQuery dbQuery)
        {
            _dbQuery = dbQuery;
        }

        public CuddlerBoolFilter Add(Expression<Func<TModel, bool>> keySelector)
        {
            var operation = keySelector.Body as BinaryExpression;
            var left = GetLeft(operation);
            var right = GetRight(operation);

            var filter = new CuddlerBoolFilter(left!).IsEqualTo($"'{right}'")
                                                     .ToFilter();
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public CuddlerNumberFilter Add(Expression<Func<TModel, int?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new CuddlerNumberFilter(key);
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public CuddlerDateFilter Add(Expression<Func<TModel, DateTime?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new CuddlerDateFilter(key);
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public CuddlerStringFilter Add(Expression<Func<TModel, string?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new CuddlerStringFilter(key);
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        private static string? GetLeft(BinaryExpression? operation)
        {
            if (operation == null)
            {
                return null;
            }

            var leftMember = operation.Left as MemberExpression;

            return leftMember!.Member.Name;
        }

        private static string? GetRight(BinaryExpression? operation)
        {
            if (operation == null)
            {
                return null;
            }

            var rightConstant = operation.Right as ConstantExpression;
            object? rightResult;
            if (rightConstant == null)
            {
                var rightMember = operation.Right as MemberExpression;
                rightResult = Expression.Lambda(rightMember!)
                                        .Compile()
                                        .DynamicInvoke();
            }
            else
            {
                rightResult = rightConstant.Value;
            }

            var right = rightResult as string;
            return right;
        }
    }
}