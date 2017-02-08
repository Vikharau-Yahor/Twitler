function DeleteTwit(id) {
    $.ajax({
        type: 'POST',
        url: '/User/DeleteTwit?twitId=' + id,
        success: function (response) {
            var postsFeed = $(".posts-feed-panel");
            postsFeed.empty();
            postsFeed.append(response);
            SetClickEventsForDeleteBtns();
        },
        error: function (data, error, mess) {
            console.log("error: " + data + " " + error + " " + mess);
        }
    });
}

function ShowModal(twitId) {
    $("#btn-accept-delete").attr("data-twit-id", twitId);
    $('#modDialog').modal('show');
}

function SetClickEventsForDeleteBtns() {
    $(".btn-delete-twit").click(function (e) {
        var twitId = this.getAttribute("data-twit-id");
        ShowModal(twitId);
    });
}

$(document).ready(function () {

    SetClickEventsForDeleteBtns();

    $("#btn-accept-delete").click(function(e) {
        var twitId = this.getAttribute("data-twit-id");
        if (twitId != "") {
            $.when(DeleteTwit(twitId)).done(SetClickEventsForDeleteBtns());
        }
        $('#modDialog').modal('hide');
    });

    $("#btn-reject-delete").click(function (e) {
        $('#modDialog').modal('hide');
    });

});