using System.Collections.Generic;
using System.Linq;

namespace Git.Domain.Dtos
{
    public class Notification<TData>
    {
        public TData Data { get; }

        public Notification()
        {
        }

        public Notification(TData outputData)
        {
            this.Data = outputData;
        }
    }

    public class Notification: Notification<object>
    {
        public bool HasOutputData => this.Data != null;

        public bool HasErrors => this.errors.Any();

        private IList<string> errors = new List<string>() ;

        public IList<string> Errors => errors;


        public Notification()
        {
            this.errors = new List<string>();
        }

        public Notification(object data): base(data)
        {
        }

        public void AddError( string message)
        {
            this.errors.Add(message);
        }
    }
}
