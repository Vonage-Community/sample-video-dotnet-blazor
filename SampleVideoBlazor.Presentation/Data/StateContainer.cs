using Vonage.Common.Monads;
using Vonage.Video;
using Vonage.Video.Authentication;

namespace SampleVideoBlazor.Presentation.Data;

internal sealed class StateContainer
{
    private Maybe<SessionService> sessionService;
    public bool HasService => this.sessionService.IsSome;
    public Maybe<SessionService> GetService() => this.sessionService;
    public void SetService(SessionService service) => this.sessionService = service;
}

internal sealed class SessionService(IVideoClient client, SessionInformation session, Role role)
{
    public Role Role => role;
    public SessionInformation Session => session;
}