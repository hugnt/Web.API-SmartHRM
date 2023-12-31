﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SmartHRM.Admin.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartHRM.Admin.Attributes
{
	public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiUrl;
		private const string HOST = "https://localhost:7062";
		private readonly string[] allowedRoles;

		public CustomAuthorizeAttribute(params string[] roles)
		{
			_httpClient = new HttpClient();
			allowedRoles = roles;
		}


		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var accessToken = context.HttpContext.Request.Cookies["AccessToken"];
			if (string.IsNullOrEmpty(accessToken))
			{
				context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
				return;

			}

			var isValidToken = await ValidateAccessTokenAsync(accessToken);

			if (!isValidToken)
			{
				//Get refesh token
				var refreshToken = context.HttpContext.Request.Cookies["RefreshToken"];
				if (string.IsNullOrEmpty(refreshToken))
				{
					context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
					return;
				}

				//get new token
				var newAccessToken = await GetNewAccessTokenAsync(accessToken, refreshToken);
				if (newAccessToken == null || newAccessToken.Data.AccessToken == null || newAccessToken.Data.RefeshToken == null)
				{
					context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
					return;
				}
			

                
				
             
				context.HttpContext.Response.Cookies.Append("AccessToken", newAccessToken.Data.AccessToken);
				context.HttpContext.Response.Cookies.Append("RefreshToken", newAccessToken.Data.RefeshToken);
			}

			var userRoles = context.HttpContext.Request.Cookies["Role"];
			var isExistRole = allowedRoles.Any(r => r.ToLower() == userRoles?.ToLower());
			if (userRoles?.ToLower() != "admin" && !isExistRole)
			{
				context.Result = new RedirectResult("/NotPermission");
				return;
			}
			var currentUrl = context.HttpContext.Request.Path.Value;
			if (string.Equals(currentUrl, "/Tasks/TasksOfEmployee", StringComparison.OrdinalIgnoreCase))
			{
				if (string.Equals(userRoles?.ToLower(), "employee", StringComparison.OrdinalIgnoreCase))
				{
					// Redirect đến trang /Task/TaskOfEmployeeDepartment
					context.Result = new RedirectResult("/Tasks/TaskOfEmployeeDepartment");
					return;
				}
			}

		}

		private async Task<ResponseModel> GetNewAccessTokenAsync(string accessToken, string refeshToken)
		{

			using var client = new HttpClient();
			client.BaseAddress = new Uri(HOST);
			HttpContent body = new StringContent(
				JsonConvert.SerializeObject(new { accessToken, refeshToken }), Encoding.UTF8, "application/json");
			try
			{
				var response = client.PostAsync("/api/User/RenewToken", body).Result;

				if (response.IsSuccessStatusCode)
				{
					var responseContent = await response.Content.ReadAsStringAsync();
					if (!string.IsNullOrEmpty(responseContent))
					{
						var responseObject = JsonConvert.DeserializeObject<ResponseModel>(responseContent);
						if (responseObject != null)
						{
							return responseObject;
						}
					}

				}

			}
			catch (Exception e)
			{
				return null;
			}
			return null;
		}

		private async Task<bool> ValidateAccessTokenAsync(string accessToken)
		{
			const string verifyTokenAPI = HOST + "/api/User/VerifyToken";
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Get, verifyTokenAPI);

				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

				var response = await _httpClient.SendAsync(request);

				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}
	}
}
