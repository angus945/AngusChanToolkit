using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface IDatabaseReader<T>
{
    public T GetByIndex(int index);
    public IEnumerable<T> GetAll();
    public IEnumerable<T> GetItems(Expression<Func<T, bool>> expression);
}