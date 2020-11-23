$(function () {
	//fetch from api/employee
	const getAll = async (msg) => {
		try {
			$("#employeeList").text("Finding employee Information...");
			let response = await fetch(`api/employee`);
			if (response.ok) {
				let payload = await response.json();
				buildEmployeeList(payload);
				msg === "" ?
					$("#status").text("Employees Loaded") : $("#status").text(`${msg} - Employees Loaded`);
			} else if (response.status !== 404) {
				let problemJson = await response.json();
				errorRtn(problemJson, response.status);
			} else {
				$("#status").text("no such path on server");
			}
		} catch (error) {
			$("#status").text(error.message);
		}
	};
	//setup for update
	const setupForUpdate = (id, data) => {
		$("#actionbutton").val("update");
		$("#modaltitle").html("<h4>update employee</h4>");

		clearModalFields();
		data.map(employee => {
			if (employee.id === parseInt(id)) {
				$("#TextBoxTitle").val(employee.title);
				$("#TextBoxFirstname").val(employee.firstName);
				$("#TextBoxLastname").val(employee.lastName);
				$("#TextBoxPhone").val(employee.phoneNo);
				$("#TextBoxEmail").val(employee.email);
				sessionStorage.setItem("id", employee.id);
				sessionStorage.setItem("departmentId", employee.departmentId);
				sessionStorage.setItem("timer", employee.timer);
				$("#modalstatus").text("update data");
				$("#theModal").modal("toggle");
				$("#deletebutton").show();
			}
		});
	};
	//sets up for add
	const setupForAdd = () => {
		$("#actionbutton").val("add");
		$("#modaltitle").html("<h4>add employee</h4>");
		$("#theModal").modal("toggle");
		$("#modalstatus").text("add new employee");
		$("#deletebutton").hide();
		clearModalFields();

	};
	//clear modal 
	const clearModalFields = () => {
		$("#TextBoxTitle").val("");
		$("#TextBoxFirstname").val("");
		$("#TextBoxLastname").val("");
		$("#TextBoxPhone").val("");
		$("#TextBoxEmail").val("");
		sessionStorage.removeItem("id");
		sessionStorage.removeItem("departmentId");
		sessionStorage.removeItem("timer");
	};
	// add method - fills an employee object with information from the text boxes
	const add = async () => {
		try {
			emp = new Object();
			emp.title = $("#TextBoxTitle").val();
			emp.firstName = $("#TextBoxFirstname").val();
			emp.lastName = $("#TextBoxLastname").val();
			emp.phoneNo = $("#TextBoxPhone").val();
			emp.email = $("#TextBoxEmail").val();
			emp.departmentId = 100;
			emp.id = -1;
			emp.timer = null;
			emp.StaffPicture64 = null;

			let response = await fetch("api/employee", {
				method: "POST",
				headers: {
					"Content-Type": "application/json; charset=utf-8"
				},
				body: JSON.stringify(emp)
			});
			if (response.ok) {
				let data = await response.json();
				getAll(data.msg);
			} else if (response.status !== 404) {
				let problemJson = await response.json();
				errorRtn(problemJson, response.status);
			} else {
				$("#status").text("no such path on server");
			}
		} catch (error) {
			$("#status").text(error.message);
		}
		$("#theModal").modal("toggle");
	};
	const update = async () => {
		try {
			emp = new Object();

			emp.title = $("#TextBoxTitle").val();
			emp.firstName = $("#TextBoxFirstname").val();
			emp.lastName = $("#TextBoxLastname").val();
			emp.phoneNo = $("#TextBoxPhone").val();
			emp.email = $("#TextBoxEmail").val();

			emp.id = parseInt(sessionStorage.getItem("id"));
			emp.departmentId = parseInt(sessionStorage.getItem("departmentId"));
			emp.timer = sessionStorage.getItem("timer");
			emp.StaffPicture64 = null;
			let response = await fetch("api/employee", {
				method: "PUT",
				headers: { "Content-Type": "application/json; charset=utf-8" },
				body: JSON.stringify(emp)
			});
			if (response.ok) {
				let data = await response.json();
				getAll(data.msg);
			} else if (response.status !== 404) {
				let problemJson = await response.json();
				errorRtn(problemJson, repsonse.status);
			} else {
				$("#status").text("no such path on server");
			}
		} catch (error) {
			$("#status").text(error.message);
		}
		$("#theModal").modal("toggle");
	}
	$("#actionbutton").click(() => {
		$("#actionbutton").val() === "update" ? update() : add();

	});

	$('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });
	$('#deletebutton').click(() => _delete());//if yes was chosen 

	const _delete = async () => {
		try {
			let response = await fetch(`api/employee/${sessionStorage.getItem('id')}`, {
				method: 'DELETE',
				headers: { 'Content-Type': 'application/json; chartset=utf-8' }
			});
			if (response.ok)//or check for response status 
			{
				let data = await response.json();
				getAll(data.msg);
			} else {
				$('#status').text(`Status - ${response.status}, Problem on delete server side, see server console`);
			}//else
			$('#theModal').modal('toggle');

		}
		catch (error) {
			$('#status').text(error.message);
		}
	};//delete 


	$("#employeeList").click((e) => {
		if (!e) e = window.event;
		let id = e.target.parentNode.id;
		if (id === "employeeList" || id === "") {
			id = e.target.id;
		}
		if (id !== "status" && id !== "heading") {
			let data = JSON.parse(sessionStorage.getItem("allemployees"));
			id === "0" ? setupForAdd() : setupForUpdate(id, data);
		} else {
			return false;
		}
	});
	// builds the the employee list
	const buildEmployeeList = (data) => {
		$("#employeeList").empty();
		div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">employee Info</div>
				<div class= "list-group-item row d-flex text-center" id="heading">
				<div class= "col-4 h4">Title</div>
				<div class= "col-4 h4">First</div>
				<div class= "col-4 h4">Last</div>
				 </div>`);
		div.appendTo($("#employeeList"));
		sessionStorage.setItem("allemployees", JSON.stringify(data));
		btn = $(`<button class="list-group=item row d-flex" id="0"><divclass="col-12 text-left">...click to add employee</div></button>`);
		btn.appendTo($("#employeeList"));
		data.map(emp => {
			btn = $(`<button class="list-group-item row d-flex" id="${emp.id}">`);
			btn.html(`<div class="col-4" id="employeetitle${emp.id}">${emp.title}</div>
                      <div class="col-4" id="employeefname${emp.id}">${emp.firstName}</div>
                      <div class="col-4" id="employeelastnam${emp.id}">${emp.lastName}</div>`
			);
			btn.appendTo($("#employeeList"));
		});
	};
	getAll("");
});

// Incase of error
const errorRtn = (problemJson, status) => {
	if (status > 499) {
		$("#status").text("Problem server side, see debug console");
	} else {
		let keys = Object.keys(problemJson.errors)
		problem = {
			status: status,
			statusText: problemJson.errors[keys[0]][0],
		};
		$("#status").text("Problem client side, see browser console");
		console.log(problem);
	}
}