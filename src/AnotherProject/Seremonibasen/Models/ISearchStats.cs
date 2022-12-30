namespace AnotherProject.Seremonibasen.Models;

[SchemaObject]
public interface ISearchStats
{
	int PageIndex { get; set; }
	int PageSize { get; set; }
	int TotalResult { get; set; }
}