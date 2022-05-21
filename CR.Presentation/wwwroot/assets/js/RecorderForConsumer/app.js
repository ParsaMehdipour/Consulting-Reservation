//webkitURL is deprecated but nevertheless
URL = window.URL || window.webkitURL;

var gumStream; 						//stream from getUserMedia()
var rec; 							//Recorder.js object
var input; 							//MediaStreamAudioSourceNode we'll be recording

// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext //audio context to help us record

var recordButton = document.getElementById("recordButton");
var stopButton = document.getElementById("stopButton");
var pauseButton = document.getElementById("pauseButton");

//add events to those 2 buttons
recordButton.addEventListener("click", startRecording);
stopButton.addEventListener("click", stopRecording);
pauseButton.addEventListener("click", pauseRecording);

function startRecording() {
    console.log("recordButton clicked");

    /*
        Simple constraints object, for more advanced audio features see
        https://addpipe.com/blog/audio-constraints-getusermedia/
    */

    var constraints = { audio: true, video: false };

    /*
      Disable the record button until we get a success or fail from getUserMedia() 
  */

    recordButton.disabled = true;
    stopButton.disabled = false;
    pauseButton.disabled = false;

    /*
        We're using the standard promise based getUserMedia() 
        https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
    */

    navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
        console.log("getUserMedia() success, stream created, initializing Recorder.js ...");

        /*
            create an audio context after getUserMedia is called
            sampleRate might change after getUserMedia is called, like it does on macOS when recording through AirPods
            the sampleRate defaults to the one set in your OS for your playback device

        */
        audioContext = new AudioContext();

        //update the format 
        //document.getElementById("formats").innerHTML =
        //"Format: 1 channel pcm @ " + audioContext.sampleRate / 1000 + "kHz";

        /*  assign to gumStream for later use  */
        gumStream = stream;

        /* use the stream */
        input = audioContext.createMediaStreamSource(stream);

        /* 
            Create the Recorder object and configure to record mono sound (1 channel)
            Recording 2 channels  will double the file size
        */
        rec = new Recorder(input, { numChannels: 1 });

        //start the recording process
        rec.record();

        console.log("Recording started");

    }).catch(function (err) {
        //enable the record button if getUserMedia() fails
        recordButton.disabled = false;
        stopButton.disabled = true;
        pauseButton.disabled = true;
    });
}

function pauseRecording() {
    console.log("pauseButton clicked rec.recording=", rec.recording);
    if (rec.recording) {
        //pause
        rec.stop();
        pauseButton.innerHTML = "شروع دوباره";
    } else {
        //resume
        rec.record();
        pauseButton.innerHTML = '<i class="fa fa - pause pause"></i>';

    }
}

function stopRecording() {
    console.log("stopButton clicked");

    //disable the stop button, enable the record too allow for new recordings
    stopButton.disabled = true;
    recordButton.disabled = false;
    pauseButton.disabled = true;

    //reset button just in case the recording is stopped while paused
    pauseButton.innerHTML = '<i class="fa fa - pause pause"></i>';

    //tell the recorder to stop the recording
    rec.stop();

    //stop microphone access
    gumStream.getAudioTracks()[0].stop();


    //create the wav blob and pass it on to createDownloadLink
    rec.exportWAV(Send);
}

function createDownloadLink1(blob) {

    var url = URL.createObjectURL(blob);
    var au = document.createElement('audio');
    var li = document.createElement('li');
    var link = document.createElement('a');

    //name of .wav file to use during upload and download (without extendion)
    var filename = new Date().toISOString();

    //add controls to the <audio> element
    au.controls = true;
    au.src = url;

    //save to disk link
    //link.href = url;
    //link.download = filename+".wav"; //download forces the browser to donwload the file using the  filename
    //link.innerHTML = " | ذخیره در سیستم | ";

    //add the new audio element to li
    li.appendChild(au);

    //add the filename to the li
    /*li.appendChild(document.createTextNode(filename + ".wav "));*/

    //add the save to disk link to li
    li.appendChild(link);

    //upload link
    var upload = document.createElement('a');
    upload.href = "#";
    upload.innerHTML = "ارسال به مشاور";
    upload.addEventListener("click",
        function (event) {
            var xhr = new XMLHttpRequest();
            xhr.onload = function (e) {
                if (this.readyState === 4) {
                    console.log("Server returned: ", e.target.responseText);
                }
            };

            var file = new File([blob], "wav");

            var formData = new FormData();

            formData.append("file", file);
            formData.append("chatUserId", ChatUserId);
            formData.append("messageFlag", 0);

            $.ajax({
                url: '/api/Chat/AddNewConsumerVoiceMessage',
                type: 'post',
                data: formData,
                contentType: false,
                processData: false,
                cache: false,
                beforeSend: function (x) {
                    $('#loading').show();
                },
                success: function (data) {
                    if (data.isSuccess === true) {
                        GetMessages();
                    }
                    else {
                        swal.fire(
                            'هشدار!',
                            data.message,
                            'warning'
                        );

                    }
                },
                complete: function () {
                    $('#loading').hide();
                },
                fail: function () {
                    $('#loading').hide();
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });

            //xhr.open("POST", "upload.php", true);
            //xhr.send(fd);
        });
    li.appendChild(document.createTextNode(" "));//add a space in between
    li.appendChild(upload);//add the upload link to li

    //add the li element to the ol
    recordingsList.appendChild(li);
}

function Send(blob) {

    var file = new File([blob], "wav");

    var formData = new FormData();

    formData.append("file", file);
    formData.append("chatUserId", ChatUserId);
    formData.append("messageFlag", 0);

    $.ajax({
        url: '/api/Chat/AddNewConsumerVoiceMessage',
        type: 'post',
        data: formData,
        contentType: false,
        processData: false,
        cache: false,
        beforeSend: function (x) {
            $('#loading').show();
        },
        success: function (data) {
            if (data.isSuccess === true) {

                $('#image').val('');

                var today = new Date();

                var time = today.getHours() + ":" + today.getMinutes();
                var user = ChatUserId;
                var messageFlag = 0;

                connection.invoke("SendVoice", user, messageFlag, data.data).catch(function (err) {
                    return console.error(err.toString());
                });

                var body2 = $("#messages-Body-Consumer").html();

                body2 += '<li class="media received">';
                body2 += '<div class="media-body">';
                body2 += '<div class="msg-box">';
                body2 += '<div>';
                body2 += '<div class="chat-msg-attachments">';
                body2 += '<audio controls>';
                body2 += '<source src="/' + data.data + '" type="audio/ogg">';
                body2 += '</audio>';
                body2 += '</div>';
                body2 += '<ul class="chat-msg-info">';
                body2 += '<li>';
                body2 += '<div class="chat-time">';
                body2 += '<span>' + time + '</span>';
                body2 += '</div>';
                body2 += '</li>';
                body2 += '</ul>';
                body2 += '</div>';
                body2 += '</div>';
                body2 += '</div>';
                body2 += '</li>';

                $("#messages-Body-Consumer").html("");
                $("#messages-Body-Consumer").html(body2);
            }
            else {
                swal.fire(
                    'هشدار!',
                    data.message,
                    'warning'
                );

            }
            $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);
        },
        complete: function () {
            $('#loading').hide();
        },
        fail: function () {
            $('#loading').hide();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

}