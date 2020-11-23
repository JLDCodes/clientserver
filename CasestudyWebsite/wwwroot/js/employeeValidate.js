$(function () {
    document.addEventListener("keyup", e => {
        $("#modalstatus").removeClass();//remove any existing css on div
        if ($("#EmployeeModalForm").valid()) {
            $("#modalstatus").attr("class", "badge badge-success");
            $("#modalstatus").text("data entered is valid");
        }
        else {
            $("#modalstatus").attr("class", "badge badge-danger");
            $("#modalstatus").text("fix errors");
        }
    });

    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }

        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: " needs valid email format"
            }
        }
    });//studentmodalform .validate 

    $.validator.addMethod("validTitle", (value) => {// custom rules
        return (value === "Mr." || value === "Ms." || value === "Mrs." || value === "Dr.");
    }, "");//.validator.addmethod

    $("#getbutton").mouseup(async (e) => {//click handler
        try {
            $("#TextBoxTitle").val("");
            $("#TextBoxLastname").val("");
            $("#TextBoxEmail").val("");
            $("#TextBoxPhone").val("");
            let validator = $("#EmployeeModalForm").validate();
            validator.resetForm();
            $("#modalstatus").attr("class", "");
            let lastname = $("#TextBoxFindLastname").val();
            $("#theModal").modal("toggle"); // pop the moda
            $("#modalstatus").text("please wait...");
            let response = await fetch(`api/employee/${lastname}`);
            if (!response.ok)
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json();
            if (data.Lastname !== "not found") {
                $("#TextBoxTitle").val(data.title);
                $("#TextBoxFirstname").val(data.firstName);
                $("#TextBoxLastname").val(data.lastName);
                $("#TextBoxPhone").val(data.phoneNo);
                $("#TextBoxEmail").val(data.email);
                $("#modalstatus").text("employee found");
                sessionStorage.setItem("Id", data.Id);
                sessionStorage.setItem("DepartmentId", data.departmentId);
                sessionStorage.setItem("Timer", data.Timer);
            } else {
                $("#TextBoxFirstname").val("not found");
                $("#TextBoxLastname").val("");
                $("#TextBoxEmail").val("");
                $("#TextBoxTitle").val("");
                $("#TextBoxPhone").val("");
                $("#modalstatus").text("no such student");
            }
        } catch (error) {
            $("#status").text(error.message);
        }
    });

});//main Jquery Function
