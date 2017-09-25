function loadDetials(id) {
    $("#logID").html("");
    $("#logKeyWord").html("");
    $("#ShortMessage").html("");
    $("#CreateDate").html("");
    $("#LogLeve").html("");
    $("#IpAddress").html("");
    $("#ServerIpAddress").html("");
    $("#PageUrl").html("");
    $("#ReferrerUrl").html("");
    $("#FullMessage").html("");
    $.ajax({
        type: "Post",
        datatype: "Json",
        url: "/Log/Detials",
        data: { id: id },
        success: function (data) {
            $("#logID").html(data.id);
            $("#logKeyWord").html(data.keyWord);
            $("#ShortMessage").html(data.shortMessage);
            $("#CreateDate").html(data.createDate);
            $("#LogLeve").html(data.logLeve);
            $("#IpAddress").html(data.ipAddress);
            $("#ServerIpAddress").html(data.serverIpAddress);
            $("#PageUrl").html(data.pageUrl);
            $("#ReferrerUrl").html(data.referrerUrl);
            $("#FullMessage").html(data.fullMessage);
            $('#logDetial').modal("show");
        }
    });



}