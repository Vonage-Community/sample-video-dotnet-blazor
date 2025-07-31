# Vonage Video API Blazor Sample

A Blazor sample application demonstrating how to integrate Vonage Video API for real-time video communications. 
This sample provides a complete implementation of video sessions, streaming, recording, and archive
management.

## Prerequisites

- .NET 9.0 or later
- A [Vonage Video API account](https://dashboard.nexmo.com/sign-up?utm_source=DEV_REL&utm_medium=github&utm_campaign=csharp-client-library)
- A Vonage Application with enabled Video capabilities. Check our [Getting started guide](https://developer.vonage.com/en/video/getting-started?source=video).

## Getting Started

### 1. Configure Vonage Credentials

Open the `appsettings.json` file and add your Vonage credentials:

```json
{
  "vonage": {
    "Application.Id": "Your AppId",
    "Application.Key": "Your PrivateKey"
  }
}
```

### 2. Run the Application

```bash
dotnet run
```

The application will start and be available at `https://localhost:7145` (specified in `launchSettings.json`).

## Usage Guide

### Creating or Joining a Session

When you first launch the application, you'll be presented with two options:

1. **Create New Session**: Start as a moderator with full control over the session.
2. **Join Existing Session**: Enter an existing session ID to join as a participant.

### Video Streaming

Once in a session, you can:

- Click **"Start Stream"** to begin sending your video and audio feed
- View other participant's video feeds in real-time
- Control your own audio/video settings

### Session Recording (Moderator Only)

As a moderator, you have additional capabilities:

- **Start archiving**: Begin recording the current session
- **Stop archiving**: End the current recording
- **Add experience composer**: Include video content from an external URL into the session, as a new participant

### Managing Archives

Navigate to the **Archives** page to:

- View all recordings associated with your account
- See detailed information including:
    - Creation date and time
    - Archive name/ID
    - Recording status
    - Duration
- Watch recorded sessions directly in the browser

## Troubleshooting

### Common Issues

**Invalid Credentials Error**

- Verify your Application ID and Private Key in `appsettings.json`
- Ensure your Vonage account is active and has Video API access

## Contributing

We welcome contributions! Please feel free to submit issues, feature requests, or pull requests to help improve this
sample.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For Vonage Video API specific questions, visit
the [Vonage Developer Documentation](https://developer.vonage.com/en/video/overview).

For issues with this sample, please create an issue in this repository.