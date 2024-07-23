using System.Web.Http;

namespace v_wallet_api.Providers
{
    public class AuthenticationResult(string message, HttpRequestMessage requestMessage) : IHttpActionResult
    {
        public string Message { get; } = message;
        public HttpRequestMessage RequestMessage { get; } = requestMessage;

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await Execute();
        }

        private async Task<HttpResponseMessage> Execute()
        {
            var response = new HttpResponseMessage
            {
                RequestMessage = RequestMessage,
                ReasonPhrase = Message
            };

            return response;
        }
    }
}
