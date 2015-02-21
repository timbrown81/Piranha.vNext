/*
 * Copyright (c) 2014 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.RavenDb.Tests
{
	/// <summary>
	/// Tests for the author repository.
	/// </summary>
	[TestClass]
	public class AuthorTests : Piranha.Tests.Repositories.AuthorTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public AuthorTests() {
			App.Init(c => {
				// embedded in memory store does not require url and database name
				c.Store = new RavenDb.Store("", "", waitForStaleResults: true, useEmbeddedInMemoryStore: true);
			});
		}

		/// <summary>
		/// Test the author repository.
		/// </summary>
		[TestMethod]
		[TestCategory("RavenDb")]
		public void Author() {
			base.Run();
		}
	}
}
