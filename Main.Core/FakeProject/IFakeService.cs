using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Core.FakeProject
{
    public interface IFakeService
    {
        FakeResponse FakeMethod(FakeRequest request);
    }
}
