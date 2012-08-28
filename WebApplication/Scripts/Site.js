viewModel = {
    votes: ko.observableArray([])
};

ko.applyBindings(viewModel);

function refresh() {
    $.ajax({
        type: "GET",
        url: "/api/votes",

        async: true,
        cache: false,
        timeout: 5000,

        success: function (data) {

            viewModel.votes([]);
            viewModel.votes(data);

            setTimeout(
                'refresh()',
                '0');
        },
        error: function () {
            setTimeout(
                'refresh()',
                '5000');
        }
    });
};

$(document).ready(function () {
    refresh();
});
