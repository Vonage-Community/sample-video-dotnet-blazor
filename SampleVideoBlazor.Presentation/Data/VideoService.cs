using System.Net;
using Vonage.Common.Monads;
using Vonage.Server;
using Vonage.Video;
using Vonage.Video.Archives;
using Vonage.Video.Archives.CreateArchive;
using Vonage.Video.Archives.GetArchives;
using Vonage.Video.Archives.StopArchive;
using Vonage.Video.ExperienceComposer;
using Vonage.Video.ExperienceComposer.Start;

namespace SampleVideoBlazor.Presentation.Data;

internal sealed class VideoService(IVideoClient client)
{
    public Task<Result<Archive>> TryStartArchiving(SessionInformation session) =>
        BuildCreateArchiveRequest(session)
            .BindAsync(request => client.ArchiveClient.CreateArchiveAsync(request));

    private static Result<CreateArchiveRequest> BuildCreateArchiveRequest(SessionInformation session) =>
        CreateArchiveRequest.Build().WithApplicationId(Guid.Parse(session.ApplicationId)).WithSessionId(session.SessionId).WithName("Blazor Sample App")
            .WithStreamMode(StreamMode.Auto)
            .WithResolution(RenderResolution.FullHighDefinitionLandscape)
            .WithLayout(new Layout(LayoutType.BestFit, null, LayoutType.BestFit))
            .WithMultiArchiveTag("YOLO")
            .Create();

    public Task StopArchiveAsync(Archive archive) =>
        client.ArchiveClient.StopArchiveAsync(StopArchiveRequest.Build()
            .WithApplicationId(Guid.Parse(archive.ApplicationId))
            .WithArchiveId(Guid.Parse(archive.Id))
            .Create());

    public Task<Result<Session>> StartExperienceComposerAsync(Uri url, SessionInformation session) =>
        StartRequest.Build().WithApplicationId(new Guid(session.ApplicationId)).WithSessionId(session.SessionId)
            .WithToken(session.Token).WithUrl(url).WithResolution(RenderResolution.FullHighDefinitionLandscape)
            .WithName("Exp").Create()
            .BindAsync(request => client.ExperienceComposerClient.StartAsync(request));

    public async Task<Result<Archive[]>> ListArchives(SessionInformation session) =>
        await client.ArchiveClient.GetArchivesAsync(GetArchivesRequest.Build().WithApplicationId(new Guid(session.ApplicationId))
                .Create())
            .Map(response => response.Items);
}