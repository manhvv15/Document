﻿namespace Ichiba.Libs.DocumentSdk.Models;
public class ReportHistoryDto
{
    public string UserProfileId { get; set; }
    public string ReportCode { get; set; }
    public Guid WorkspaceId { get; set; }
    public string Link { get; set; }
}