using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MessageBird.Resources
{
    public interface IResource
    {
        string Id { get; }
        string Name { get; }

        void FromResource(string resource);
        string ToResource();
    }
}
