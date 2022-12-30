using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
[Table("EmployeeNumberGenerator")]
public class EmployeeNumberRecord
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Number { get; set; }
	public Guid PersonId { get; set; }
}