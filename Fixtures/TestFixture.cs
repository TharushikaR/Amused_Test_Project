using RestfulApiTests.Client;

namespace RestfulApiTests.Fixtures
{
    public class TestFixture
    {
        public RestClientHelper Client { get; }

        public TestFixture()
        {
            Client = new RestClientHelper();
        }
    }
}