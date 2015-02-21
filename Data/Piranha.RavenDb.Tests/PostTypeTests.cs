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
	/// Tests for the post type repository.
	/// </summary>
	[TestClass]
	public class PostTypeTests : Piranha.Tests.Repositories.PostTypeTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PostTypeTests() {
			App.Init(c => {
				// embedded in memory store does not require url and database name
				c.Store = new RavenDb.Store("", "", waitForStaleResults: true, useEmbeddedInMemoryStore: true);
			});
		}

		/// <summary>
		/// Test the post type repository.
		/// </summary>
		[TestMethod]
		[TestCategory("RavenDb")]
		public void PostTypes() {
			base.Run();
		}
	}
}
