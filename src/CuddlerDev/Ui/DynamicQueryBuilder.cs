using System.Linq.Expressions;
using Kendo.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CuddlerDev.Ui;

public class DynamicQueryBuilder<TModel> where TModel : class
{
    private readonly DynamicDbQuery _dbQuery;

    public DynamicQueryBuilder()
    {
        _dbQuery = new DynamicDbQuery(typeof(TModel));
    }

    public DynamicDbQuery AsQueryObj()
    {
        return _dbQuery;
    }

    public DynamicQueryBuilder<TModel> Filter(Action<FilterFactory> configurator)
    {
        configurator(new FilterFactory(_dbQuery));

        return this;
    }

    public DynamicQueryBuilder<TModel> OrderBy<TKey>(Expression<Func<TModel, TKey>> keySelector)
    {
        var key = GetKey(keySelector);
        _dbQuery.SortList.Add(new SortDescriptor(key, ListSortDirection.Descending));
        return this;
    }

    public DynamicQueryBuilder<TModel> OrderByAscending<TKey>(Expression<Func<TModel, TKey>> keySelector)
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

    public IQueryable<TModel> Queryable()
    {
        throw new NotImplementedException();
    }

    public class DynamicBoolFilter : DynamicBaseFilter
    {
        private readonly DynamicAndOrFilter<DynamicBoolFilter> _andOr;

        public DynamicBoolFilter(string key) : base(key)
        {
            _andOr = new DynamicAndOrFilter<DynamicBoolFilter>(this);
        }

        public DynamicAndOrFilter<DynamicBoolFilter> IsEqualTo(bool b)
        {
            _query += $"{_key}~eq~{b.ToString().ToLower()}";

            return _andOr;
        }

        public DynamicAndOrFilter<DynamicBoolFilter> IsEqualTo(string b)
        {
            _query += $"{_key}~eq~{b}";

            return _andOr;
        }

        public DynamicAndOrFilter<DynamicBoolFilter> IsNotEqualTo(bool b)
        {
            _query += $"{_key}~neq~{b.ToString().ToLower()}";

            return _andOr;
        }

        public DynamicAndOrFilter<DynamicBoolFilter> IsNotNull()
        {
            _query += $"{_key}~isnotnull~null";

            return _andOr;
        }

        public DynamicAndOrFilter<DynamicBoolFilter> IsNull()
        {
            _query += $"{_key}~isnull~null";

            return _andOr;
        }
    }

    public class FilterFactory
    {
        private readonly DynamicDbQuery _dbQuery;

        public FilterFactory(DynamicDbQuery dbQuery)
        {
            _dbQuery = dbQuery;
        }

        public DynamicBoolFilter Add(Expression<Func<TModel, bool>> keySelector)
        {
            var operation = keySelector.Body as BinaryExpression;
            var left = GetLeft(operation);
            var right = GetRight(operation);

            var filter = new DynamicBoolFilter(left!).IsEqualTo($"'{right}'")
                                                     .ToFilter();
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public DynamicNumberFilter Add(Expression<Func<TModel, int?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new DynamicNumberFilter(key);
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public DynamicDateFilter Add(Expression<Func<TModel, DateTime?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new DynamicDateFilter(key);
            _dbQuery.FilterList.Add(filter);

            return filter;
        }

        public DynamicStringFilter Add(Expression<Func<TModel, string?>> keySelector)
        {
            var key = GetKey(keySelector);

            var filter = new DynamicStringFilter(key);
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