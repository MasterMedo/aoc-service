using System;
using Main.Core.FakeProject;

namespace FakeProject
{
    public class FakeService : IFakeService
    {
        public FakeResponse FakeMethod(FakeRequest request)
        {
            return new FakeResponse {FakeProperty = "fake it till you make it"};
        }
    }
}
