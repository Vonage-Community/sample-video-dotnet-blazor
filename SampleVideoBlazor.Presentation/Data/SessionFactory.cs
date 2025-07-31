using Vonage.Common.Monads;
using Vonage.Request;
using Vonage.Video;
using Vonage.Video.Authentication;
using Vonage.Video.Sessions;
using Vonage.Video.Sessions.CreateSession;

namespace SampleVideoBlazor.Presentation.Data;

internal sealed class SessionFactory(
    IVideoClient client, 
    IVideoTokenGenerator generator,
    Credentials credentials)
{
    internal async Task<Result<SessionService>> CreateSession() =>
        await client.SessionClient.CreateSessionAsync(CreateSessionRequest.Build()
                .WithLocation(IpAddress.Empty)
                .WithMediaMode(MediaMode.Routed).WithArchiveMode(ArchiveMode.Manual).Create())
            .Bind(response => this.BuildService(response.SessionId, Role.Moderator));

    internal Task<Result<SessionService>> JoinSession(string sessionId) => Task.FromResult(this.BuildService(sessionId,  Role.Subscriber));

    private Result<SessionService> BuildService(string sessionId, Role role) =>
        generator
            .GenerateToken(credentials, TokenAdditionalClaims.Parse(sessionId))
            .Map(token => new SessionInformation(credentials.ApplicationId, token.SessionId, token.Token))
            .Map(sessionInfo => new SessionService(client, sessionInfo, role));
}