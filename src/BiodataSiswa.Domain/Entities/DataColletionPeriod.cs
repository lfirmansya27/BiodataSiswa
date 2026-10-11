namespace BiodataSiswa.Domain.Entities;

public class DataCollectionPeriod
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string AcademicYear { get; set; }
    public DateTime StartsAtUtc { get; set; }
    public DateTime EndsAtUtc { get; set; }
    public string Status { get; set; } = "draft";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}