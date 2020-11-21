$(function () {
    $("#getbutton").click(async (e) => { //click event handler makes asynchronous fetch to server
        try {
            let email = $("#TextBoxEmail").val();
            $("#status").text("please wait...");
            let response = await fetch(`api/employee/${email}`);
            if (response.ok) {
                let data = await response.json(); // this returns a promise, so we await it
                if (data.lastName !== "not found") {
                    $("#email").text(data.email);
                    $("#title").text(data.title);
                    $("#firstName").text(data.firstName);
                    $("#phone").text(data.phoneNo);
                    $("#status").text("employee found");
                } else {
                    $("#firstName").text("not found");
                    $("#email").text("");
                    $("#title").text("");
                    $("#phone").text("");
                    $("#status").text("no such employee");
                }
            } else if (response.status !== 404) { // probably some other client side error
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            }// else
        } catch (error) {
            $("#status").text(error.message);
        }// try/catch     

    });//click event   

}); //jQuery ready method 

//server was rearched but server had a problem with the call 
const errorRtn = (problemJson, status) => {
    if (status > 499) {
        $("#status").text("Problem server side, see debug console");
    } else {
        let keys = Object.keys(problemJson.errors)
        problem = {
            status: status,
            statusText: problemJson.errors[keys[0]][0], //first error
        };
        $("#status").text("Problem cleint side, see brower console");
        console.log(problem);
    }//else
}