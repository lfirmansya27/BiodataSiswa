namespace BiodataSiswa.Domain.Entities;

public class Student
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public required string StudentNumber { get; set; }
    public required string FullName { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
