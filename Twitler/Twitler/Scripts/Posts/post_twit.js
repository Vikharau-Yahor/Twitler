function PostTwit(data) {
    var json = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        url: '/User/PostTwit',
        data: json,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            var postsFeed = $(".posts-feed-panel");
            postsFeed.empty();
            postsFeed.append(response);
        },
        error: function (data, error, mess) {
            console.log("error: " + data + " " + error + " " + mess);
        }
    });
}

function ValidatePostMessage(data) {
    if (data == null) {
        console.log("ValidatePostMessage take empty data");
        return false;
    }
    if (data.Message == "") {
        alert("Введите текст твита!");
        return false;
    }
    if (data.Message.length > 500) {
        alert("Слишком длинный твит (максимум 500 символов)");
        return false;
    }
    return true;
}


function CollectDataOfNewTwit() {
    var data = {}
    data.Message = $("#text-post-message").val();
    var now = new Date();
    var options = {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric'
    };
    data.DatePost = now.toLocaleString("ru", options);
    return data;
}

$(document).ready(function () {

    $("#btn-post-twit").click(function (e) {
        var newTwit = CollectDataOfNewTwit();
        //if (ValidatePostMessage(newTwit))
            PostTwit(newTwit);
    });

});