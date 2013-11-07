/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Globalization;
using System.Security;
using DotNetApi.Web;
using DotNetApi.Web.XmlRpc;
using PlanetLab.Api.Auth;

namespace PlanetLab.Requests
{
	/// <summary>
	/// A class representing an asynchronous request for PlanetLab data.
	/// </summary>
	public sealed class PlRequest : AsyncWebRequest
	{
		public enum RequestMethod
		{
			[PlName("GetSites")]
			GetSites,
			[PlName("GetNodes")]
			GetNodes,
			[PlName("GetPCUs")]
			GetPcus,
			[PlName("GetPersons")]
			GetPersons,
			[PlName("GetSlices")]
			GetSlices,
			[PlName("GetAddresses")]
			GetAddresses,
			[PlName("GetInterfaces")]
			GetInterfaces,
			[PlName("GetNodeGroups")]
			GetNodeGroups,
			[PlName("GetConfFiles")]
			GetConfigurationFiles,
			[PlName("GetKeys")]
			GetKeys,
			[PlName("GetAddressTypes")]
			GetAddressTypes,
			[PlName("GetInterfaceTags")]
			GetInterfaceTags,
			[PlName("GetNodeTags")]
			GetNodeTags,
			[PlName("GetPersonTags")]
			GetPersonTags,
			[PlName("GetSiteTags")]
			GetSiteTags,
			[PlName("GetSliceTags")]
			GetSliceTags,
			[PlName("AddSliceToNodes")]
			AddSliceToNodes,
			[PlName("DeleteSliceFromNodes")]
			DeleteSliceFromNodes,
			[PlName("UpdateSlice")]
			UpdateSlice
		};

		/// <summary>
		/// Conversion class for an asynchronous operation returning a string.
		/// </summary>
		public class XmlRpcRequestFunction : IAsyncFunction<XmlRpcResponse>
		{
			private IFormatProvider format;

			/// <summary>
			/// Creates a new conversion instance for the specified format provider.
			/// </summary>
			/// <param name="format">The format.</param>
			public XmlRpcRequestFunction(IFormatProvider format)
			{
				this.format = format;
			}

			/// <summary>
			/// Returns a string for the received asynchronous data.
			/// </summary>
			/// <param name="data">The data string.</param>
			/// <returns>The XML RPC response.</returns>
			public XmlRpcResponse GetResult(string data)
			{
				return XmlRpcResponse.Create(data, this.format);
			}
		}

		private static readonly Uri url = new Uri("https://www.planet-lab.eu/PLCAPI/");
		private readonly XmlRpcRequestFunction funcConvert;
		private readonly CultureInfo culture = new CultureInfo("en-US");
		private RequestMethod method;

		/// <summary>
		/// Creates an asynchronous PlanetLab request with the specified settings.
		/// </summary>
		public PlRequest(RequestMethod method)
		{
			this.method = method;
			this.funcConvert = new XmlRpcRequestFunction(this.culture);
		}

		// Public methods.

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab password.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public IAsyncResult Begin(string username, SecureString password, AsyncWebRequestCallback callback, object state = null)
		{
			// Check the arguments.
			if (null == username) throw new ArgumentNullException("username");
			if (null == password) throw new ArgumentNullException("password");
			if (null == callback) throw new ArgumentNullException("callback");

			// Create the parameters.
			object[] parameters = new object[1];

			parameters[0] = new PlAuthentication(username, password);

			// Call the base class method.
			return this.Begin(parameters, callback, state);
		}

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab password.</param>
		/// <param name="parameter">The request parameter.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public IAsyncResult Begin(string username, SecureString password, object parameter, AsyncWebRequestCallback callback, object state = null)
		{
			// Check the arguments.
			if (null == username) throw new ArgumentNullException("username");
			if (null == password) throw new ArgumentNullException("password");
			if (null == parameter) throw new ArgumentNullException("parameter");
			if (null == callback) throw new ArgumentNullException("callback");

			// Create the parameters.
			object[] param = new object[2];

			param[0] = new PlAuthentication(username, password);
			param[1] = parameter;

			// Call the base class method.
			return this.Begin(param, callback, state);
		}

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab password.</param>
		/// <param name="parameters">The request parameters.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public IAsyncResult Begin(string username, SecureString password, object[] parameters, AsyncWebRequestCallback callback, object state = null)
		{
			// Check the arguments.
			if (null == username) throw new ArgumentNullException("username");
			if (null == password) throw new ArgumentNullException("password");
			if (null == parameters) throw new ArgumentNullException("parameters");
			if (null == callback) throw new ArgumentNullException("callback");

			// Create the parameters.
			object[] param = new object[parameters.Length + 1];

			param[0] = new PlAuthentication(username, password);
			for (int index = 0; index < parameters.Length; index++)
			{
				param[index + 1] = parameters[index];
			}

			// Call the base class method.
			return this.Begin(param, callback, state);
		}

		/// <summary>
		/// Ends the asynchronus request.
		/// </summary>
		/// <param name="result">The asynchronous result.</param>
		/// <param name="asyncResult">The request state.</param>
		/// <returns>The request result.</returns>
		public XmlRpcResponse End(IAsyncResult result, out AsyncWebResult asyncResult)
		{
			// Get the asynchronous result.
			asyncResult = result as AsyncWebResult;

			// Determine the encoding of the received response
			return this.End<XmlRpcResponse>(result, this.funcConvert);
		}

		// Private methods.

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="parameters">The list of parameters.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		private IAsyncResult Begin(object[] parameters, AsyncWebRequestCallback callback, object state = null)
		{
			// Create the XML request body.
			byte[] body = XmlRpcRequest.Create(this.method.GetName(), parameters);

			// Create the asynchronous state.
			AsyncWebResult asyncState = AsyncWebRequest.Create(PlRequest.url, callback, state);

			// Set the request method.
			asyncState.Request.Method = "POST";
			// Set the user agent.
			asyncState.Request.UserAgent = "Internet Analytics App";
			// Set the content type.
			asyncState.Request.ContentType = "text/xml";
			// Set the accepted language.
			asyncState.Request.Headers["Accept-Language"] = this.culture.Name;
			// Set the body.
			asyncState.SendData.Append(body);

			// Begin the request.
			return this.Begin(asyncState);
		}
	}
}
