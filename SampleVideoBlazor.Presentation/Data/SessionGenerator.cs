using Vonage.Common.Monads;
using Vonage.Request;
using Vonage.Video;
using Vonage.Video.Authentication;
using Vonage.Video.Sessions;
using Vonage.Video.Sessions.CreateSession;

namespace SampleVideoBlazor.Presentation.Data;

internal sealed class SessionGenerator(
    IVideoClient client,
    IVideoTokenGenerator generator,
    Credentials credentials)
{
    public async Task<Result<SessionInformation>> CreateSession() =>
        (await client.SessionClient.CreateSessionAsync(CreateSessionRequest.Build()
            .WithLocation(IpAddress.Empty)
            .WithMediaMode(MediaMode.Routed).WithArchiveMode(ArchiveMode.Manual).Create()))
        .Bind(sessionResponse => generator
            .GenerateToken(credentials, TokenAdditionalClaims.Parse(sessionResponse.SessionId))
            .Map(token => new SessionInformation(credentials.ApplicationId, token.SessionId, token.Token)));
}