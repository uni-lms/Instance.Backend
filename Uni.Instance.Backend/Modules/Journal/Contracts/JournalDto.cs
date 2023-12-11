namespace Uni.Instance.Backend.Modules.Journal.Contracts; 

public class JournalDto {
  public required string CourseName { get; set; }
  public required List<JournalItem> Items { get; set; }
}