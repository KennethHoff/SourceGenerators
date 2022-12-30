using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CourseDay
{
    public DateTime DateAndTimeFrom { get; set; }
    [Required]
    [StringLength(500)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column("Length")]
    public int LengthInMinutes { get; set; }
    
    [NotMapped]
    public TimeSpan Length => TimeSpan.FromMinutes(LengthInMinutes);
    public Guid CourseId { get; set; }
	
	public required DateTimeOffset DateAndTimeFromUtc { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    // ReSharper disable once IdentifierTypo
    public virtual ICollection<CourseDayParticipation> Participations { get; set; } = new List<CourseDayParticipation>();

}
