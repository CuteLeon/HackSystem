using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackSystem.WebAPI.Model.WebLog;

public class WebAPILog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WebAPILogID { get; set; }

    public string RequestURI { get; set; }

    public string QueryString { get; set; }

    public string Method { get; set; }

    public string SourceHost { get; set; }

    public string UserAgent { get; set; }

    public string TraceIdentifier { get; set; }

    public bool IsAuthenticated { get; set; }

    public string IdentityName { get; set; }

    public string RequestBody { get; set; }

    public string ResponseBody { get; set; }

    public int StatusCode { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime FinishDateTime { get; set; }

    public long ElapsedTime { get; set; }

    public string Exception { get; set; }
}
