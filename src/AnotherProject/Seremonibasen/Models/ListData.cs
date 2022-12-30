using AnotherProject.Seremonibasen.Models.Abstractions;

namespace AnotherProject.Seremonibasen.Models;

public class ListData<TListData> : BasePocoObject, ISearchStats
{
    public List<TListData> List { get; set; } = new();

    #region Interface implementations

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalResult { get; set; }

    #endregion
}