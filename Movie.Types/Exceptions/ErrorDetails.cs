using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Movie.Api.Exceptions
{
	[DataContract]
    public class ErrorDetails: Exception
    {
		[DataMember(Name = "statusCode")]
		public int StatusCode { get; set; }
		[DataMember(Name = "description")]
		public string Description { get; set; }


		//public override string ToString()
		//{
		//	return JsonConvert.SerializeObject(this);
		//}
	}
}
