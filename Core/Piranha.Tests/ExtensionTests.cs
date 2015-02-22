/*
 * Copyright (c) 2014-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha.vnext
 * 
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Piranha.Tests
{
	/// <summary>
	/// Tests for extensions
	/// </summary>
	[TestClass]
	public class ExtensionTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ExtensionTests() {
			App.Init();
		}

		/// <summary>
		/// Checks that all of the regions have been imported properly
		/// </summary>
		[TestMethod]
		[TestCategory("Extensions")]
		public void ContentBlocks() {
			Assert.AreEqual(3, App.Extensions.ContentBlocks.Count);
		}

		/// <summary>
		/// Checks that all of the properties have been imported properly
		/// </summary>
		[TestMethod]
		[TestCategory("Extensions")]
		public void TemplateFields() {
			Assert.AreEqual(6, App.Extensions.TemplateFields.Count);
		}
	}
}
