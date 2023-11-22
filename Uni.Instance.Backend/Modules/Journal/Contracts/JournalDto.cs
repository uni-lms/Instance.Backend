namespace Uni.Instance.Backend.Modules.Journal.Contracts; 

public class JournalDto {
  public string CourseName { get; set; }
  public List<JournalItem> Items { get; set; }
}