using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace HackSystem.WebAPI.Model.Mock;

    public class MockRouteLogDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteLogID { get; set; }

        public int RouteID { get; set; }

        public string ConnectionID { get; set; }

        public string URI { get; set; }

        public string Method { get; set; }

        public string SourceHost { get; set; }

        public int StatusCode { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        public MockType MockType { get; set; }

        public string ForwardAddress { get; set; }

        public string ForwardMethod { get; set; }

        public string ForwardRequestBodyTemplate { get; set; }

        public HttpStatusCode ForwardResponseStatusCode { get; set; }

        public string ForwardResponseBody { get; set; }

        public MockType ForwardMockType { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime ForwardDateTime { get; set; }

        public DateTime FinishDateTime { get; set; }

        public MockRouteLogStatus MockRouteLogStatus { get; set; }

        public string Exception { get; set; }
    }
