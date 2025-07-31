var session, publisher;

window.initializeStream = (apiKey, sessionId, token) => {
    session = OT.initSession(apiKey, sessionId)
    publisher = OT.initPublisher('publisher', { insertMode: 'append',
        width: '100%',
        height: '100%'}, _ => {
        loadDevices();
    });
    session.connect(token, function (error) {
        if (error) {
            console.error('Failed to connect', error);
        } else {
            console.log('Session connected.')
            session.publish(publisher, function (error) {
                if (error) {
                    console.error('Failed to publish', error);
                }
            });
        }
    });

    session.on('streamCreated', function (event) {
        console.log('Stream created.')
        session.subscribe(event.stream, 'subscribers', {
            insertMode: 'append',
            width: '100%',
            height: '100%'
        }, function (error) {
            if (error) {
                console.error('Failed to subscribe', error);
            }
        });
    });

};

window.disposeStream = () => {
    publisher.publishVideo(false);
    session.disconnect();
    session.unpublish(publisher);
    publisher.destroy();
};

window.onbeforeunload = function () {
    disposeStream();
}

window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
            alert("Copied to clipboard!");
        })
            .catch(function (error) {
                alert(error);
            });
    }
};

function loadDevices() {

    var cameras = document.getElementById('cameras');
    cameras.addEventListener('change', function (event) {
        publisher.setVideoSource(event.target.value);
    });

    var mics = document.getElementById('mics');
    mics.addEventListener('change', function (event) {
        publisher.setAudioSource(event.target.value);
    });

    var videoSource = publisher.getVideoSource();
    var audioSource = publisher.getAudioSource();

    OT.getDevices((err, devices) => {

        if (err) {
            console.error(err);
        }

        var audioInputs = devices.filter((device) => device.kind === 'audioInput');
        var videoInputs = devices.filter((device) => device.kind === 'videoInput');

        videoInputs.forEach((device, idx) => {
            var option = document.createElement("option");
            var txt = document.createTextNode(device.label);
            option.appendChild(txt);
            option.setAttribute("value", device.deviceId);

            if (videoSource != null && device.deviceId === videoSource.deviceId) {
                option.selected = true;
            }
            cameras.insertBefore(option, cameras.lastChild);
        });

        audioInputs.forEach((device, idx) => {
            var option = document.createElement("option");
            var txt = document.createTextNode(device.label);
            option.appendChild(txt);
            option.setAttribute("value", device.deviceId);

            if (audioSource != null && device.deviceId === audioSource.deviceId) {
                option.selected = true;
            }
            mics.insertBefore(option, mics.lastChild);
        });
    });
}