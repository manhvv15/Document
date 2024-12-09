namespace Ichiba.Libs.DocumentSdk.Models;

public class ReportHistory
{
    public string Code { get; set; }           
    public string Name { get; set; }           
    public string Link { get; set; }           
    public int Status { get; set; }            
    public string ReportGroupName { get; set; }    
    public string UserProfileId { get; set; }  
    public Guid WorkspaceId { get; set; }      
    public DateTime ExportedAt { get; set; }  
    public string ExportedBy { get; set; }     
}
