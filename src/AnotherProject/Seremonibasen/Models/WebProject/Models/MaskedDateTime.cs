namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class MaskedDateTime
{
	public const int WeekdayNotSelectedValue = -1;


	public DateTime Date { get; set; }

	public int Year => Date.Year;
	public int Day =>  -1;
	public int Month =>  -1;

	/// <summary>
	/// Weekday, not connected to the date.
	/// </summary>
	public int CustomWeekday { get; set; } = WeekdayNotSelectedValue;

	public int TotalMinutes =>  -1;

}