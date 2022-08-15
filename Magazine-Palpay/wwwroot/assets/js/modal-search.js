$(document).ready(function () {
    var pageindex = 2;
    var NoMoredata = false;
    var inProgress = false;
    $('.scrolling').scroll(function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight && !inProgress && !NoMoredata) {
                $("#loadingdiv").show();
                $.post("/Merchants/List", {
                    "PageIndex": pageindex, "FullName": $('#FullName').val(),
                    "BranchName": $("#BranchName").val(), "IdNum": $("#IdNum").val(),
                    "PosId": $("#PosId").val(), "Outlet": $("#Outlet").val(),
                    "SerialNumber": $("#SerialNumber").val(), "MobileNo": $("#MobileNo").val()
                },
                    function (data) {
                        pageindex = pageindex + 1;
                        NoMoredata = data.NoMoredata;
                        $("#MerhcantList").append(data.html);
                        $("#loadingdiv").hide();
                        inProgress = false;
                    }
                );
            } 
    });

    $("#filterDown").click(function () {
        $("#FilterContent").slideToggle(1000);
    });
    $("#modal-submit").on("click", function () {
        var formData = new FormData(); // Currently empty
        $('#modal-form input, #modal-form select').each(function () {
            var input = $(this);
            formData.append(input.attr('name'), input.val());
        }); 
        PostData(formData);
    });
    $("#modal-reset").on("click", function () {
        $('#modal-form input, #modal-form select').each(function () {
            var input = $(this);
            $("input[name='" + input.attr('name')+"']").val('');
        });
    });

    function PostData(data) {
        try {
            $.ajax({
                type: 'POST',
                url: "/Merchants/List",
                data: data,
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#MerhcantList').html(res.html);
                        $('#modal-totaly-response').text(res.totalCount);
                    } else {
                        $('#MerhcantList').html("");
                        $('#modal-totaly-response').text("0");
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    };
});
function GetMerchantData(id,branchId, name) {
    $("#MerchantsId").val(id);
    $("#MerchantsBranchesId").val(branchId);
    $("#MerchantsId-label").text(name);
    $('#kt_modal_select_users').modal('hide');
};