import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
	const LOGIN_URL = "/SignIn"

	$("#password").change(function () {
		if ($(this).val().length > 8) {
			$("#retypePassword").prop("disabled", false);
		}
		else {
			$("#retypePassword").val("");
			$("#retypePassword").prop("disabled", true);
        }
	});

	$(".btn-register").click(async function () {
		var username = $("#username").val();
		var password = $("#password").val();
		var retypePassword = $("#retypePassword").val();

		var infor = { username, password, retypePassword };
		console.log(infor);

		var isValid = validateInfo(infor);

		if (!isValid.status) {
			console.log("Invalid " + isValid.message);
			//Toastify
			Toastify({
				text: "Register failed!",
				close: true,
				className: "bg-danger",
				duration: 2500
			}).showToast();

			return false;
		}

		var newAccount = {
			roleId: 3,
			username: infor.username,
			password: infor.password,
        }

		await postNewAccount("/Account",newAccount);
		return true;
	});

	async function postNewAccount(endPoint, data) {
		try {
			const res = await $.ajax({
				url: `${API.API_URL}${endPoint}`,
				type: "POST",
				data: JSON.stringify(data),
				contentType: "application/json",
				beforeSend: function (xhr) {
					AJAXCONFIG.ajaxBeforeSend(xhr, false);
				}
			});
			if (res) {
				console.log(res);
				Toastify({
					text: "Register successfully!",
					close: true,
					className: "bg-success",
					duration: 2500
				}).showToast();

				var account = {
					Username: data.username,
					Password: data.password
				}
				await postAccount(account);
			}
		} catch (e) {
			Swal.fire({
				icon: "error",
				title: "Oops...",
				text: "Something went wrong!",
			});
			console.log(e);
		}
		finally {
			AJAXCONFIG.ajaxAfterSend();
		}
	}

	async function postAccount(account) {
		try {
			const res = await $.ajax({
				url: LOGIN_URL,
				type: "POST",
				contentType: "application/json",
				data: JSON.stringify(account),
				beforeSend: function (xhr) {
					AJAXCONFIG.ajaxBeforeSend(xhr, false);
				}
			});
			$(".loading").css("display", "none");
			if (res && res.length > 0) {
				window.location.href = res;
			}

		} catch (e) {
			Swal.fire({
				icon: "error",
				title: "Oops...",
				text: "Something went wrong!",
			});
			console.log(e);;
		}
		finally {
			AJAXCONFIG.ajaxAfterSend();
		}
	}

	function validateInfo(infor) {

		if (infor.username == null || infor.username == "" || infor.username.includes(" ") || infor.username.length < 6) {
			return {
				status: false,
				message: `Require: Username is not valid `
			};
		}

		if (infor.password == null || infor.password == "" || infor.password.length < 8) {
			return {
				status: false,
				message: `Require: Password is not valid `
			};
		}

		//check password matches
		if (infor.password !== infor.retypePassword) {
			return {
				status: false,
				message: `Password does not match`
			};
		}

		return {
			status: true,
			message: `infor validation`
		};

    }


	$(".btn-signin").click(function () {
		window.location.href = "/Admin/Login";
	});

});