$(function() {
    var registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);

    function onRegisterUserClick() {
        var url = "/account/registeruserasync";
        var aft = $("[name='__RequestVerificationToken']").val();
        var name = $(".register-user-panel .first-name").val();
        var email = $(".register-user-panel .email").val();
        var password = $(".register-user-panel .password").val();

        $.post(url, { __RequestVerificationToken: aft, email: email, name: name, password: password })
            .done(function (data) {
                var parsed = $.parseHTML(data);
                var hasErrors = $(parsed).find('[data-valmsg-summary]').text().replace(/\n|\r/g, '').length > 0;
                if (hasErrors) {
                    $(".register-user-panel").html(data);
                    registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);
                } else {
                    registerUserButton = $(".register-user-panel button").click(onRegisterUserClick);
                    location.reload(true);
                }
            })
            .fail(function (xhr, status, error) {
                console.log(error);
            });
    }
})