$(function () {
    var code = $(".register-code-panel input");


    function displayMessage(success, message) {
        var alertDiv = $(".register-code-panel .alert");
        alertDiv.text(message);
        if (success) {
            alertDiv.removeClass("alert-danger").addClass("alert-success");
        } else {
            alertDiv.removeClass("alert-success").addClass("alert-danger");
        }

        alertDiv.removeClass("hidden");
    }

    $(".register-code-panel button").click(function(e) {
        $(".register-code-panel .alert").addClass("hidden");
        if (code.val().length === 0) {
            displayMessage(false, "Enter a code");
            return;
        }

            $.post("/registercode/register", { code: code.val() })
                .done(function(data) {
                    displayMessage(true, "The code has been registered.");
                    code.val("");
                })
                .fail(function(xhr, status, error) {
                    displayMessage(false, "Could not register the code");
                });
    });
});