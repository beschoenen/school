var base = '/api/';

function deleteDevice(deviceCode, button) {
    var sure = confirm("Are you sure?");

    if(sure) {
        $.ajax({
            type: "DELETE",
            url: base + "devices/" + deviceCode,
            headers: {
                'X-Device-Token': getCookie('Device')
            },
            success: function() {
                $(button).closest('.device-box').remove();
            }
        });
    }
}

function deleteEntry(entryCode, button) {
    var sure = confirm("Are you sure?");

    if(sure) {
        $.ajax({
            type: "DELETE",
            url: base + "entries/" + entryCode,
            headers: {
                'X-Device-Token': getCookie('Device')
            },
            success: function() {
                $(button).closest('.entry-box').remove();
            }
        });
    }
}

function deleteUser(userId, button) {
    var sure = confirm("Are you sure?");

    if(sure) {
        $.ajax({
            type: "DELETE",
            url: base + "users/" + userId,
            headers: {
                'X-Device-Token': getCookie('Device')
            },
            success: function() {
                $(button).closest('.user-box').remove();
            }
        });
    }
}