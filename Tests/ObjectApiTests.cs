using FluentAssertions;
using System.Net;
using System.Text.Json;
using RestfulApiTests.Fixtures;
using RestfulApiTests.Models;
using Xunit;

namespace RestfulApiTests.Tests
{
    public class ObjectApiTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private string _createdObjectId;

        public ObjectApiTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        private async Task<string> CreateTestObject()
        {
            var payload = new
            {
                name = "Test Device",
                data = new
                {
                    year = 2024,
                    price = 1000
                }
            };

            var response = await _fixture.Client.CreateObject(payload);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var createdObject = JsonSerializer.Deserialize<ApiObject>(json);

            return createdObject.Id;
        }

        [Fact(DisplayName = "1. Get all objects")]
        public async Task GetAllObjects_ShouldReturnList()
        {
            var response = await _fixture.Client.GetAllObjects();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "2. Create object using POST")]
        public async Task CreateObject_ShouldReturnCreatedObject()
        {
            _createdObjectId = await CreateTestObject();

            _createdObjectId.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "3. Get single object by ID")]
        public async Task GetObjectById_ShouldReturnCorrectObject()
        {
            var id = await CreateTestObject();

            var response = await _fixture.Client.GetObjectById(id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            json.Should().Contain(id);
        }

        [Fact(DisplayName = "4. Update existing object")]
        public async Task UpdateObject_ShouldModifyObject()
        {
            var id = await CreateTestObject();

            var updatePayload = new
            {
                name = "Updated Device",
                data = new
                {
                    year = 2025,
                    price = 1500
                }
            };

            var response = await _fixture.Client.UpdateObject(id, updatePayload);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            json.Should().Contain("Updated Device");
        }

        [Fact(DisplayName = "5. Delete object")]
        public async Task DeleteObject_ShouldRemoveObject()
        {
            var id = await CreateTestObject();

            var deleteResponse = await _fixture.Client.DeleteObject(id);
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "6. Deleted object should not exist")]
        public async Task DeletedObject_ShouldReturnNotFound()
        {
            var id = await CreateTestObject();

            await _fixture.Client.DeleteObject(id);

            var response = await _fixture.Client.GetObjectById(id);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}