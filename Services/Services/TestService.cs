using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Services.Contracts;

namespace Services.Services
{
    public class TestService : ITestService, IScopedDependency
    {
        public string GetTest()
        {
            return "OK";
        }
    }
}
