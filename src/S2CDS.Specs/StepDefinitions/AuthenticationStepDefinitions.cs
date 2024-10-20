using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using S2CDS.Api.Dtos.v1.Authentication;
using S2CDS.Api.Infrastructure.Repositories.User;
using System.Net.Http.Json;
using System.Text;

namespace S2CDS.Specs.StepDefinitions
{
    [Binding]
    public class AuthenticationStepDefinitions
    {
        public WebAppFactory _factory = new();
        public HttpClient Client { get; set; }
        public HttpResponseMessage Response { get; set; } = null!;

        public AuthenticationStepDefinitions()
        {
            Client = _factory.CreateDefaultClient(new Uri($"https://localhost:7084/"));
        }

        [Given("um usuario valido")]
        public void DadoUmUsuarioValido()
        {
            var user = new UserEntity
            {
                Username = "@fulano",
                Email = "fulano@gmail.com",
                Id = Guid.NewGuid().ToString(),
                Password = "fulano123",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _factory.UserRepository.Init(new() { user });
        }

        [When("passar as credenciais via POST ao '/api/v1/authentication'")]
        public async Task QuandoPassarAsCredenciaisViaPost()
        {
            var data = new AuthenticationRequest
            {
                EmailOrUsername = "@fulano",
                Password = "fulano123"
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync("api/v1/authentication", content);
        }

        [Then(@"deve retonar um status code (.*)")]
        public void EntaoDeveRetornarUmStatusCode200(int result)
        {
            var actual = Response.StatusCode;
            actual.ToString().Should().Be(result.ToString());
        }

        [Then("retornar o token")]
        public async Task ERetornarOToken()
        {
            var actual = await Response.Content.ReadFromJsonAsync<string>();
            actual.Should().NotBeEmpty();
        }
    }
}
