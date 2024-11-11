namespace Ichiba.Libs.DocumentSdk.Models;

public class TemplateData
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Size { get; set; }
    public string Type { get; set; } 
    public string Link { get; set; } 
    public Guid WorkspaceId { get; set; }
}
