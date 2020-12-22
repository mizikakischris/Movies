using Movie.Api.Exceptions;
using Movie.Types.Dtos;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Movie.Types.Responses
{
    [DataContract]
    public class Response<T> where T : class
    {
        [DataMember]
        public string Version { get { return "1.1"; } }

        [DataMember(Name = "payload")]
        public Payload<T> Payload { get; set; }

        [DataMember(Name = "exception")]
        public ErrorDetails Exception { get; set; }

    }


    public class Payload<T> where T : class
    {
        public T PayloadObject;
        public List<T> Movies { get; set; }
        public List<T> Actors { get; set; }
    }
}
