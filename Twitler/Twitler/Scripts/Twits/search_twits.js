function SearchTwits(data) {
    var sendData = encodeURIComponent(data);
    $.ajax({
        type: 'GET',
        url: '/User/SearchTwits?searchString=' + sendData,
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

$(document).ready(function () {

    $("#btn-search-twit").click(function (e) {
        var searchString = $("#text-search").val();
        if (searchString != "") {
            SearchTwits(searchString);
        }
    });

});