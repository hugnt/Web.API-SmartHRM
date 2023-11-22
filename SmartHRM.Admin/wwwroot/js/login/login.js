import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';
$(document).ready(function () {
	const LOGIN_URL = "/SignIn"

	$(".h-screen").on("keydown", async function (event) {
		if (event.which === 13) {
			await $(".btn-signup").click();
		}
	});

	$(".btn-signin").click(async function () {
		var account = {
			Username : $("#username").val(),
			Password: $("#password-input").val()
		}
		await postAccount(account);
	});

	$(".btn-register").click(function () {
		window.location.href = "/SignUp";
	});

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
			if (res && res.length > 0) {
				console.log(res);
				Toastify({
					text: "Login successfully!",
					close: true,
					className: "bg-success",
					duration: 2500
				}).showToast();
				window.location.href = res;
            }
			
		} catch (e) {
			console.log(e);
			Toastify({
				text: "Login failed!",
				close: true,
				className: "bg-danger",
				duration: 2500
			}).showToast();
		}
		finally {
			AJAXCONFIG.ajaxAfterSend();
		}
	}

});