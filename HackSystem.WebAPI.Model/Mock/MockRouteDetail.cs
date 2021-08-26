using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackSystem.WebAPI.Model.Mock;

public class MockRouteDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RouteID { get; set; }

    public string RouteName { get; set; }

    public bool Enabled { get; set; }

    public string MockURI { get; set; }

    public string MockMethod { get; set; }

    public string MockSourceHost { get; set; }

    public int DelayDuration { get; set; }

    public int StatusCode { get; set; }

    public string ResponseBodyTemplate { get; set; }

    public MockType MockType { get; set; }

    public string ForwardAddress { get; set; }

    public string ForwardMethod { get; set; }

    public string ForwardRequestBodyTemplate { get; set; }

    public MockType ForwardMockType { get; set; }
}
