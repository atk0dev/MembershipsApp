$(function() {
    var pwdLinkHover = $("#pwdLink").hover(onCloseLogin);
    var resetPassword = $("#resetPwd").click(onResetPassword);

    function onCloseLogin() {
        $("div[data-login-user-area]").removeClass("open");
    }

    function onResetPassword() {
        var email = $(".modal-dialog .reset-email").val();
        var aft = $("[name='__RequestVerificationToken']").val();
        var url = "/Account/ForgotPasswordConfirmation";

        $.post("/Account/ForgotPassword", { __RequestVerificationToken: aft, email: email })
            .done(function (data) {
                window.location.href = url;
            })
            .fail(function (xhr, status, error) {
                console.log(error);
                window.location.href = url;
            });
    }
});