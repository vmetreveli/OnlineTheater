using System.Linq.Expressions;

namespace OnlineTheater.Domains.Primitives.Specifications;

/// <summary>
///     Represents the abstract base class for specifications.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class Specification<TEntity>
    where TEntity : EntityBase
{
    protected Specification(Expression<Func<TEntity, bool>>? criteria) =>
        Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>>? IncludeExpressions { get; } = new();
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddIncLide(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;

    // public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
    // specification.Criteria();
}