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
using DotNetApi.Web;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Requests
{
	/// <summary>
	/// A class representing an asynchronous request for PlanetLab data.
	/// </summary>
	public class PlRequest : AsyncWebRequest
	{
		/// <summary>
		/// Conversion class for an asynchronous operation returning a string.
		/// </summary>
		public class XmlRpcRequestFunction : IAsyncFunction<XmlRpcResponse>
		{
			/// <summary>
			/// Returns a string for the received asynchronous data.
			/// </summary>
			/// <param name="data">The data string.</param>
			/// <returns>The XML RPC response.</returns>
			public XmlRpcResponse GetResult(string data)
			{
				return XmlRpcResponse.Create(data);
			}
		}

		private Uri url = new Uri("https://www.planet-lab.eu/PLCAPI/");
		private XmlRpcRequestFunction funcConvert = new XmlRpcRequestFunction();

		/// <summary>
		/// Creates an asynchronous PlanetLab request with the specified settings.
		/// </summary>
		public PlRequest() { }

		// Public methods.

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="method">The PlanetLab method.</param>
		/// <param name="parameters">The list of parameters.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public IAsyncResult Begin(string method, object[] parameters, AsyncWebRequestCallback callback, object state = null)
		{
			// Create the XML request body.
			byte[] body = XmlRpcRequest.Create(method, parameters);

			// Create the asynchronous state.
			AsyncWebResult asyncState = AsyncWebRequest.Create(this.url, callback, state);

			// Set the request method.
			asyncState.Request.Method = "POST";
			// Set the user agent.
			asyncState.Request.UserAgent = "YouTube Analytics App";
			// Set the content type.
			asyncState.Request.ContentType = "text/xml";
			// Set the body.
			asyncState.SendData.Append(body);

			// Begin the request.
			return this.Begin(asyncState);
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
	}
}
